using System;

class Program
{
    static void Main()
    {
        // 問題 10-1: 安全な数値入力システム
        Console.WriteLine("安全な数値入力システム");
        Console.WriteLine("===================");
        
        int number = GetSafeIntegerInput("整数を入力してください: ");
        double decimalNumber = GetSafeDoubleInput("小数を入力してください: ");
        
        Console.WriteLine($"入力された整数: {number}");
        Console.WriteLine($"入力された小数: {decimalNumber}");
    }
    
    static int GetSafeIntegerInput(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = null;
            
            try
            {
                input = Console.ReadLine();
                int result = int.Parse(input);
                return result;
            }
            catch (FormatException)
            {
                Console.WriteLine($"エラー: '{input}' は有効な整数ではありません。数値を入力してください。");
            }
            catch (OverflowException)
            {
                Console.WriteLine($"エラー: '{input}' は整数の範囲を超えています。");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("エラー: 入力が空です。数値を入力してください。");
            }
            finally
            {
                // リソースクリーンアップのデモンストレーション
                if (input != null && input.Length > 10)
                {
                    Console.WriteLine("長い入力文字列のメモリをクリーンアップしました。");
                }
            }
        }
    }
    
    static double GetSafeDoubleInput(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = null;
            
            try
            {
                input = Console.ReadLine();
                double result = double.Parse(input);
                
                if (double.IsInfinity(result))
                {
                    throw new OverflowException("数値が無限大です");
                }
                
                if (double.IsNaN(result))
                {
                    throw new FormatException("数値が無効です");
                }
                
                return result;
            }
            catch (FormatException)
            {
                Console.WriteLine($"エラー: '{input}' は有効な数値ではありません。");
            }
            catch (OverflowException ex)
            {
                Console.WriteLine($"エラー: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("入力処理を完了しました。");
            }
        }
    }
}