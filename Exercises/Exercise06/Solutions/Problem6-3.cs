using System;

class Program
{
    static void Main()
    {
        // 問題 6-3: 文字列の操作を行うメソッド
        string testString1 = "こんにちは";
        string testString2 = "しんぶんし"; // 回文
        string testString3 = "Hello World";
        
        Console.WriteLine($"元の文字列: {testString1}");
        Console.WriteLine($"逆順: {ReverseString(testString1)}");
        Console.WriteLine($"回文かどうか: {IsPalindrome(testString1)}");
        Console.WriteLine();
        
        Console.WriteLine($"元の文字列: {testString2}");
        Console.WriteLine($"逆順: {ReverseString(testString2)}");
        Console.WriteLine($"回文かどうか: {IsPalindrome(testString2)}");
        Console.WriteLine();
        
        Console.WriteLine($"元の文字列: {testString3}");
        Console.WriteLine($"逆順: {ReverseString(testString3)}");
        Console.WriteLine($"母音の数: {CountVowels(testString3)}");
    }
    
    static string ReverseString(string input)
    {
        char[] chars = input.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }
    
    static bool IsPalindrome(string input)
    {
        string reversed = ReverseString(input);
        return input.Equals(reversed, StringComparison.OrdinalIgnoreCase);
    }
    
    static int CountVowels(string input)
    {
        int count = 0;
        string vowels = "aeiouAEIOU";
        
        foreach (char c in input)
        {
            if (vowels.Contains(c))
            {
                count++;
            }
        }
        
        return count;
    }
}