using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // 問題 6-8: 数学計算ユーティリティメソッド群
        Console.WriteLine("数学計算ユーティリティのテスト");
        
        // 最大公約数のテスト
        int a = 48, b = 18;
        Console.WriteLine($"{a}と{b}の最大公約数: {GetGCD(a, b)}");
        
        // 最小公倍数のテスト
        Console.WriteLine($"{a}と{b}の最小公倍数: {GetLCM(a, b)}");
        
        // 素数判定のテスト
        int[] testNumbers = {2, 7, 15, 17, 25, 29};
        Console.WriteLine("\n素数判定:");
        foreach (int num in testNumbers)
        {
            Console.WriteLine($"{num}: {(IsPrime(num) ? "素数" : "合成数")}");
        }
        
        // 指定範囲の素数を取得
        int start = 10, end = 30;
        int[] primes = GetPrimesInRange(start, end);
        Console.WriteLine($"\n{start}から{end}までの素数:");
        Console.Write("[");
        for (int i = 0; i < primes.Length; i++)
        {
            Console.Write(primes[i]);
            if (i < primes.Length - 1) Console.Write(", ");
        }
        Console.WriteLine("]");
        
        // 完全数判定のテスト
        int[] perfectTestNumbers = {6, 28, 12, 496};
        Console.WriteLine("\n完全数判定:");
        foreach (int num in perfectTestNumbers)
        {
            Console.WriteLine($"{num}: {(IsPerfectNumber(num) ? "完全数" : "完全数ではない")}");
        }
    }
    
    static int GetGCD(int a, int b)
    {
        // ユークリッドの互除法
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return Math.Abs(a);
    }
    
    static int GetLCM(int a, int b)
    {
        // 最小公倍数 = (a * b) / 最大公約数
        return Math.Abs(a * b) / GetGCD(a, b);
    }
    
    static bool IsPrime(int number)
    {
        if (number <= 1)
        {
            return false;
        }
        if (number <= 3)
        {
            return true;
        }
        if (number % 2 == 0 || number % 3 == 0)
        {
            return false;
        }
        
        // 5から√nまでの奇数で割り切れるかチェック
        for (int i = 5; i * i <= number; i += 6)
        {
            if (number % i == 0 || number % (i + 2) == 0)
            {
                return false;
            }
        }
        
        return true;
    }
    
    static int[] GetPrimesInRange(int start, int end)
    {
        List<int> primes = new List<int>();
        
        for (int i = start; i <= end; i++)
        {
            if (IsPrime(i))
            {
                primes.Add(i);
            }
        }
        
        return primes.ToArray();
    }
    
    static bool IsPerfectNumber(int number)
    {
        if (number <= 1)
        {
            return false;
        }
        
        int sum = 1; // 1は常に約数
        
        // 2から√numberまでの約数を見つける
        for (int i = 2; i * i <= number; i++)
        {
            if (number % i == 0)
            {
                sum += i;
                // i * i != numberの場合、number/iも約数
                if (i * i != number)
                {
                    sum += number / i;
                }
            }
        }
        
        return sum == number;
    }
}