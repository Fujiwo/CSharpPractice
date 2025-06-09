using System;
using System.Collections.Generic;
using System.Linq;

class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public bool IsAvailable { get; set; }
    
    public Book(string title, string author, string isbn)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        IsAvailable = true;
    }
    
    public void DisplayInfo()
    {
        string status = IsAvailable ? "利用可能" : "貸出中";
        Console.WriteLine($"タイトル: {Title}");
        Console.WriteLine($"著者: {Author}");
        Console.WriteLine($"ISBN: {ISBN}");
        Console.WriteLine($"状態: {status}");
        Console.WriteLine("-------------------");
    }
}

class Library
{
    private List<Book> books;
    
    public Library()
    {
        books = new List<Book>();
    }
    
    public void AddBook(Book book)
    {
        books.Add(book);
        Console.WriteLine($"本 '{book.Title}' を図書館に追加しました。");
    }
    
    public bool BorrowBook(string isbn)
    {
        Book book = books.FirstOrDefault(b => b.ISBN == isbn);
        if (book == null)
        {
            Console.WriteLine($"ISBN {isbn} の本が見つかりません。");
            return false;
        }
        
        if (!book.IsAvailable)
        {
            Console.WriteLine($"'{book.Title}' は既に貸出中です。");
            return false;
        }
        
        book.IsAvailable = false;
        Console.WriteLine($"'{book.Title}' を貸し出しました。");
        return true;
    }
    
    public bool ReturnBook(string isbn)
    {
        Book book = books.FirstOrDefault(b => b.ISBN == isbn);
        if (book == null)
        {
            Console.WriteLine($"ISBN {isbn} の本が見つかりません。");
            return false;
        }
        
        if (book.IsAvailable)
        {
            Console.WriteLine($"'{book.Title}' は既に返却済みです。");
            return false;
        }
        
        book.IsAvailable = true;
        Console.WriteLine($"'{book.Title}' が返却されました。");
        return true;
    }
    
    public List<Book> SearchByTitle(string title)
    {
        return books.Where(b => b.Title.Contains(title)).ToList();
    }
    
    public List<Book> SearchByAuthor(string author)
    {
        return books.Where(b => b.Author.Contains(author)).ToList();
    }
    
    public void DisplayAllBooks()
    {
        Console.WriteLine("\n=== 図書館の蔵書一覧 ===");
        if (books.Count == 0)
        {
            Console.WriteLine("蔵書がありません。");
            return;
        }
        
        foreach (Book book in books)
        {
            book.DisplayInfo();
        }
    }
    
    public void DisplayAvailableBooks()
    {
        Console.WriteLine("\n=== 利用可能な本 ===");
        List<Book> availableBooks = books.Where(b => b.IsAvailable).ToList();
        
        if (availableBooks.Count == 0)
        {
            Console.WriteLine("現在利用可能な本はありません。");
            return;
        }
        
        foreach (Book book in availableBooks)
        {
            book.DisplayInfo();
        }
    }
}

class Program
{
    static void Main()
    {
        // 問題 7-6: 図書館の本管理システム
        Library library = new Library();
        
        // 本を追加
        library.AddBook(new Book("吾輩は猫である", "夏目漱石", "978-4-10-101001-0"));
        library.AddBook(new Book("坊っちゃん", "夏目漱石", "978-4-10-101002-7"));
        library.AddBook(new Book("羅生門", "芥川龍之介", "978-4-10-102001-9"));
        library.AddBook(new Book("銀河鉄道の夜", "宮沢賢治", "978-4-10-103001-8"));
        
        // 全蔵書表示
        library.DisplayAllBooks();
        
        // 本の貸出
        Console.WriteLine("\n=== 貸出テスト ===");
        library.BorrowBook("978-4-10-101001-0");
        library.BorrowBook("978-4-10-102001-9");
        library.BorrowBook("978-4-10-999999-9"); // 存在しない本
        
        // 利用可能な本表示
        library.DisplayAvailableBooks();
        
        // 本の返却
        Console.WriteLine("\n=== 返却テスト ===");
        library.ReturnBook("978-4-10-101001-0");
        
        // 検索テスト
        Console.WriteLine("\n=== 検索テスト ===");
        List<Book> searchResults = library.SearchByAuthor("夏目漱石");
        Console.WriteLine($"著者 '夏目漱石' の検索結果: {searchResults.Count}件");
        foreach (Book book in searchResults)
        {
            book.DisplayInfo();
        }
    }
}