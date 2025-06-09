using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

class FileDownloadSimulator
{
    private readonly Random random = new Random();
    
    public async Task<string> DownloadFileAsync(string fileName, int sizeMB, IProgress<int> progress = null, CancellationToken cancellationToken = default)
    {
        var sw = Stopwatch.StartNew();
        
        try
        {
            Console.WriteLine($"ダウンロード開始: {fileName} ({sizeMB}MB)");
            
            // ダウンロード時間をシミュレート（1MBあたり100-500ms）
            int totalTime = sizeMB * random.Next(100, 500);
            
            for (int i = 0; i <= 100; i += 10)
            {
                cancellationToken.ThrowIfCancellationRequested();
                
                await Task.Delay(totalTime / 10, cancellationToken);
                progress?.Report(i);
            }
            
            sw.Stop();
            string result = $"{fileName}: 完了 ({sw.ElapsedMilliseconds}ms)";
            Console.WriteLine(result);
            return result;
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine($"{fileName}: キャンセルされました");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{fileName}: エラー - {ex.Message}");
            throw;
        }
    }
    
    public async Task<List<string>> DownloadFilesSequentiallyAsync(List<(string name, int size)> files)
    {
        var results = new List<string>();
        var sw = Stopwatch.StartNew();
        
        Console.WriteLine("\n=== 逐次ダウンロード開始 ===");
        
        foreach (var file in files)
        {
            try
            {
                var result = await DownloadFileAsync(file.name, file.size);
                results.Add(result);
            }
            catch (Exception ex)
            {
                results.Add($"{file.name}: 失敗 - {ex.Message}");
            }
        }
        
        sw.Stop();
        Console.WriteLine($"逐次ダウンロード完了: {sw.ElapsedMilliseconds}ms");
        return results;
    }
    
    public async Task<List<string>> DownloadFilesConcurrentlyAsync(List<(string name, int size)> files)
    {
        var sw = Stopwatch.StartNew();
        
        Console.WriteLine("\n=== 並行ダウンロード開始 ===");
        
        var tasks = new List<Task<string>>();
        
        foreach (var file in files)
        {
            var progress = new Progress<int>(percentage => 
                Console.WriteLine($"{file.name}: {percentage}%"));
            
            tasks.Add(DownloadFileAsync(file.name, file.size, progress));
        }
        
        try
        {
            var results = await Task.WhenAll(tasks);
            sw.Stop();
            Console.WriteLine($"並行ダウンロード完了: {sw.ElapsedMilliseconds}ms");
            return new List<string>(results);
        }
        catch (Exception)
        {
            sw.Stop();
            Console.WriteLine($"並行ダウンロード完了（一部失敗）: {sw.ElapsedMilliseconds}ms");
            
            var results = new List<string>();
            foreach (var task in tasks)
            {
                try
                {
                    results.Add(await task);
                }
                catch (Exception ex)
                {
                    results.Add($"失敗: {ex.Message}");
                }
            }
            return results;
        }
    }
    
    public async Task<List<string>> DownloadFilesWithTimeoutAsync(List<(string name, int size)> files, TimeSpan timeout)
    {
        var sw = Stopwatch.StartNew();
        
        Console.WriteLine($"\n=== タイムアウト付きダウンロード開始 (制限時間: {timeout.TotalSeconds}秒) ===");
        
        using (var cts = new CancellationTokenSource(timeout))
        {
            try
            {
                var tasks = new List<Task<string>>();
                
                foreach (var file in files)
                {
                    tasks.Add(DownloadFileAsync(file.name, file.size, null, cts.Token));
                }
                
                var results = await Task.WhenAll(tasks);
                sw.Stop();
                Console.WriteLine($"タイムアウト付きダウンロード完了: {sw.ElapsedMilliseconds}ms");
                return new List<string>(results);
            }
            catch (OperationCanceledException)
            {
                sw.Stop();
                Console.WriteLine($"タイムアウトによりダウンロード中断: {sw.ElapsedMilliseconds}ms");
                return new List<string> { "タイムアウトエラー" };
            }
        }
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        // 問題 12-1: ファイルダウンロードシミュレーター
        Console.WriteLine("ファイルダウンロードシミュレーター");
        Console.WriteLine("==============================");
        
        var simulator = new FileDownloadSimulator();
        
        var files = new List<(string name, int size)>
        {
            ("document.pdf", 5),
            ("video.mp4", 20),
            ("music.mp3", 8),
            ("image.jpg", 3),
            ("software.zip", 15)
        };
        
        // 単一ファイルのダウンロード（プログレス表示付き）
        Console.WriteLine("\n=== 単一ファイルダウンロード ===");
        var progress = new Progress<int>(percentage => 
            Console.WriteLine($"プログレス: {percentage}%"));
        
        await simulator.DownloadFileAsync("sample.zip", 10, progress);
        
        // 逐次ダウンロード
        var sequentialResults = await simulator.DownloadFilesSequentiallyAsync(files);
        
        // 並行ダウンロード
        var concurrentResults = await simulator.DownloadFilesConcurrentlyAsync(files);
        
        // タイムアウト付きダウンロード
        var timeoutResults = await simulator.DownloadFilesWithTimeoutAsync(files, TimeSpan.FromSeconds(3));
        
        // 結果の比較
        Console.WriteLine("\n=== 結果比較 ===");
        Console.WriteLine("逐次ダウンロード結果:");
        foreach (var result in sequentialResults)
        {
            Console.WriteLine($"  {result}");
        }
        
        Console.WriteLine("\n並行ダウンロード結果:");
        foreach (var result in concurrentResults)
        {
            Console.WriteLine($"  {result}");
        }
        
        Console.WriteLine("\nタイムアウト付きダウンロード結果:");
        foreach (var result in timeoutResults)
        {
            Console.WriteLine($"  {result}");
        }
        
        // パフォーマンスの説明
        Console.WriteLine("\n=== パフォーマンス分析 ===");
        Console.WriteLine("逐次処理: ファイルを一つずつ順番にダウンロード");
        Console.WriteLine("並行処理: 複数のファイルを同時にダウンロード");
        Console.WriteLine("→ 並行処理の方が全体的な処理時間が短縮される");
        Console.WriteLine("→ ただし、リソース使用量は増加する");
    }
}