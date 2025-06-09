using System;

class Program
{
    static void Main()
    {
        // 問題 2-2: ユーザーから入力を受け取り、計算結果を出力
        Console.Write("1つ目の整数を入力してください: ");
        int num1 = int.Parse(Console.ReadLine());
        
        Console.Write("2つ目の整数を入力してください: ");
        int num2 = int.Parse(Console.ReadLine());
        
        int addition = num1 + num2;
        int subtraction = num1 - num2;
        int multiplication = num1 * num2;
        double division = (double)num1 / num2;
        
        Console.WriteLine($"加算: {num1} + {num2} = {addition}");
        Console.WriteLine($"減算: {num1} - {num2} = {subtraction}");
        Console.WriteLine($"乗算: {num1} * {num2} = {multiplication}");
        Console.WriteLine($"除算: {num1} / {num2} = {division:F2}");
    }
}