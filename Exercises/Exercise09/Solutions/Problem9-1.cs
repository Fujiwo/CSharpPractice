using System;

interface IDrawable
{
    void Draw();
}

interface IResizable
{
    void Resize(double factor);
}

abstract class Shape
{
    public string Name { get; protected set; }
    
    public abstract double Area();
}

class Rectangle : Shape, IDrawable, IResizable
{
    public double Width { get; private set; }
    public double Height { get; private set; }
    
    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
        Name = "長方形";
    }
    
    public override double Area()
    {
        return Width * Height;
    }
    
    public void Draw()
    {
        Console.WriteLine($"{Name}を描画しています (幅: {Width}, 高さ: {Height})");
        Console.WriteLine("┌─────────┐");
        Console.WriteLine("│         │");
        Console.WriteLine("│         │");
        Console.WriteLine("└─────────┘");
    }
    
    public void Resize(double factor)
    {
        Width *= factor;
        Height *= factor;
        Console.WriteLine($"{Name}を{factor}倍にリサイズしました");
    }
}

class Circle : Shape, IDrawable, IResizable
{
    public double Radius { get; private set; }
    
    public Circle(double radius)
    {
        Radius = radius;
        Name = "円";
    }
    
    public override double Area()
    {
        return Math.PI * Radius * Radius;
    }
    
    public void Draw()
    {
        Console.WriteLine($"{Name}を描画しています (半径: {Radius})");
        Console.WriteLine("   ●●●   ");
        Console.WriteLine(" ●     ● ");
        Console.WriteLine("●       ●");
        Console.WriteLine(" ●     ● ");
        Console.WriteLine("   ●●●   ");
    }
    
    public void Resize(double factor)
    {
        Radius *= factor;
        Console.WriteLine($"{Name}を{factor}倍にリサイズしました");
    }
}

class Program
{
    static void Main()
    {
        // 問題 9-1: 図形描画システム
        IDrawable[] drawables = {
            new Rectangle(5.0, 3.0),
            new Circle(2.5)
        };
        
        Console.WriteLine("図形描画システム");
        Console.WriteLine("================");
        
        foreach (IDrawable drawable in drawables)
        {
            Shape shape = (Shape)drawable;
            Console.WriteLine($"\n{shape.Name} - 面積: {shape.Area():F2}");
            drawable.Draw();
            
            if (drawable is IResizable resizable)
            {
                resizable.Resize(1.5);
                Console.WriteLine($"リサイズ後の面積: {shape.Area():F2}");
            }
            
            Console.WriteLine("-------------------");
        }
    }
}