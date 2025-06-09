using System;

class Program
{
    static void Main()
    {
        // 問題 3-1: 年齢を入力して年齢層を判定
        Console.Write("年齢を入力してください: ");
        int age = int.Parse(Console.ReadLine());
        
        if (age >= 0 && age <= 12)
        {
            Console.WriteLine("子供");
        }
        else if (age >= 13 && age <= 19)
        {
            Console.WriteLine("十代");
        }
        else if (age >= 20 && age <= 64)
        {
            Console.WriteLine("大人");
        }
        else if (age >= 65)
        {
            Console.WriteLine("シニア");
        }
        else
        {
            Console.WriteLine("無効な年齢です");
        }
    }
}