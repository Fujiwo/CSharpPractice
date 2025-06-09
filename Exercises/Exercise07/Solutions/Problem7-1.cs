using System;

class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
    public double Grade { get; set; }
    
    public Student(string name, int age, double grade)
    {
        Name = name;
        Age = age;
        Grade = grade;
    }
    
    public void DisplayInfo()
    {
        Console.WriteLine($"名前: {Name}");
        Console.WriteLine($"年齢: {Age}歳");
        Console.WriteLine($"成績: {Grade}点");
        Console.WriteLine("-------------------");
    }
}

class Program
{
    static void Main()
    {
        // 問題 7-1: 学生情報を管理するクラス
        Student student1 = new Student("田中太郎", 20, 85.5);
        Student student2 = new Student("佐藤花子", 19, 92.0);
        Student student3 = new Student("鈴木次郎", 21, 78.3);
        
        Console.WriteLine("学生情報一覧:");
        student1.DisplayInfo();
        student2.DisplayInfo();
        student3.DisplayInfo();
    }
}