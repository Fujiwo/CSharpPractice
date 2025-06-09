# Exercise 07: クラスとオブジェクト (難易度: 3-4)

## 概要
C#のクラスとオブジェクト指向プログラミングの基礎について学習します。

## 問題

### 問題 7-1 (難易度: 3)
学生の情報を管理するクラスを作成してください。

**要求事項:**
- Studentクラスを作成する
- プロパティ: Name (string), Age (int), Grade (double)
- コンストラクタで初期値を設定する
- 学生情報を表示するメソッドを作成する
- メインメソッドで複数の学生オブジェクトを作成し、情報を表示する

### 問題 7-2 (難易度: 4)
銀行口座を管理するクラスを作成してください。

**要求事項:**
- BankAccountクラスを作成する
- プロパティ: AccountNumber (string), Balance (decimal), Owner (string)
- メソッド: Deposit(預金), Withdraw(引き出し), GetBalance(残高照会)
- 残高不足の場合はエラーメッセージを表示する
- メインメソッドで口座を作成し、各操作をテストする

### 問題 7-3 (難易度: 4)
図形の面積を計算するクラス群を作成してください。

**要求事項:**
- Rectangleクラス（長方形）: Width, Height プロパティ、CalculateArea メソッド
- Circleクラス（円）: Radius プロパティ、CalculateArea メソッド
- 各クラスに適切なコンストラクタを実装する
- メインメソッドで各図形の面積を計算して表示する

### 問題 7-4 (難易度: 4)
在庫管理システムを作成してください。

**要求事項:**
- Productクラス: Id, Name, Price, Stock プロパティ
- InventoryManagerクラス: 商品の追加、在庫更新、検索機能
- メソッド: AddProduct, UpdateStock, FindProduct, GetLowStockProducts
- メインメソッドで各機能をテストする
- 在庫が少ない商品（5個以下）を警告表示する

### 問題 7-5 (難易度: 4)
従業員管理システムを作成してください。

**要求事項:**
- Employeeクラス: Id, Name, Department, Salary プロパティ
- FullTimeEmployeeクラス: Employee継承、固定給与
- PartTimeEmployeeクラス: Employee継承、時給計算機能
- 各クラスに適切なコンストラクタとCalculatePay メソッドを実装
- メインメソッドで様々な従業員を作成し、給与を計算・表示する

### 問題 7-6 (難易度: 4)
図書館の本管理システムを作成してください。

**要求事項:**
- Bookクラス: Title, Author, ISBN, IsAvailable プロパティ
- Libraryクラス: 本の管理機能
- メソッド: AddBook, BorrowBook, ReturnBook, SearchByTitle, SearchByAuthor
- 貸出状況の管理（利用可能/貸出中）
- メインメソッドで図書館システムをテストする

### 問題 7-7 (難易度: 4)
ショッピングカートシステムを作成してください。

**要求事項:**
- Itemクラス: Name, Price, Quantity プロパティ
- ShoppingCartクラス: アイテムの管理機能
- メソッド: AddItem, RemoveItem, UpdateQuantity, CalculateTotal, ApplyDiscount
- 割引機能（10%、20%など）
- メインメソッドでカートの操作をテストし、合計金額を表示する

### 問題 7-8 (難易度: 4)
動物の分類システムを作成してください。

**要求事項:**
- Animalクラス（基底クラス）: Name, Age プロパティ、MakeSound 仮想メソッド
- Dogクラス、Catクラス、Birdクラス（Animal継承）
- 各動物クラスでMakeSoundメソッドをオーバーライド
- AnimalShelterクラス: 動物の管理機能
- メソッド: AddAnimal, GetAllAnimals, GetAnimalsByType
- メインメソッドで動物シェルターをテストし、各動物の鳴き声を表示する
