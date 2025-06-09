using System;

interface IFileProcessor
{
    void Process(string content);
    string GetProcessorName();
}

class TextProcessor : IFileProcessor
{
    public void Process(string content)
    {
        Console.WriteLine($"テキスト処理を実行中...");
        Console.WriteLine($"内容: {content}");
        Console.WriteLine($"文字数: {content.Length}文字");
        Console.WriteLine($"行数: {content.Split('\n').Length}行");
    }
    
    public string GetProcessorName()
    {
        return "テキストプロセッサー";
    }
}

class CsvProcessor : IFileProcessor
{
    public void Process(string content)
    {
        Console.WriteLine($"CSV処理を実行中...");
        string[] lines = content.Split('\n');
        Console.WriteLine($"レコード数: {lines.Length}行");
        
        if (lines.Length > 0)
        {
            string[] columns = lines[0].Split(',');
            Console.WriteLine($"列数: {columns.Length}列");
            Console.WriteLine("カラム名:");
            foreach (string column in columns)
            {
                Console.WriteLine($"  - {column.Trim()}");
            }
        }
    }
    
    public string GetProcessorName()
    {
        return "CSVプロセッサー";
    }
}

class JsonProcessor : IFileProcessor
{
    public void Process(string content)
    {
        Console.WriteLine($"JSON処理を実行中...");
        Console.WriteLine($"JSON内容: {content}");
        
        // 簡単なJSON解析デモ
        int openBraces = 0, closeBraces = 0;
        foreach (char c in content)
        {
            if (c == '{') openBraces++;
            if (c == '}') closeBraces++;
        }
        
        Console.WriteLine($"開始ブレース: {openBraces}個");
        Console.WriteLine($"終了ブレース: {closeBraces}個");
        Console.WriteLine($"JSONの妥当性: {(openBraces == closeBraces ? "有効" : "無効")}");
    }
    
    public string GetProcessorName()
    {
        return "JSONプロセッサー";
    }
}

class Program
{
    static void Main()
    {
        // 問題 9-2: ファイル処理システム
        IFileProcessor[] processors = {
            new TextProcessor(),
            new CsvProcessor(),
            new JsonProcessor()
        };
        
        string[] testContents = {
            "これはテストテキストです。\n複数行のサンプルです。",
            "名前,年齢,職業\n田中太郎,25,エンジニア\n佐藤花子,30,デザイナー",
            "{\"name\": \"田中太郎\", \"age\": 25, \"job\": \"エンジニア\"}"
        };
        
        Console.WriteLine("ファイル処理システム");
        Console.WriteLine("===================");
        
        for (int i = 0; i < processors.Length; i++)
        {
            Console.WriteLine($"\n{processors[i].GetProcessorName()}で処理中:");
            Console.WriteLine("----------------------------------------");
            processors[i].Process(testContents[i]);
            Console.WriteLine();
        }
    }
}