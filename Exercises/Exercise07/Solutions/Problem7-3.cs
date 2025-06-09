using System;

class Rectangle
{
    public double Width { get; set; }
    public double Height { get; set; }
    
    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }
    
    public double CalculateArea()
    {
        return Width * Height;
    }
    
    public void DisplayInfo()
    {
        Console.WriteLine($"長方形 - 幅: {Width}, 高さ: {Height}");
        Console.WriteLine($"面積: {CalculateArea()}");
    }
}

class Circle
{
    public double Radius { get; set; }
    
    public Circle(double radius)
    {
        Radius = radius;
    }
    
    public double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
    
    public void DisplayInfo()
    {
        Console.WriteLine($"円 - 半径: {Radius}");
        Console.WriteLine($"面積: {CalculateArea():F2}");
    }
}

class Program
{
    static void Main()
    {
        // 問題 7-3: 図形の面積を計算するクラス群
        Rectangle rectangle1 = new Rectangle(5.0, 3.0);
        Rectangle rectangle2 = new Rectangle(7.5, 4.2);
        
        Circle circle1 = new Circle(3.0);
        Circle circle2 = new Circle(5.5);
        
        Console.WriteLine("図形の面積計算:");
        Console.WriteLine("================");
        
        rectangle1.DisplayInfo();
        Console.WriteLine();
        
        rectangle2.DisplayInfo();
        Console.WriteLine();
        
        circle1.DisplayInfo();
        Console.WriteLine();
        
        circle2.DisplayInfo();
    }
}