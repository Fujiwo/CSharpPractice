using System;

class Program
{
    static void Main()
    {
        // 問題 6-4: 再帰メソッドを使用した数学的計算
        Console.WriteLine("再帰メソッドのテスト");
        
        // 階乗のテスト
        int factorialNum = 5;
        Console.WriteLine($"{factorialNum}! = {Factorial(factorialNum)}");
        
        // フィボナッチ数列のテスト
        int fibNum = 10;
        Console.WriteLine($"フィボナッチ数列の{fibNum}番目: {Fibonacci(fibNum)}");
        
        // 桁数計算のテスト
        int digitNum = 12345;
        Console.WriteLine($"{digitNum}の桁数: {CountDigits(digitNum)}");
        
        // より大きな数での桁数テスト
        digitNum = 987654321;
        Console.WriteLine($"{digitNum}の桁数: {CountDigits(digitNum)}");
    }
    
    static int Factorial(int n)
    {
        if (n <= 1)
        {
            return 1;
        }
        return n * Factorial(n - 1);
    }
    
    static int Fibonacci(int n)
    {
        if (n <= 1)
        {
            return n;
        }
        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }
    
    static int CountDigits(int n)
    {
        if (n == 0)
        {
            return 1;
        }
        if (n < 10)
        {
            return 1;
        }
        return 1 + CountDigits(n / 10);
    }
}