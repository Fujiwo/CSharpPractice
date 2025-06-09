using System;
using System.Collections.Generic;
using System.Linq;

abstract class Animal
{
    public string Name { get; set; }
    public int Age { get; set; }
    
    public Animal(string name, int age)
    {
        Name = name;
        Age = age;
    }
    
    public virtual void DisplayInfo()
    {
        Console.WriteLine($"名前: {Name}, 年齢: {Age}歳");
    }
    
    public abstract string MakeSound();
    
    public virtual string GetAnimalType()
    {
        return this.GetType().Name;
    }
}

class Dog : Animal
{
    public string Breed { get; set; }
    
    public Dog(string name, int age, string breed) : base(name, age)
    {
        Breed = breed;
    }
    
    public override string MakeSound()
    {
        return "ワンワン！";
    }
    
    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"種類: 犬（{Breed}）");
    }
}

class Cat : Animal
{
    public string Color { get; set; }
    
    public Cat(string name, int age, string color) : base(name, age)
    {
        Color = color;
    }
    
    public override string MakeSound()
    {
        return "ニャーニャー！";
    }
    
    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"種類: 猫（{Color}）");
    }
}

class Bird : Animal
{
    public string Species { get; set; }
    public bool CanFly { get; set; }
    
    public Bird(string name, int age, string species, bool canFly) : base(name, age)
    {
        Species = species;
        CanFly = canFly;
    }
    
    public override string MakeSound()
    {
        return "チュンチュン！";
    }
    
    public override void DisplayInfo()
    {
        base.DisplayInfo();
        string flyStatus = CanFly ? "飛べる" : "飛べない";
        Console.WriteLine($"種類: 鳥（{Species}・{flyStatus}）");
    }
}

class AnimalShelter
{
    private List<Animal> animals;
    
    public AnimalShelter()
    {
        animals = new List<Animal>();
    }
    
    public void AddAnimal(Animal animal)
    {
        animals.Add(animal);
        Console.WriteLine($"{animal.GetAnimalType()} の '{animal.Name}' をシェルターに追加しました。");
    }
    
    public List<Animal> GetAllAnimals()
    {
        return new List<Animal>(animals);
    }
    
    public List<Animal> GetAnimalsByType<T>() where T : Animal
    {
        return animals.OfType<T>().Cast<Animal>().ToList();
    }
    
    public List<Animal> GetAnimalsByType(Type animalType)
    {
        return animals.Where(a => a.GetType() == animalType).ToList();
    }
    
    public void DisplayAllAnimals()
    {
        Console.WriteLine("\n=== シェルターの全動物 ===");
        if (animals.Count == 0)
        {
            Console.WriteLine("シェルターに動物はいません。");
            return;
        }
        
        for (int i = 0; i < animals.Count; i++)
        {
            Console.WriteLine($"\n動物 #{i + 1}:");
            animals[i].DisplayInfo();
            Console.WriteLine($"鳴き声: {animals[i].MakeSound()}");
            Console.WriteLine("-------------------");
        }
    }
    
    public void DisplayAnimalsByType(Type animalType)
    {
        List<Animal> filteredAnimals = GetAnimalsByType(animalType);
        Console.WriteLine($"\n=== {animalType.Name} 一覧 ===");
        
        if (filteredAnimals.Count == 0)
        {
            Console.WriteLine($"{animalType.Name} はいません。");
            return;
        }
        
        foreach (Animal animal in filteredAnimals)
        {
            animal.DisplayInfo();
            Console.WriteLine($"鳴き声: {animal.MakeSound()}");
            Console.WriteLine("-------------------");
        }
    }
    
    public void PlaySounds()
    {
        Console.WriteLine("\n=== 動物たちの大合唱 ===");
        foreach (Animal animal in animals)
        {
            Console.WriteLine($"{animal.Name}: {animal.MakeSound()}");
        }
    }
    
    public int GetAnimalCount()
    {
        return animals.Count;
    }
    
    public void GetShelterStatistics()
    {
        Console.WriteLine("\n=== シェルター統計 ===");
        Console.WriteLine($"総動物数: {animals.Count}匹");
        
        int dogCount = GetAnimalsByType(typeof(Dog)).Count;
        int catCount = GetAnimalsByType(typeof(Cat)).Count;
        int birdCount = GetAnimalsByType(typeof(Bird)).Count;
        
        Console.WriteLine($"犬: {dogCount}匹");
        Console.WriteLine($"猫: {catCount}匹");
        Console.WriteLine($"鳥: {birdCount}羽");
    }
}

class Program
{
    static void Main()
    {
        // 問題 7-8: 動物の分類システム
        AnimalShelter shelter = new AnimalShelter();
        
        Console.WriteLine("動物シェルター管理システム");
        
        // 動物をシェルターに追加
        shelter.AddAnimal(new Dog("ポチ", 3, "柴犬"));
        shelter.AddAnimal(new Dog("ハチ", 5, "秋田犬"));
        shelter.AddAnimal(new Cat("タマ", 2, "三毛猫"));
        shelter.AddAnimal(new Cat("クロ", 4, "黒猫"));
        shelter.AddAnimal(new Bird("ピーちゃん", 1, "セキセイインコ", true));
        shelter.AddAnimal(new Bird("ペンペン", 6, "ペンギン", false));
        
        // 全動物表示
        shelter.DisplayAllAnimals();
        
        // 種類別表示
        shelter.DisplayAnimalsByType(typeof(Dog));
        shelter.DisplayAnimalsByType(typeof(Cat));
        
        // 動物たちの鳴き声
        shelter.PlaySounds();
        
        // 統計情報
        shelter.GetShelterStatistics();
    }
}