using System;
using System.Collections.Generic;
using System.Linq;

class Item
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    
    public Item(string name, decimal price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }
    
    public decimal GetSubtotal()
    {
        return Price * Quantity;
    }
    
    public void DisplayInfo()
    {
        Console.WriteLine($"{Name} - 単価: {Price:C} × {Quantity}個 = {GetSubtotal():C}");
    }
}

class ShoppingCart
{
    private List<Item> items;
    private decimal discountPercentage;
    
    public ShoppingCart()
    {
        items = new List<Item>();
        discountPercentage = 0;
    }
    
    public void AddItem(Item item)
    {
        // 既に同じ名前のアイテムがあるかチェック
        Item existingItem = items.FirstOrDefault(i => i.Name == item.Name);
        if (existingItem != null)
        {
            existingItem.Quantity += item.Quantity;
            Console.WriteLine($"'{item.Name}' の数量を {existingItem.Quantity} 個に更新しました。");
        }
        else
        {
            items.Add(item);
            Console.WriteLine($"'{item.Name}' をカートに追加しました。");
        }
    }
    
    public bool RemoveItem(string itemName)
    {
        Item item = items.FirstOrDefault(i => i.Name == itemName);
        if (item != null)
        {
            items.Remove(item);
            Console.WriteLine($"'{itemName}' をカートから削除しました。");
            return true;
        }
        else
        {
            Console.WriteLine($"'{itemName}' はカートにありません。");
            return false;
        }
    }
    
    public bool UpdateQuantity(string itemName, int newQuantity)
    {
        Item item = items.FirstOrDefault(i => i.Name == itemName);
        if (item != null)
        {
            if (newQuantity <= 0)
            {
                return RemoveItem(itemName);
            }
            else
            {
                item.Quantity = newQuantity;
                Console.WriteLine($"'{itemName}' の数量を {newQuantity} 個に更新しました。");
                return true;
            }
        }
        else
        {
            Console.WriteLine($"'{itemName}' はカートにありません。");
            return false;
        }
    }
    
    public decimal CalculateSubtotal()
    {
        return items.Sum(item => item.GetSubtotal());
    }
    
    public decimal CalculateTotal()
    {
        decimal subtotal = CalculateSubtotal();
        decimal discountAmount = subtotal * (discountPercentage / 100);
        return subtotal - discountAmount;
    }
    
    public void ApplyDiscount(decimal percentage)
    {
        if (percentage >= 0 && percentage <= 100)
        {
            discountPercentage = percentage;
            Console.WriteLine($"{percentage}% の割引を適用しました。");
        }
        else
        {
            Console.WriteLine("無効な割引率です（0-100%の範囲で入力してください）。");
        }
    }
    
    public void DisplayCart()
    {
        Console.WriteLine("\n=== ショッピングカート ===");
        if (items.Count == 0)
        {
            Console.WriteLine("カートは空です。");
            return;
        }
        
        foreach (Item item in items)
        {
            item.DisplayInfo();
        }
        
        decimal subtotal = CalculateSubtotal();
        Console.WriteLine($"\n小計: {subtotal:C}");
        
        if (discountPercentage > 0)
        {
            decimal discountAmount = subtotal * (discountPercentage / 100);
            Console.WriteLine($"割引 ({discountPercentage}%): -{discountAmount:C}");
        }
        
        Console.WriteLine($"合計: {CalculateTotal():C}");
        Console.WriteLine("========================");
    }
    
    public int GetItemCount()
    {
        return items.Count;
    }
}

class Program
{
    static void Main()
    {
        // 問題 7-7: ショッピングカートシステム
        ShoppingCart cart = new ShoppingCart();
        
        Console.WriteLine("ショッピングカートシステムのテスト");
        
        // アイテムを追加
        cart.AddItem(new Item("ノートPC", 80000, 1));
        cart.AddItem(new Item("マウス", 2500, 2));
        cart.AddItem(new Item("キーボード", 5000, 1));
        cart.AddItem(new Item("マウス", 2500, 1)); // 既存アイテムの数量追加
        
        // カート内容表示
        cart.DisplayCart();
        
        // 数量更新
        Console.WriteLine("\n=== 数量更新 ===");
        cart.UpdateQuantity("マウス", 2);
        cart.UpdateQuantity("キーボード", 2);
        
        // 10%割引を適用
        Console.WriteLine("\n=== 割引適用 ===");
        cart.ApplyDiscount(10);
        cart.DisplayCart();
        
        // アイテム削除
        Console.WriteLine("\n=== アイテム削除 ===");
        cart.RemoveItem("キーボード");
        
        // 20%割引に変更
        Console.WriteLine("\n=== 割引変更 ===");
        cart.ApplyDiscount(20);
        cart.DisplayCart();
        
        // 新しいアイテム追加
        Console.WriteLine("\n=== 追加購入 ===");
        cart.AddItem(new Item("モニター", 25000, 1));
        cart.DisplayCart();
    }
}