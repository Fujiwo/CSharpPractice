using System;

class Program
{
    static void Main()
    {
        // 問題 4-3: 0が入力されるまで数値の合計を計算
        int sum = 0;
        int count = 0;
        int input;
        
        Console.WriteLine("数値を入力してください（0で終了）:");
        
        while (true)
        {
            Console.Write("数値: ");
            input = int.Parse(Console.ReadLine());
            
            if (input == 0)
            {
                break;
            }
            
            sum += input;
            count++;
        }
        
        Console.WriteLine($"合計: {sum}");
        Console.WriteLine($"入力された数値の個数: {count}");
    }
}