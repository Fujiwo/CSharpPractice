using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // 問題 5-2: 文字列のリストをフィルタリング
        List<string> fruits = new List<string>
        {
            "りんご", "バナナ", "オレンジ", "ぶどう", "いちご"
        };
        
        Console.WriteLine("全ての果物:");
        foreach (string fruit in fruits)
        {
            Console.WriteLine(fruit);
        }
        
        Console.WriteLine("\n3文字以上の果物:");
        List<string> filteredFruits = new List<string>();
        
        foreach (string fruit in fruits)
        {
            if (fruit.Length >= 3)
            {
                filteredFruits.Add(fruit);
                Console.WriteLine(fruit);
            }
        }
        
        Console.WriteLine($"\n3文字以上の果物の個数: {filteredFruits.Count}");
    }
}