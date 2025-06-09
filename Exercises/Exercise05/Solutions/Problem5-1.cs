using System;

class Program
{
    static void Main()
    {
        // 問題 5-1: 整数配列の合計と平均を計算
        int[] numbers = { 10, 25, 33, 47, 52, 68, 71, 89, 94, 100 };
        
        int sum = 0;
        
        Console.WriteLine("配列の要素:");
        foreach (int number in numbers)
        {
            Console.Write(number + " ");
            sum += number;
        }
        
        double average = (double)sum / numbers.Length;
        
        Console.WriteLine();
        Console.WriteLine($"合計: {sum}");
        Console.WriteLine($"平均: {average:F2}");
    }
}