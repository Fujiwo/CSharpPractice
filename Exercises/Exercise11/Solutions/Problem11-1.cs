using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
    public double Grade { get; set; }
    public string Subject { get; set; }
    
    public Student(string name, int age, double grade, string subject)
    {
        Name = name;
        Age = age;
        Grade = grade;
        Subject = subject;
    }
    
    public override string ToString()
    {
        return $"{Name} ({Age}歳) - {Subject}: {Grade}点";
    }
}

class Program
{
    static void Main()
    {
        // 問題 11-1: 学生データの管理とクエリ
        List<Student> students = CreateStudentData();
        
        Console.WriteLine("学生データ管理システム");
        Console.WriteLine("====================");
        
        // 全学生データの表示
        Console.WriteLine("\n=== 全学生データ ===");
        foreach (var student in students)
        {
            Console.WriteLine(student);
        }
        
        // クエリ1: 成績が80点以上の学生
        Console.WriteLine("\n=== 成績80点以上の学生 ===");
        var highPerformers = students.Where(s => s.Grade >= 80);
        foreach (var student in highPerformers)
        {
            Console.WriteLine(student);
        }
        
        // クエリ2: 年齢でグループ化
        Console.WriteLine("\n=== 年齢別グループ ===");
        var ageGroups = students.GroupBy(s => s.Age);
        foreach (var group in ageGroups)
        {
            Console.WriteLine($"{group.Key}歳: {group.Count()}人");
            foreach (var student in group)
            {
                Console.WriteLine($"  - {student.Name} ({student.Subject}: {student.Grade}点)");
            }
        }
        
        // クエリ3: 科目別の平均成績
        Console.WriteLine("\n=== 科目別平均成績 ===");
        var subjectAverages = students
            .GroupBy(s => s.Subject)
            .Select(g => new 
            {
                Subject = g.Key,
                Average = g.Average(s => s.Grade),
                Count = g.Count()
            });
        
        foreach (var avg in subjectAverages)
        {
            Console.WriteLine($"{avg.Subject}: 平均{avg.Average:F1}点 ({avg.Count}人)");
        }
        
        // クエリ4: 名前に特定の文字が含まれる学生
        Console.WriteLine("\n=== 名前に「田」が含まれる学生 ===");
        var nameFilter = students.Where(s => s.Name.Contains("田"));
        foreach (var student in nameFilter)
        {
            Console.WriteLine(student);
        }
        
        // 統計情報
        Console.WriteLine("\n=== 統計情報 ===");
        Console.WriteLine($"総学生数: {students.Count}人");
        Console.WriteLine($"最高成績: {students.Max(s => s.Grade)}点");
        Console.WriteLine($"最低成績: {students.Min(s => s.Grade)}点");
        Console.WriteLine($"平均成績: {students.Average(s => s.Grade):F1}点");
        
        // 成績上位3名
        Console.WriteLine("\n=== 成績上位3名 ===");
        var top3 = students.OrderByDescending(s => s.Grade).Take(3);
        int rank = 1;
        foreach (var student in top3)
        {
            Console.WriteLine($"{rank}位: {student}");
            rank++;
        }
    }
    
    static List<Student> CreateStudentData()
    {
        return new List<Student>
        {
            new Student("田中太郎", 20, 85.5, "数学"),
            new Student("佐藤花子", 19, 92.0, "英語"),
            new Student("鈴木次郎", 21, 78.3, "数学"),
            new Student("田村美咲", 20, 88.7, "国語"),
            new Student("山田健太", 19, 91.2, "英語"),
            new Student("中村真理", 22, 76.8, "国語"),
            new Student("小林誠", 20, 89.5, "数学"),
            new Student("加藤優子", 21, 94.1, "英語"),
            new Student("斎藤大輔", 19, 82.6, "国語"),
            new Student("田口愛", 20, 87.9, "数学")
        };
    }
}