using System;

class Program
{
    static void Main()
    {
        // 問題 6-5: メソッドのオーバーロード
        Console.WriteLine("メソッドオーバーロードのテスト");
        
        // 2つのint値の加算
        Console.WriteLine($"Calculate(5, 3) = {Calculate(5, 3)}");
        
        // 3つのint値の加算
        Console.WriteLine($"Calculate(5, 3, 2) = {Calculate(5, 3, 2)}");
        
        // 2つのdouble値の加算
        Console.WriteLine($"Calculate(5.5, 3.2) = {Calculate(5.5, 3.2)}");
        
        // 文字列の繰り返し
        Console.WriteLine($"Calculate(\"Hello\", 3) = {Calculate("Hello", 3)}");
        Console.WriteLine($"Calculate(\"*\", 5) = {Calculate("*", 5)}");
    }
    
    // 2つのint型パラメータで加算
    static int Calculate(int a, int b)
    {
        return a + b;
    }
    
    // 3つのint型パラメータで加算
    static int Calculate(int a, int b, int c)
    {
        return a + b + c;
    }
    
    // 2つのdouble型パラメータで加算
    static double Calculate(double a, double b)
    {
        return a + b;
    }
    
    // 文字列を指定回数繰り返し
    static string Calculate(string text, int count)
    {
        string result = "";
        for (int i = 0; i < count; i++)
        {
            result += text;
        }
        return result;
    }
}