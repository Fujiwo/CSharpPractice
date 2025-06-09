# Exercise 10: 例外処理 (難易度: 3-4)

## 概要
C#の例外処理（try-catch-finally）について学習します。

## 問題

### 問題 10-1 (難易度: 3)
安全な数値入力システムを作成してください。

**要求事項:**
- ユーザーから数値を入力として受け取る
- 無効な入力に対して適切なエラーハンドリングを行う
- FormatException, OverflowException を個別に処理する
- ユーザーが有効な数値を入力するまで繰り返す
- finally ブロックを使用してリソースのクリーンアップをデモンストレーションする

### 問題 10-2 (難易度: 4)
ファイル読み込み処理の例外処理を実装してください。

**要求事項:**
- 指定されたファイルを読み込む機能
- FileNotFoundException, UnauthorizedAccessException, IOException を処理
- カスタム例外クラス FileProcessingException を作成する
- 複数のファイルを処理し、エラーが発生しても他のファイル処理を継続する
- 処理結果のサマリーを表示する

### 問題 10-3 (難易度: 4)
電卓アプリケーションに包括的な例外処理を追加してください。

**要求事項:**
- 四則演算を行う電卓クラス
- DivideByZeroException, ArgumentException を処理
- カスタム例外 InvalidOperationException を作成
- 複数の計算を実行し、エラー発生時の適切な処理を実装
- ログ機能を追加して例外情報を記録する
