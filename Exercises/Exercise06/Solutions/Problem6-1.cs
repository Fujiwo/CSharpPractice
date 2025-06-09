using System;

class Program
{
    static void Main()
    {
        // 問題 6-1: 電卓の基本機能を実装
        Console.WriteLine("電卓プログラム");
        Console.Write("1つ目の数値を入力してください: ");
        double num1 = double.Parse(Console.ReadLine());
        
        Console.Write("2つ目の数値を入力してください: ");
        double num2 = double.Parse(Console.ReadLine());
        
        Console.WriteLine($"加算: {num1} + {num2} = {Add(num1, num2)}");
        Console.WriteLine($"減算: {num1} - {num2} = {Subtract(num1, num2)}");
        Console.WriteLine($"乗算: {num1} * {num2} = {Multiply(num1, num2)}");
        
        if (num2 != 0)
        {
            Console.WriteLine($"除算: {num1} / {num2} = {Divide(num1, num2)}");
        }
        else
        {
            Console.WriteLine("除算: エラー - 0で割ることはできません");
        }
    }
    
    static double Add(double a, double b)
    {
        return a + b;
    }
    
    static double Subtract(double a, double b)
    {
        return a - b;
    }
    
    static double Multiply(double a, double b)
    {
        return a * b;
    }
    
    static double Divide(double a, double b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("0で割ることはできません");
        }
        return a / b;
    }
}