using System;
using System.IO;
using System.Collections.Generic;

public class FileProcessingException : Exception
{
    public FileProcessingException(string message) : base(message) { }
    public FileProcessingException(string message, Exception innerException) : base(message, innerException) { }
}

class FileProcessor
{
    private List<string> processedFiles = new List<string>();
    private List<string> failedFiles = new List<string>();
    
    public void ProcessFiles(string[] filePaths)
    {
        Console.WriteLine("ファイル処理を開始します...\n");
        
        foreach (string filePath in filePaths)
        {
            ProcessSingleFile(filePath);
        }
        
        DisplaySummary();
    }
    
    private void ProcessSingleFile(string filePath)
    {
        try
        {
            Console.WriteLine($"ファイル処理中: {filePath}");
            
            // ファイル存在チェック
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"ファイルが見つかりません: {filePath}");
            }
            
            // ファイル読み込み
            string content = File.ReadAllText(filePath);
            
            // ファイル内容の検証
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new FileProcessingException($"ファイルが空です: {filePath}");
            }
            
            // 処理のシミュレーション
            ProcessFileContent(content, filePath);
            
            processedFiles.Add(filePath);
            Console.WriteLine($"✓ 処理完了: {filePath}\n");
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"✗ ファイル未発見エラー: {ex.Message}");
            failedFiles.Add(filePath);
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"✗ アクセス権限エラー: {ex.Message}");
            failedFiles.Add(filePath);
        }
        catch (IOException ex)
        {
            Console.WriteLine($"✗ I/Oエラー: {ex.Message}");
            failedFiles.Add(filePath);
        }
        catch (FileProcessingException ex)
        {
            Console.WriteLine($"✗ 処理エラー: {ex.Message}");
            failedFiles.Add(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ 予期しないエラー: {ex.Message}");
            failedFiles.Add(filePath);
        }
        finally
        {
            // リソースクリーンアップ
            GC.Collect();
        }
    }
    
    private void ProcessFileContent(string content, string filePath)
    {
        // ファイル処理のシミュレーション
        Console.WriteLine($"  内容サイズ: {content.Length} bytes");
        Console.WriteLine($"  行数: {content.Split('\n').Length}");
        
        // 処理時間のシミュレーション
        System.Threading.Thread.Sleep(100);
    }
    
    private void DisplaySummary()
    {
        Console.WriteLine("=== 処理結果サマリー ===");
        Console.WriteLine($"処理成功: {processedFiles.Count}件");
        Console.WriteLine($"処理失敗: {failedFiles.Count}件");
        
        if (processedFiles.Count > 0)
        {
            Console.WriteLine("\n成功したファイル:");
            foreach (string file in processedFiles)
            {
                Console.WriteLine($"  ✓ {file}");
            }
        }
        
        if (failedFiles.Count > 0)
        {
            Console.WriteLine("\n失敗したファイル:");
            foreach (string file in failedFiles)
            {
                Console.WriteLine($"  ✗ {file}");
            }
        }
    }
}

class Program
{
    static void Main()
    {
        // 問題 10-2: ファイル読み込み処理の例外処理
        Console.WriteLine("ファイル処理システム");
        Console.WriteLine("==================");
        
        // テスト用のファイルパス（実際には存在しないファイルも含む）
        string[] testFiles = {
            "existing_file.txt",
            "non_existent_file.txt", 
            "/system/protected_file.txt",
            "empty_file.txt"
        };
        
        // テスト用ファイルを作成
        CreateTestFiles();
        
        FileProcessor processor = new FileProcessor();
        processor.ProcessFiles(testFiles);
        
        // テストファイルをクリーンアップ
        CleanupTestFiles();
    }
    
    static void CreateTestFiles()
    {
        try
        {
            File.WriteAllText("existing_file.txt", "これはテスト用のファイル内容です。\n複数行のテストデータです。");
            File.WriteAllText("empty_file.txt", "");
            Console.WriteLine("テスト用ファイルを作成しました。\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"テストファイルの作成に失敗しました: {ex.Message}");
        }
    }
    
    static void CleanupTestFiles()
    {
        try
        {
            if (File.Exists("existing_file.txt")) File.Delete("existing_file.txt");
            if (File.Exists("empty_file.txt")) File.Delete("empty_file.txt");
            Console.WriteLine("\nテスト用ファイルをクリーンアップしました。");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"クリーンアップエラー: {ex.Message}");
        }
    }
}