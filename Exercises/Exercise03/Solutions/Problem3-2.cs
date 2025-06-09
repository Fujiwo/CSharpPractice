using System;

class Program
{
    static void Main()
    {
        // 問題 3-2: 点数を入力して成績評価
        Console.Write("点数を入力してください（0-100）: ");
        int score = int.Parse(Console.ReadLine());
        
        string grade = score switch
        {
            >= 90 and <= 100 => "A",
            >= 80 and < 90 => "B",
            >= 70 and < 80 => "C",
            >= 60 and < 70 => "D",
            >= 0 and < 60 => "F",
            _ => "無効な点数"
        };
        
        Console.WriteLine($"成績: {grade}");
    }
}