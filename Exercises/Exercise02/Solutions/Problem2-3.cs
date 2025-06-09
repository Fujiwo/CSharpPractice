using System;

class Program
{
    static void Main()
    {
        // 問題 2-3: 文字列の操作
        string firstName = "太郎";
        string lastName = "田中";
        
        // 文字列の連結
        string fullName = lastName + " " + firstName;
        
        // フルネームの文字数
        int nameLength = fullName.Length;
        
        // フルネームを大文字に変換
        string upperCaseName = fullName.ToUpper();
        
        Console.WriteLine($"フルネーム: {fullName}");
        Console.WriteLine($"文字数: {nameLength}文字");
        Console.WriteLine($"大文字: {upperCaseName}");
    }
}