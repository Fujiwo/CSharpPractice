using System;

abstract class Employee
{
    public string Name { get; set; }
    public int Id { get; set; }
    public decimal BaseSalary { get; set; }
    
    public Employee(string name, int id, decimal baseSalary)
    {
        Name = name;
        Id = id;
        BaseSalary = baseSalary;
    }
    
    public abstract decimal CalculateSalary();
    
    public virtual void DisplayInfo()
    {
        Console.WriteLine($"ID: {Id}, 名前: {Name}");
        Console.WriteLine($"給与: {CalculateSalary():C}");
    }
}

class FullTimeEmployee : Employee
{
    public decimal Bonus { get; set; }
    
    public FullTimeEmployee(string name, int id, decimal baseSalary, decimal bonus) 
        : base(name, id, baseSalary)
    {
        Bonus = bonus;
    }
    
    public override decimal CalculateSalary()
    {
        return BaseSalary + Bonus;
    }
    
    public override void DisplayInfo()
    {
        Console.WriteLine($"正社員 - ID: {Id}, 名前: {Name}");
        Console.WriteLine($"基本給: {BaseSalary:C}, 賞与: {Bonus:C}");
        Console.WriteLine($"総給与: {CalculateSalary():C}");
    }
}

class PartTimeEmployee : Employee
{
    public decimal HourlyRate { get; set; }
    public int WorkedHours { get; set; }
    
    public PartTimeEmployee(string name, int id, decimal hourlyRate, int workedHours) 
        : base(name, id, 0)
    {
        HourlyRate = hourlyRate;
        WorkedHours = workedHours;
    }
    
    public override decimal CalculateSalary()
    {
        return HourlyRate * WorkedHours;
    }
    
    public override void DisplayInfo()
    {
        Console.WriteLine($"パートタイム - ID: {Id}, 名前: {Name}");
        Console.WriteLine($"時給: {HourlyRate:C}, 勤務時間: {WorkedHours}時間");
        Console.WriteLine($"総給与: {CalculateSalary():C}");
    }
}

class Program
{
    static void Main()
    {
        // 問題 8-2: 従業員管理システム
        Employee[] employees = {
            new FullTimeEmployee("田中太郎", 1001, 300000, 50000),
            new PartTimeEmployee("佐藤花子", 2001, 1200, 80),
            new FullTimeEmployee("鈴木次郎", 1002, 350000, 60000),
            new PartTimeEmployee("山田三郎", 2002, 1000, 120)
        };
        
        Console.WriteLine("従業員給与一覧:");
        Console.WriteLine("================");
        
        foreach (Employee employee in employees)
        {
            employee.DisplayInfo();
            Console.WriteLine("-------------------");
        }
    }
}