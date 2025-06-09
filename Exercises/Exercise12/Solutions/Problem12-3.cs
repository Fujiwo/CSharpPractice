using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

// API レスポンスモデル
class ApiResponse
{
    public string Endpoint { get; set; }
    public bool IsSuccess { get; set; }
    public string Data { get; set; }
    public TimeSpan ResponseTime { get; set; }
    public string ErrorMessage { get; set; }
    public int StatusCode { get; set; }
    
    public override string ToString()
    {
        return IsSuccess 
            ? $"{Endpoint}: 成功 ({ResponseTime.TotalMilliseconds:F0}ms)"
            : $"{Endpoint}: 失敗 ({ErrorMessage})";
    }
}

// レート制限管理
class RateLimiter
{
    private readonly SemaphoreSlim semaphore;
    private readonly Timer resetTimer;
    private readonly int maxRequests;
    private readonly TimeSpan timeWindow;
    
    public RateLimiter(int maxRequests, TimeSpan timeWindow)
    {
        this.maxRequests = maxRequests;
        this.timeWindow = timeWindow;
        this.semaphore = new SemaphoreSlim(maxRequests, maxRequests);
        
        // 定期的にセマフォをリセット
        this.resetTimer = new Timer(Reset, null, timeWindow, timeWindow);
    }
    
    public async Task<bool> TryExecuteAsync(Func<Task> action, CancellationToken cancellationToken = default)
    {
        if (await semaphore.WaitAsync(0, cancellationToken))
        {
            try
            {
                await action();
                return true;
            }
            finally
            {
                // すぐにはリリースしない（レート制限のため）
            }
        }
        return false;
    }
    
    private void Reset(object state)
    {
        // 利用可能なリクエスト数をリセット
        var currentCount = semaphore.CurrentCount;
        var toRelease = maxRequests - currentCount;
        if (toRelease > 0)
        {
            semaphore.Release(toRelease);
        }
    }
    
    public void Dispose()
    {
        resetTimer?.Dispose();
        semaphore?.Dispose();
    }
}

class WebClientSimulator
{
    private readonly Random random = new Random();
    private readonly RateLimiter rateLimiter;
    
    public WebClientSimulator()
    {
        // 1秒間に最大5リクエストまで
        rateLimiter = new RateLimiter(5, TimeSpan.FromSeconds(1));
    }
    
    // APIリクエストのシミュレーション
    public async Task<ApiResponse> SendRequestAsync(string endpoint, int timeoutMs = 5000, CancellationToken cancellationToken = default)
    {
        var sw = Stopwatch.StartNew();
        
        try
        {
            using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            timeoutCts.CancelAfter(timeoutMs);
            
            // リクエスト処理時間をシミュレート
            int responseTime = random.Next(100, 3000);
            await Task.Delay(responseTime, timeoutCts.Token);
            
            // 時々エラーを発生させる
            if (random.Next(1, 10) > 7)
            {
                throw new InvalidOperationException("サーバーエラー");
            }
            
            sw.Stop();
            
            return new ApiResponse
            {
                Endpoint = endpoint,
                IsSuccess = true,
                Data = $"Response from {endpoint}",
                ResponseTime = sw.Elapsed,
                StatusCode = 200
            };
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            sw.Stop();
            return new ApiResponse
            {
                Endpoint = endpoint,
                IsSuccess = false,
                ErrorMessage = "リクエストがキャンセルされました",
                ResponseTime = sw.Elapsed,
                StatusCode = 0
            };
        }
        catch (OperationCanceledException)
        {
            sw.Stop();
            return new ApiResponse
            {
                Endpoint = endpoint,
                IsSuccess = false,
                ErrorMessage = "タイムアウト",
                ResponseTime = sw.Elapsed,
                StatusCode = 408
            };
        }
        catch (Exception ex)
        {
            sw.Stop();
            return new ApiResponse
            {
                Endpoint = endpoint,
                IsSuccess = false,
                ErrorMessage = ex.Message,
                ResponseTime = sw.Elapsed,
                StatusCode = 500
            };
        }
    }
    
    // リトライ機能付きリクエスト
    public async Task<ApiResponse> SendRequestWithRetryAsync(string endpoint, int maxRetries = 3, CancellationToken cancellationToken = default)
    {
        for (int attempt = 1; attempt <= maxRetries; attempt++)
        {
            var response = await SendRequestAsync(endpoint, 2000, cancellationToken);
            
            if (response.IsSuccess)
            {
                return response;
            }
            
            Console.WriteLine($"リトライ {attempt}/{maxRetries}: {endpoint} - {response.ErrorMessage}");
            
            if (attempt < maxRetries)
            {
                // 指数バックオフ
                var delay = TimeSpan.FromMilliseconds(Math.Pow(2, attempt) * 1000);
                await Task.Delay(delay, cancellationToken);
            }
        }
        
        // 最後の試行
        return await SendRequestAsync(endpoint, 2000, cancellationToken);
    }
    
    // レート制限付きリクエスト
    public async Task<ApiResponse> SendRequestWithRateLimitAsync(string endpoint, CancellationToken cancellationToken = default)
    {
        bool executed = await rateLimiter.TryExecuteAsync(async () =>
        {
            // 実際のリクエスト処理は別で実行
        }, cancellationToken);
        
        if (!executed)
        {
            return new ApiResponse
            {
                Endpoint = endpoint,
                IsSuccess = false,
                ErrorMessage = "レート制限により拒否されました",
                StatusCode = 429
            };
        }
        
        return await SendRequestAsync(endpoint, 3000, cancellationToken);
    }
    
    // 複数のAPIエンドポイントに同時リクエスト
    public async Task<List<ApiResponse>> SendConcurrentRequestsAsync(List<string> endpoints, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"\n=== 同時リクエスト開始 ({endpoints.Count}件) ===");
        
        var tasks = endpoints.Select(endpoint => 
            SendRequestWithRetryAsync(endpoint, 2, cancellationToken)).ToList();
        
        var responses = await Task.WhenAll(tasks);
        
        Console.WriteLine("同時リクエスト完了");
        return responses.ToList();
    }
    
    // レート制限テスト
    public async Task TestRateLimitAsync(List<string> endpoints, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"\n=== レート制限テスト開始 ===");
        
        var tasks = endpoints.Select(endpoint =>
            SendRequestWithRateLimitAsync(endpoint, cancellationToken)).ToList();
        
        var responses = await Task.WhenAll(tasks);
        
        var rateLimitedCount = responses.Count(r => r.StatusCode == 429);
        Console.WriteLine($"レート制限テスト完了 - 制限された要求: {rateLimitedCount}件");
        
        foreach (var response in responses)
        {
            Console.WriteLine($"  {response}");
        }
    }
    
    // 結果レポート生成
    public void GenerateReport(List<ApiResponse> responses)
    {
        Console.WriteLine("\n=== レポート ===");
        
        var successCount = responses.Count(r => r.IsSuccess);
        var failureCount = responses.Count - successCount;
        var averageResponseTime = responses.Where(r => r.IsSuccess).Average(r => r.ResponseTime.TotalMilliseconds);
        var maxResponseTime = responses.Max(r => r.ResponseTime.TotalMilliseconds);
        var minResponseTime = responses.Min(r => r.ResponseTime.TotalMilliseconds);
        
        Console.WriteLine($"総リクエスト数: {responses.Count}");
        Console.WriteLine($"成功: {successCount}件 ({(double)successCount / responses.Count * 100:F1}%)");
        Console.WriteLine($"失敗: {failureCount}件 ({(double)failureCount / responses.Count * 100:F1}%)");
        Console.WriteLine($"平均レスポンス時間: {averageResponseTime:F0}ms");
        Console.WriteLine($"最大レスポンス時間: {maxResponseTime:F0}ms");
        Console.WriteLine($"最小レスポンス時間: {minResponseTime:F0}ms");
        
        // エラーの種類別集計
        var errorGroups = responses.Where(r => !r.IsSuccess)
            .GroupBy(r => r.ErrorMessage)
            .Select(g => new { Error = g.Key, Count = g.Count() });
        
        if (errorGroups.Any())
        {
            Console.WriteLine("\nエラー種別:");
            foreach (var error in errorGroups)
            {
                Console.WriteLine($"  {error.Error}: {error.Count}件");
            }
        }
        
        // レスポンス時間の分布
        var fastRequests = responses.Count(r => r.ResponseTime.TotalMilliseconds < 1000);
        var mediumRequests = responses.Count(r => r.ResponseTime.TotalMilliseconds >= 1000 && r.ResponseTime.TotalMilliseconds < 2000);
        var slowRequests = responses.Count(r => r.ResponseTime.TotalMilliseconds >= 2000);
        
        Console.WriteLine("\nレスポンス時間分布:");
        Console.WriteLine($"  高速 (<1s): {fastRequests}件");
        Console.WriteLine($"  中程度 (1-2s): {mediumRequests}件");
        Console.WriteLine($"  低速 (>=2s): {slowRequests}件");
    }
    
    public void Dispose()
    {
        rateLimiter?.Dispose();
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        // 問題 12-3: Webクライアントシミュレーター
        Console.WriteLine("Webクライアントシミュレーター");
        Console.WriteLine("============================");
        
        using var simulator = new WebClientSimulator();
        
        var endpoints = new List<string>
        {
            "/api/users",
            "/api/products",
            "/api/orders",
            "/api/inventory",
            "/api/analytics",
            "/api/reports",
            "/api/settings",
            "/api/notifications"
        };
        
        using var cts = new CancellationTokenSource();
        cts.CancelAfter(TimeSpan.FromSeconds(30)); // 30秒でタイムアウト
        
        try
        {
            // 単一リクエストのテスト
            Console.WriteLine("=== 単一リクエストテスト ===");
            var singleResponse = await simulator.SendRequestAsync("/api/test");
            Console.WriteLine(singleResponse);
            
            // リトライ機能のテスト
            Console.WriteLine("\n=== リトライ機能テスト ===");
            var retryResponse = await simulator.SendRequestWithRetryAsync("/api/unstable", 3, cts.Token);
            Console.WriteLine(retryResponse);
            
            // 同時リクエストのテスト
            var responses = await simulator.SendConcurrentRequestsAsync(endpoints, cts.Token);
            
            // レート制限のテスト
            await simulator.TestRateLimitAsync(endpoints.Take(10).ToList(), cts.Token);
            
            // レポート生成
            simulator.GenerateReport(responses);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("\nタイムアウトによりプログラムが中断されました");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nエラーが発生しました: {ex.Message}");
        }
        
        Console.WriteLine("\nプログラム終了");
    }
}