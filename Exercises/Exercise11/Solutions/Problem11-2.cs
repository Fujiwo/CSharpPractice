using System;
using System.Collections.Generic;
using System.Linq;

class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    
    public Customer(int customerId, string name, string city)
    {
        CustomerId = customerId;
        Name = name;
        City = city;
    }
}

class Order
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    
    public Order(int orderId, int customerId, DateTime orderDate, decimal totalAmount)
    {
        OrderId = orderId;
        CustomerId = customerId;
        OrderDate = orderDate;
        TotalAmount = totalAmount;
    }
}

class OrderItem
{
    public int OrderId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    
    public OrderItem(int orderId, string productName, int quantity, decimal price)
    {
        OrderId = orderId;
        ProductName = productName;
        Quantity = quantity;
        Price = price;
    }
    
    public decimal TotalPrice => Quantity * Price;
}

class Program
{
    static void Main()
    {
        // 問題 11-2: 注文管理システムの複雑なLINQクエリ
        Console.WriteLine("注文管理システム");
        Console.WriteLine("================");
        
        var customers = CreateCustomerData();
        var orders = CreateOrderData();
        var orderItems = CreateOrderItemData();
        
        // クエリ1: 顧客別の注文総額
        Console.WriteLine("\n=== 顧客別注文総額 ===");
        var customerTotals = from customer in customers
                           join order in orders on customer.CustomerId equals order.CustomerId
                           group order by new { customer.CustomerId, customer.Name } into g
                           select new
                           {
                               CustomerId = g.Key.CustomerId,
                               CustomerName = g.Key.Name,
                               TotalAmount = g.Sum(o => o.TotalAmount),
                               OrderCount = g.Count()
                           };
        
        foreach (var customer in customerTotals.OrderByDescending(c => c.TotalAmount))
        {
            Console.WriteLine($"{customer.CustomerName}: {customer.TotalAmount:C} ({customer.OrderCount}回注文)");
        }
        
        // クエリ2: 月別の売上集計
        Console.WriteLine("\n=== 月別売上集計 ===");
        var monthlySales = orders
            .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
            .Select(g => new
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                TotalSales = g.Sum(o => o.TotalAmount),
                OrderCount = g.Count()
            })
            .OrderBy(m => m.Year).ThenBy(m => m.Month);
        
        foreach (var month in monthlySales)
        {
            Console.WriteLine($"{month.Year}年{month.Month}月: {month.TotalSales:C} ({month.OrderCount}件)");
        }
        
        // クエリ3: 最も売れている商品トップ5
        Console.WriteLine("\n=== 人気商品トップ5 ===");
        var topProducts = orderItems
            .GroupBy(item => item.ProductName)
            .Select(g => new
            {
                ProductName = g.Key,
                TotalQuantity = g.Sum(item => item.Quantity),
                TotalRevenue = g.Sum(item => item.TotalPrice),
                OrderCount = g.Count()
            })
            .OrderByDescending(p => p.TotalQuantity)
            .Take(5);
        
        int rank = 1;
        foreach (var product in topProducts)
        {
            Console.WriteLine($"{rank}位: {product.ProductName} - {product.TotalQuantity}個売上 " +
                           $"(売上額: {product.TotalRevenue:C}, {product.OrderCount}回注文)");
            rank++;
        }
        
        // クエリ4: 特定の都市（東京）の顧客の注文履歴
        Console.WriteLine("\n=== 東京の顧客の注文履歴 ===");
        var tokyoOrders = from customer in customers
                         join order in orders on customer.CustomerId equals order.CustomerId
                         where customer.City == "東京"
                         orderby order.OrderDate descending
                         select new
                         {
                             customer.Name,
                             order.OrderId,
                             order.OrderDate,
                             order.TotalAmount
                         };
        
        foreach (var order in tokyoOrders)
        {
            Console.WriteLine($"{order.Name}: 注文#{order.OrderId} ({order.OrderDate:yyyy/MM/dd}) - {order.TotalAmount:C}");
        }
        
        // 詳細分析
        Console.WriteLine("\n=== 詳細分析 ===");
        
        // 平均注文金額
        var avgOrderAmount = orders.Average(o => o.TotalAmount);
        Console.WriteLine($"平均注文金額: {avgOrderAmount:C}");
        
        // 最高額の注文
        var maxOrder = orders.OrderByDescending(o => o.TotalAmount).First();
        var maxOrderCustomer = customers.First(c => c.CustomerId == maxOrder.CustomerId);
        Console.WriteLine($"最高額注文: {maxOrder.TotalAmount:C} ({maxOrderCustomer.Name}様)");
        
        // 都市別の顧客数と平均注文金額
        Console.WriteLine("\n=== 都市別統計 ===");
        var cityStats = from customer in customers
                       join order in orders on customer.CustomerId equals order.CustomerId
                       group order by customer.City into g
                       select new
                       {
                           City = g.Key,
                           CustomerCount = g.Select(o => o.CustomerId).Distinct().Count(),
                           AverageOrderAmount = g.Average(o => o.TotalAmount),
                           TotalOrders = g.Count()
                       };
        
        foreach (var stat in cityStats.OrderByDescending(s => s.AverageOrderAmount))
        {
            Console.WriteLine($"{stat.City}: {stat.CustomerCount}人, 平均注文額{stat.AverageOrderAmount:C}, " +
                           $"総注文数{stat.TotalOrders}件");
        }
    }
    
    static List<Customer> CreateCustomerData()
    {
        return new List<Customer>
        {
            new Customer(1, "田中太郎", "東京"),
            new Customer(2, "佐藤花子", "大阪"),
            new Customer(3, "鈴木次郎", "東京"),
            new Customer(4, "田村美咲", "名古屋"),
            new Customer(5, "山田健太", "福岡"),
            new Customer(6, "中村真理", "東京"),
            new Customer(7, "小林誠", "大阪"),
            new Customer(8, "加藤優子", "横浜")
        };
    }
    
    static List<Order> CreateOrderData()
    {
        return new List<Order>
        {
            new Order(1001, 1, new DateTime(2024, 1, 15), 15000),
            new Order(1002, 2, new DateTime(2024, 1, 20), 8500),
            new Order(1003, 1, new DateTime(2024, 2, 3), 12000),
            new Order(1004, 3, new DateTime(2024, 2, 10), 25000),
            new Order(1005, 4, new DateTime(2024, 2, 15), 18000),
            new Order(1006, 2, new DateTime(2024, 3, 1), 9500),
            new Order(1007, 5, new DateTime(2024, 3, 8), 22000),
            new Order(1008, 6, new DateTime(2024, 3, 12), 13500),
            new Order(1009, 1, new DateTime(2024, 4, 5), 16500),
            new Order(1010, 7, new DateTime(2024, 4, 18), 11000),
            new Order(1011, 8, new DateTime(2024, 4, 25), 19500),
            new Order(1012, 3, new DateTime(2024, 5, 2), 14000)
        };
    }
    
    static List<OrderItem> CreateOrderItemData()
    {
        return new List<OrderItem>
        {
            new OrderItem(1001, "ノートPC", 1, 12000),
            new OrderItem(1001, "マウス", 1, 3000),
            new OrderItem(1002, "キーボード", 2, 4250),
            new OrderItem(1003, "モニター", 1, 12000),
            new OrderItem(1004, "ノートPC", 2, 12500),
            new OrderItem(1005, "タブレット", 2, 9000),
            new OrderItem(1006, "スピーカー", 1, 9500),
            new OrderItem(1007, "ノートPC", 1, 12000),
            new OrderItem(1007, "マウス", 2, 3000),
            new OrderItem(1007, "キーボード", 1, 4000),
            new OrderItem(1008, "モニター", 1, 13500),
            new OrderItem(1009, "タブレット", 1, 9000),
            new OrderItem(1009, "スピーカー", 1, 7500),
            new OrderItem(1010, "キーボード", 1, 4000),
            new OrderItem(1010, "マウス", 2, 3500),
            new OrderItem(1011, "ノートPC", 1, 12000),
            new OrderItem(1011, "モニター", 1, 7500),
            new OrderItem(1012, "タブレット", 1, 9000),
            new OrderItem(1012, "スピーカー", 1, 5000)
        };
    }
}