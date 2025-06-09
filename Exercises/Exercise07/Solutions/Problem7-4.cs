using System;
using System.Collections.Generic;
using System.Linq;

class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    
    public Product(int id, string name, decimal price, int stock)
    {
        Id = id;
        Name = name;
        Price = price;
        Stock = stock;
    }
    
    public void DisplayInfo()
    {
        Console.WriteLine($"ID: {Id}, 商品名: {Name}, 価格: {Price:C}, 在庫: {Stock}個");
    }
}

class InventoryManager
{
    private List<Product> products;
    
    public InventoryManager()
    {
        products = new List<Product>();
    }
    
    public void AddProduct(Product product)
    {
        products.Add(product);
        Console.WriteLine($"商品 '{product.Name}' を追加しました。");
    }
    
    public void UpdateStock(int productId, int newStock)
    {
        Product product = FindProduct(productId);
        if (product != null)
        {
            product.Stock = newStock;
            Console.WriteLine($"商品ID {productId} の在庫を {newStock} 個に更新しました。");
        }
        else
        {
            Console.WriteLine($"商品ID {productId} が見つかりません。");
        }
    }
    
    public Product FindProduct(int productId)
    {
        return products.FirstOrDefault(p => p.Id == productId);
    }
    
    public List<Product> GetLowStockProducts()
    {
        return products.Where(p => p.Stock <= 5).ToList();
    }
    
    public void DisplayAllProducts()
    {
        Console.WriteLine("\n=== 全商品一覧 ===");
        foreach (Product product in products)
        {
            product.DisplayInfo();
        }
    }
    
    public void DisplayLowStockWarning()
    {
        List<Product> lowStockProducts = GetLowStockProducts();
        if (lowStockProducts.Count > 0)
        {
            Console.WriteLine("\n⚠️ 在庫警告（5個以下）:");
            foreach (Product product in lowStockProducts)
            {
                product.DisplayInfo();
            }
        }
        else
        {
            Console.WriteLine("\n在庫不足の商品はありません。");
        }
    }
}

class Program
{
    static void Main()
    {
        // 問題 7-4: 在庫管理システム
        InventoryManager manager = new InventoryManager();
        
        // 商品を追加
        manager.AddProduct(new Product(1, "ノートPC", 80000, 10));
        manager.AddProduct(new Product(2, "マウス", 2500, 3));
        manager.AddProduct(new Product(3, "キーボード", 5000, 8));
        manager.AddProduct(new Product(4, "モニター", 25000, 2));
        
        // 全商品表示
        manager.DisplayAllProducts();
        
        // 在庫更新
        Console.WriteLine("\n=== 在庫更新 ===");
        manager.UpdateStock(2, 15);
        manager.UpdateStock(4, 1);
        
        // 商品検索
        Console.WriteLine("\n=== 商品検索 ===");
        Product found = manager.FindProduct(3);
        if (found != null)
        {
            Console.WriteLine("検索結果:");
            found.DisplayInfo();
        }
        
        // 在庫警告表示
        manager.DisplayLowStockWarning();
    }
}