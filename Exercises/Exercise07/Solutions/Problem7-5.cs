using System;

abstract class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    
    public Employee(int id, string name, string department)
    {
        Id = id;
        Name = name;
        Department = department;
    }
    
    public virtual void DisplayInfo()
    {
        Console.WriteLine($"ID: {Id}, 名前: {Name}, 部署: {Department}");
    }
    
    public abstract decimal CalculatePay();
}

class FullTimeEmployee : Employee
{
    public decimal Salary { get; set; }
    
    public FullTimeEmployee(int id, string name, string department, decimal salary) 
        : base(id, name, department)
    {
        Salary = salary;
    }
    
    public override decimal CalculatePay()
    {
        return Salary;
    }
    
    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"雇用形態: 正社員, 月給: {Salary:C}");
    }
}

class PartTimeEmployee : Employee
{
    public decimal HourlyRate { get; set; }
    public int HoursWorked { get; set; }
    
    public PartTimeEmployee(int id, string name, string department, decimal hourlyRate, int hoursWorked) 
        : base(id, name, department)
    {
        HourlyRate = hourlyRate;
        HoursWorked = hoursWorked;
    }
    
    public override decimal CalculatePay()
    {
        return HourlyRate * HoursWorked;
    }
    
    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"雇用形態: パートタイム, 時給: {HourlyRate:C}, 労働時間: {HoursWorked}時間");
    }
}

class Program
{
    static void Main()
    {
        // 問題 7-5: 従業員管理システム
        Console.WriteLine("従業員管理システム");
        
        // 正社員を作成
        FullTimeEmployee fullTime1 = new FullTimeEmployee(1, "田中太郎", "開発部", 300000);
        FullTimeEmployee fullTime2 = new FullTimeEmployee(2, "佐藤花子", "営業部", 280000);
        
        // パートタイム従業員を作成
        PartTimeEmployee partTime1 = new PartTimeEmployee(3, "鈴木次郎", "総務部", 1200, 80);
        PartTimeEmployee partTime2 = new PartTimeEmployee(4, "高橋美咲", "開発部", 1500, 120);
        
        // 従業員配列を作成
        Employee[] employees = { fullTime1, fullTime2, partTime1, partTime2 };
        
        Console.WriteLine("\n=== 従業員情報と給与計算 ===");
        decimal totalPayroll = 0;
        
        foreach (Employee employee in employees)
        {
            employee.DisplayInfo();
            decimal pay = employee.CalculatePay();
            Console.WriteLine($"支給額: {pay:C}");
            Console.WriteLine("-------------------");
            totalPayroll += pay;
        }
        
        Console.WriteLine($"総給与支給額: {totalPayroll:C}");
    }
}