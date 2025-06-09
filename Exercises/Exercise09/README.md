# Exercise 09: インターフェースと抽象クラス (難易度: 4)

## 概要
C#のインターフェースと抽象クラスの使い方について学習します。

## 問題

### 問題 9-1 (難易度: 4)
図形描画システムを作成してください。

**要求事項:**
- IDrawable インターフェースを作成する（Draw() メソッド）
- IResizable インターフェースを作成する（Resize(double factor) メソッド）
- Shape 抽象クラスを作成する（Area() 抽象メソッド、Name プロパティ）
- Rectangle, Circle クラスを作成し、両方のインターフェースを実装する
- メインメソッドで各図形を描画し、リサイズする

### 問題 9-2 (難易度: 4)
ファイル処理システムを作成してください。

**要求事項:**
- IFileProcessor インターフェースを作成する
- メソッド: Process(string content), GetProcessorName()
- 実装クラス: TextProcessor, CsvProcessor, JsonProcessor を作成する
- 各プロセッサーで異なる処理を実装する
- メインメソッドでファイルプロセッサーの配列を使用して処理を実行する
