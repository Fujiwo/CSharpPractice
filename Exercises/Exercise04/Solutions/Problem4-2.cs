using System;

class Program
{
    static void Main()
    {
        // 問題 4-2: 九九の表を出力
        for (int i = 1; i <= 9; i++)
        {
            Console.WriteLine($"--- {i}の段 ---");
            for (int j = 1; j <= 9; j++)
            {
                int result = i * j;
                Console.WriteLine($"{i} × {j} = {result}");
            }
            Console.WriteLine(); // 空行
        }
    }
}