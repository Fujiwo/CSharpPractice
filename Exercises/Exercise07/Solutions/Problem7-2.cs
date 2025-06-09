using System;

class BankAccount
{
    public string AccountNumber { get; private set; }
    public decimal Balance { get; private set; }
    public string Owner { get; private set; }
    
    public BankAccount(string accountNumber, string owner, decimal initialBalance = 0)
    {
        AccountNumber = accountNumber;
        Owner = owner;
        Balance = initialBalance;
    }
    
    public void Deposit(decimal amount)
    {
        if (amount > 0)
        {
            Balance += amount;
            Console.WriteLine($"{amount:C}を預金しました。残高: {Balance:C}");
        }
        else
        {
            Console.WriteLine("預金額は0より大きい値を入力してください。");
        }
    }
    
    public void Withdraw(decimal amount)
    {
        if (amount > 0)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                Console.WriteLine($"{amount:C}を引き出しました。残高: {Balance:C}");
            }
            else
            {
                Console.WriteLine("残高不足です。引き出しできません。");
            }
        }
        else
        {
            Console.WriteLine("引き出し額は0より大きい値を入力してください。");
        }
    }
    
    public decimal GetBalance()
    {
        Console.WriteLine($"現在の残高: {Balance:C}");
        return Balance;
    }
    
    public void DisplayAccountInfo()
    {
        Console.WriteLine($"口座番号: {AccountNumber}");
        Console.WriteLine($"口座名義: {Owner}"); 
        Console.WriteLine($"残高: {Balance:C}");
        Console.WriteLine("-------------------");
    }
}

class Program
{
    static void Main()
    {
        // 問題 7-2: 銀行口座を管理するクラス
        BankAccount account = new BankAccount("12345-678", "田中太郎", 10000);
        
        Console.WriteLine("口座情報:");
        account.DisplayAccountInfo();
        
        Console.WriteLine("\n取引開始:");
        account.Deposit(5000);
        account.Withdraw(3000);
        account.Withdraw(15000); // 残高不足のテスト
        account.GetBalance();
    }
}