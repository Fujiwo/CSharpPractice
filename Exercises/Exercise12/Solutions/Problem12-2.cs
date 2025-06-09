using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

// データ処理用のモデルクラス
class DataItem
{
    public int Id { get; set; }
    public string Data { get; set; }
    public DateTime Timestamp { get; set; }
    
    public DataItem(int id, string data)
    {
        Id = id;
        Data = data;
        Timestamp = DateTime.Now;
    }
    
    public override string ToString()
    {
        return $"ID:{Id}, Data:{Data}, Time:{Timestamp:HH:mm:ss}";
    }
}

class ProcessedData
{
    public int Id { get; set; }
    public string ProcessedValue { get; set; }
    public DateTime ProcessingTime { get; set; }
    public TimeSpan ProcessingDuration { get; set; }
    
    public override string ToString()
    {
        return $"ID:{Id}, Processed:{ProcessedValue}, Duration:{ProcessingDuration.TotalMilliseconds}ms";
    }
}

class AsyncDataProcessor
{
    private readonly Random random = new Random();
    private int retryCount = 0;
    
    // データ取得の非同期ストリーム
    public async IAsyncEnumerable<DataItem> GenerateDataAsync(int count, 
        [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"データ生成開始: {count}件");
        
        for (int i = 1; i <= count; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            // データ生成の遅延をシミュレート
            await Task.Delay(random.Next(50, 200), cancellationToken);
            
            var data = new DataItem(i, $"データ_{i}_{random.Next(1000, 9999)}");
            Console.WriteLine($"生成: {data}");
            
            yield return data;
        }
        
        Console.WriteLine("データ生成完了");
    }
    
    // データ変換処理
    public async Task<ProcessedData> TransformDataAsync(DataItem input, CancellationToken cancellationToken = default)
    {
        var startTime = DateTime.Now;
        
        try
        {
            // 変換処理の遅延をシミュレート
            await Task.Delay(random.Next(100, 500), cancellationToken);
            
            // 時々エラーを発生させる（リトライのテスト用）
            if (random.Next(1, 10) > 8)
            {
                throw new InvalidOperationException($"データ変換エラー (ID: {input.Id})");
            }
            
            var result = new ProcessedData
            {
                Id = input.Id,
                ProcessedValue = $"変換済み_{input.Data.ToUpper()}",
                ProcessingTime = DateTime.Now,
                ProcessingDuration = DateTime.Now - startTime
            };
            
            Console.WriteLine($"変換完了: {result}");
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"変換エラー: {ex.Message}");
            throw;
        }
    }
    
    // リトライ機能付きデータ変換
    public async Task<ProcessedData> TransformDataWithRetryAsync(DataItem input, int maxRetries = 3, CancellationToken cancellationToken = default)
    {
        for (int attempt = 1; attempt <= maxRetries; attempt++)
        {
            try
            {
                return await TransformDataAsync(input, cancellationToken);
            }
            catch (Exception ex) when (attempt < maxRetries)
            {
                retryCount++;
                Console.WriteLine($"リトライ {attempt}/{maxRetries}: {ex.Message}");
                await Task.Delay(TimeSpan.FromMilliseconds(attempt * 100), cancellationToken);
            }
        }
        
        // 最大リトライ回数に達した場合は最後にもう一度試行
        return await TransformDataAsync(input, cancellationToken);
    }
    
    // データ保存処理
    public async Task SaveDataAsync(ProcessedData data, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        // 保存処理の遅延をシミュレート
        await Task.Delay(random.Next(50, 150), cancellationToken);
        
        Console.WriteLine($"保存完了: ID:{data.Id}");
    }
    
    // バッチ処理パイプライン
    public async Task<List<ProcessedData>> ProcessBatchAsync(int dataCount, CancellationToken cancellationToken = default)
    {
        Console.WriteLine("\n=== バッチ処理開始 ===");
        var results = new List<ProcessedData>();
        
        try
        {
            await foreach (var item in GenerateDataAsync(dataCount, cancellationToken))
            {
                var processed = await TransformDataWithRetryAsync(item, 3, cancellationToken);
                await SaveDataAsync(processed, cancellationToken);
                results.Add(processed);
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("バッチ処理がキャンセルされました");
            throw;
        }
        
        Console.WriteLine($"バッチ処理完了: {results.Count}件処理");
        return results;
    }
    
    // ストリーム処理パイプライン
    public async Task ProcessStreamAsync(int dataCount, int concurrency = 3, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"\n=== ストリーム処理開始 (並行度: {concurrency}) ===");
        
        var semaphore = new SemaphoreSlim(concurrency, concurrency);
        var tasks = new List<Task>();
        
        try
        {
            await foreach (var item in GenerateDataAsync(dataCount, cancellationToken))
            {
                var task = ProcessSingleItemAsync(item, semaphore, cancellationToken);
                tasks.Add(task);
            }
            
            await Task.WhenAll(tasks);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("ストリーム処理がキャンセルされました");
            throw;
        }
        
        Console.WriteLine("ストリーム処理完了");
    }
    
    private async Task ProcessSingleItemAsync(DataItem item, SemaphoreSlim semaphore, CancellationToken cancellationToken)
    {
        await semaphore.WaitAsync(cancellationToken);
        
        try
        {
            var processed = await TransformDataWithRetryAsync(item, 3, cancellationToken);
            await SaveDataAsync(processed, cancellationToken);
        }
        finally
        {
            semaphore.Release();
        }
    }
    
    public void DisplayStatistics()
    {
        Console.WriteLine($"\n=== 統計情報 ===");
        Console.WriteLine($"総リトライ回数: {retryCount}");
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        // 問題 12-2: 非同期データ処理パイプライン
        Console.WriteLine("非同期データ処理パイプライン");
        Console.WriteLine("==========================");
        
        var processor = new AsyncDataProcessor();
        
        // キャンセレーショントークンの設定
        using var cts = new CancellationTokenSource();
        
        // 10秒後に自動キャンセル
        cts.CancelAfter(TimeSpan.FromSeconds(10));
        
        try
        {
            // バッチ処理のテスト
            var batchResults = await processor.ProcessBatchAsync(5, cts.Token);
            
            Console.WriteLine("\nバッチ処理結果:");
            foreach (var result in batchResults)
            {
                Console.WriteLine($"  {result}");
            }
            
            // ストリーム処理のテスト
            await processor.ProcessStreamAsync(5, 2, cts.Token);
            
            processor.DisplayStatistics();
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("\n処理がタイムアウトによりキャンセルされました");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nエラーが発生しました: {ex.Message}");
        }
        
        // 手動キャンセルのデモ
        Console.WriteLine("\n=== 手動キャンセルのデモ ===");
        Console.WriteLine("5秒後にEnterキーを押すとキャンセルされます");
        
        using var manualCts = new CancellationTokenSource();
        
        // バックグラウンドでキャンセル監視
        _ = Task.Run(async () =>
        {
            await Task.Delay(5000);
            Console.WriteLine("5秒経過 - Enterキーでキャンセル可能");
            Console.ReadLine();
            manualCts.Cancel();
            Console.WriteLine("キャンセル要求を送信しました");
        });
        
        try
        {
            await processor.ProcessBatchAsync(10, manualCts.Token);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("手動キャンセルが実行されました");
        }
        
        Console.WriteLine("\nプログラム終了");
    }
}