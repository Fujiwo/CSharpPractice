using System;
using System.Collections.Generic;
using System.IO;

public class InvalidCalculatorOperationException : Exception
{
    public InvalidCalculatorOperationException(string message) : base(message) { }
    public InvalidCalculatorOperationException(string message, Exception innerException) : base(message, innerException) { }
}

class Calculator
{
    private List<string> operationLog = new List<string>();
    private List<string> errorLog = new List<string>();
    
    public double Add(double a, double b)
    {
        try
        {
            if (double.IsInfinity(a) || double.IsInfinity(b))
            {
                throw new ArgumentException("無限大の値は計算できません");
            }
            
            double result = a + b;
            LogOperation($"{a} + {b} = {result}");
            return result;
        }
        catch (Exception ex)
        {
            LogError($"加算エラー: {ex.Message}");
            throw;
        }
    }
    
    public double Subtract(double a, double b)
    {
        try
        {
            if (double.IsInfinity(a) || double.IsInfinity(b))
            {
                throw new ArgumentException("無限大の値は計算できません");
            }
            
            double result = a - b;
            LogOperation($"{a} - {b} = {result}");
            return result;
        }
        catch (Exception ex)
        {
            LogError($"減算エラー: {ex.Message}");
            throw;
        }
    }
    
    public double Multiply(double a, double b)
    {
        try
        {
            if (double.IsInfinity(a) || double.IsInfinity(b))
            {
                throw new ArgumentException("無限大の値は計算できません");
            }
            
            double result = a * b;
            
            if (double.IsInfinity(result))
            {
                throw new InvalidCalculatorOperationException("計算結果が無限大になりました");
            }
            
            LogOperation($"{a} * {b} = {result}");
            return result;
        }
        catch (Exception ex)
        {
            LogError($"乗算エラー: {ex.Message}");
            throw;
        }
    }
    
    public double Divide(double a, double b)
    {
        try
        {
            if (b == 0)
            {
                throw new DivideByZeroException("0で割ることはできません");
            }
            
            if (double.IsInfinity(a) || double.IsInfinity(b))
            {
                throw new ArgumentException("無限大の値は計算できません");
            }
            
            double result = a / b;
            
            if (double.IsInfinity(result))
            {
                throw new InvalidCalculatorOperationException("計算結果が無限大になりました");
            }
            
            LogOperation($"{a} / {b} = {result}");
            return result;
        }
        catch (Exception ex)
        {
            LogError($"除算エラー: {ex.Message}");
            throw;
        }
    }
    
    private void LogOperation(string operation)
    {
        string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 操作: {operation}";
        operationLog.Add(logEntry);
        Console.WriteLine($"✓ {operation}");
    }
    
    private void LogError(string error)
    {
        string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] エラー: {error}";
        errorLog.Add(logEntry);
        Console.WriteLine($"✗ {error}");
    }
    
    public void DisplayLogs()
    {
        Console.WriteLine("\n=== 操作ログ ===");
        foreach (string log in operationLog)
        {
            Console.WriteLine(log);
        }
        
        if (errorLog.Count > 0)
        {
            Console.WriteLine("\n=== エラーログ ===");
            foreach (string log in errorLog)
            {
                Console.WriteLine(log);
            }
        }
    }
    
    public void SaveLogsToFile(string fileName)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine("=== Calculator Logs ===");
                writer.WriteLine($"Generated: {DateTime.Now}");
                writer.WriteLine();
                
                writer.WriteLine("=== Operations ===");
                foreach (string log in operationLog)
                {
                    writer.WriteLine(log);
                }
                
                if (errorLog.Count > 0)
                {
                    writer.WriteLine();
                    writer.WriteLine("=== Errors ===");
                    foreach (string log in errorLog)
                    {
                        writer.WriteLine(log);
                    }
                }
            }
            Console.WriteLine($"ログをファイルに保存しました: {fileName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ログ保存エラー: {ex.Message}");
        }
    }
}

class Program
{
    static void Main()
    {
        // 問題 10-3: 電卓アプリケーションの包括的例外処理
        Console.WriteLine("高機能電卓（例外処理付き）");
        Console.WriteLine("=========================");
        
        Calculator calc = new Calculator();
        
        // テスト計算の実行
        PerformCalculations(calc);
        
        // ログの表示
        calc.DisplayLogs();
        
        // ログをファイルに保存
        calc.SaveLogsToFile("calculator_log.txt");
    }
    
    static void PerformCalculations(Calculator calc)
    {
        // 正常な計算
        try
        {
            calc.Add(10, 5);
            calc.Subtract(20, 8);
            calc.Multiply(6, 7);
            calc.Divide(15, 3);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"計算エラー: {ex.Message}");
        }
        
        // エラーが発生する計算
        try
        {
            Console.WriteLine("\n--- エラーケースのテスト ---");
            calc.Divide(10, 0); // DivideByZeroException
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine($"ゼロ除算エラー: {ex.Message}");
        }
        
        try
        {
            calc.Add(double.PositiveInfinity, 10); // ArgumentException
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"引数エラー: {ex.Message}");
        }
        
        try
        {
            calc.Multiply(double.MaxValue, 2); // InvalidCalculatorOperationException
        }
        catch (InvalidCalculatorOperationException ex)
        {
            Console.WriteLine($"計算操作エラー: {ex.Message}");
        }
        
        // 処理を継続
        try
        {
            Console.WriteLine("\n--- 処理継続 ---");
            calc.Add(100, 200);
            calc.Subtract(500, 300);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"予期しないエラー: {ex.Message}");
        }
    }
}