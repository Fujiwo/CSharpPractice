using System;

class Program
{
    static void Main()
    {
        // 問題 3-3: 三つの数値の中から最大値を見つける
        Console.Write("1つ目の数値を入力してください: ");
        int num1 = int.Parse(Console.ReadLine());
        
        Console.Write("2つ目の数値を入力してください: ");
        int num2 = int.Parse(Console.ReadLine());
        
        Console.Write("3つ目の数値を入力してください: ");
        int num3 = int.Parse(Console.ReadLine());
        
        int max = num1;
        
        if (num2 > max)
        {
            max = num2;
        }
        
        if (num3 > max)
        {
            max = num3;
        }
        
        Console.WriteLine($"最大値: {max}");
    }
}