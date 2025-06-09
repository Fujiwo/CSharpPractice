using System;

class Animal
{
    public string Name { get; set; }
    public int Age { get; set; }
    
    public Animal(string name, int age)
    {
        Name = name;
        Age = age;
    }
    
    public virtual void MakeSound()
    {
        Console.WriteLine($"{Name}が音を出しています");
    }
    
    public virtual void Move()
    {
        Console.WriteLine($"{Name}が移動しています");
    }
    
    public void DisplayInfo()
    {
        Console.WriteLine($"名前: {Name}, 年齢: {Age}歳");
    }
}

class Dog : Animal
{
    public Dog(string name, int age) : base(name, age)
    {
    }
    
    public override void MakeSound()
    {
        Console.WriteLine($"{Name}が「ワンワン」と鳴いています");
    }
    
    public override void Move()
    {
        Console.WriteLine($"{Name}が走っています");
    }
}

class Cat : Animal
{
    public Cat(string name, int age) : base(name, age)
    {
    }
    
    public override void MakeSound()
    {
        Console.WriteLine($"{Name}が「ニャーニャー」と鳴いています");
    }
    
    public override void Move()
    {
        Console.WriteLine($"{Name}がしなやかに歩いています");
    }
}

class Program
{
    static void Main()
    {
        // 問題 8-1: 動物の継承階層
        Animal[] animals = {
            new Dog("ポチ", 3),
            new Cat("ミケ", 2),
            new Dog("ハチ", 5)
        };
        
        Console.WriteLine("動物たちの行動:");
        foreach (Animal animal in animals)
        {
            animal.DisplayInfo();
            animal.MakeSound();
            animal.Move();
            Console.WriteLine("-------------------");
        }
    }
}