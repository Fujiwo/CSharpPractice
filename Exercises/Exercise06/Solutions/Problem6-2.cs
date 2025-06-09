using System;

class Program
{
    static void Main()
    {
        // 問題 6-2: 数値配列の統計情報を計算
        int[] numbers = { 15, 32, 8, 47, 91, 23, 6, 78, 54, 12 };
        
        Console.WriteLine("配列の要素:");
        foreach (int number in numbers)
        {
            Console.Write(number + " ");
        }
        Console.WriteLine();
        
        int max = FindMax(numbers);
        int min = FindMin(numbers);
        double average = FindAverage(numbers);
        
        Console.WriteLine($"最大値: {max}");
        Console.WriteLine($"最小値: {min}");
        Console.WriteLine($"平均値: {average:F2}");
    }
    
    static int FindMax(int[] array)
    {
        int max = array[0];
        foreach (int value in array)
        {
            if (value > max)
            {
                max = value;
            }
        }
        return max;
    }
    
    static int FindMin(int[] array)
    {
        int min = array[0];
        foreach (int value in array)
        {
            if (value < min)
            {
                min = value;
            }
        }
        return min;
    }
    
    static double FindAverage(int[] array)
    {
        int sum = 0;
        foreach (int value in array)
        {
            sum += value;
        }
        return (double)sum / array.Length;
    }
}