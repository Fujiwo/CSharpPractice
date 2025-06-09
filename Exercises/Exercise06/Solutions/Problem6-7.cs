using System;

class Program
{
    static void Main()
    {
        // 問題 6-7: パスワード検証システム
        Console.WriteLine("パスワード検証システム");
        Console.WriteLine("パスワードを入力してください:");
        
        string password = Console.ReadLine();
        
        Console.WriteLine("\n=== パスワード検証結果 ===");
        
        // 各検証の実行
        bool lengthOk = CheckPasswordLength(password);
        Console.WriteLine($"長さチェック (8文字以上): {(lengthOk ? "OK" : "NG")}");
        
        bool hasUpper = HasUpperCase(password);
        Console.WriteLine($"大文字チェック: {(hasUpper ? "OK" : "NG")}");
        
        bool hasLower = HasLowerCase(password);
        Console.WriteLine($"小文字チェック: {(hasLower ? "OK" : "NG")}");
        
        bool hasDigit = HasDigit(password);
        Console.WriteLine($"数字チェック: {(hasDigit ? "OK" : "NG")}");
        
        bool hasSpecial = HasSpecialCharacter(password);
        Console.WriteLine($"特殊文字チェック: {(hasSpecial ? "OK" : "NG")}");
        
        // 総合判定
        string strength = GetPasswordStrength(password);
        Console.WriteLine($"\nパスワード強度: {strength}");
    }
    
    static bool CheckPasswordLength(string password)
    {
        return password != null && password.Length >= 8;
    }
    
    static bool HasUpperCase(string password)
    {
        if (password == null) return false;
        
        foreach (char c in password)
        {
            if (char.IsUpper(c))
            {
                return true;
            }
        }
        return false;
    }
    
    static bool HasLowerCase(string password)
    {
        if (password == null) return false;
        
        foreach (char c in password)
        {
            if (char.IsLower(c))
            {
                return true;
            }
        }
        return false;
    }
    
    static bool HasDigit(string password)
    {
        if (password == null) return false;
        
        foreach (char c in password)
        {
            if (char.IsDigit(c))
            {
                return true;
            }
        }
        return false;
    }
    
    static bool HasSpecialCharacter(string password)
    {
        if (password == null) return false;
        
        string specialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?";
        
        foreach (char c in password)
        {
            if (specialChars.Contains(c.ToString()))
            {
                return true;
            }
        }
        return false;
    }
    
    static string GetPasswordStrength(string password)
    {
        if (password == null) return "無効";
        
        int score = 0;
        
        if (CheckPasswordLength(password)) score++;
        if (HasUpperCase(password)) score++;
        if (HasLowerCase(password)) score++;
        if (HasDigit(password)) score++;
        if (HasSpecialCharacter(password)) score++;
        
        switch (score)
        {
            case 0:
            case 1:
                return "非常に弱い";
            case 2:
                return "弱い";
            case 3:
                return "普通";
            case 4:
                return "強い";
            case 5:
                return "非常に強い";
            default:
                return "不明";
        }
    }
}