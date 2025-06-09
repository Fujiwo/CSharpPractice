using System;

class Program
{
    static void Main()
    {
        // 問題 5-3: 二次元配列を使用した学生成績表
        int[,] scores = {
            { 85, 92, 78 }, // 学生1: 国語, 数学, 英語
            { 76, 88, 91 }, // 学生2: 国語, 数学, 英語
            { 92, 85, 89 }  // 学生3: 国語, 数学, 英語
        };
        
        string[] subjects = { "国語", "数学", "英語" };
        
        Console.WriteLine("学生成績表");
        Console.WriteLine("学生\\科目\t国語\t数学\t英語\t平均");
        Console.WriteLine("----------------------------------------");
        
        // 各学生の平均点を計算
        for (int student = 0; student < 3; student++)
        {
            int total = 0;
            Console.Write($"学生{student + 1}\t\t");
            
            for (int subject = 0; subject < 3; subject++)
            {
                Console.Write($"{scores[student, subject]}\t");
                total += scores[student, subject];
            }
            
            double average = (double)total / 3;
            Console.WriteLine($"{average:F1}");
        }
        
        Console.WriteLine("----------------------------------------");
        
        // 各科目の平均点を計算
        Console.Write("科目平均\t\t");
        for (int subject = 0; subject < 3; subject++)
        {
            int total = 0;
            for (int student = 0; student < 3; student++)
            {
                total += scores[student, subject];
            }
            double average = (double)total / 3;
            Console.Write($"{average:F1}\t");
        }
        Console.WriteLine();
    }
}