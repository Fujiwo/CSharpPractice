using System;

class Program
{
    static void Main()
    {
        // 問題 2-1: 異なるデータ型の変数を宣言し、値を代入して出力
        int age = 25;
        string name = "田中太郎";
        double height = 175.5;
        bool isStudent = true;
        
        Console.WriteLine($"名前: {name}");
        Console.WriteLine($"年齢: {age}歳");
        Console.WriteLine($"身長: {height}cm");
        Console.WriteLine($"学生: {isStudent}");
    }
}