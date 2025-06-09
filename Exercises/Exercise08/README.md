# Exercise 08: 継承とポリモーフィズム (難易度: 4)

## 概要
C#の継承とポリモーフィズムについて学習します。

## 問題

### 問題 8-1 (難易度: 4)
動物の継承階層を作成してください。

**要求事項:**
- 基底クラス Animal を作成する
- プロパティ: Name (string), Age (int)
- 仮想メソッド: MakeSound(), Move()
- 派生クラス: Dog, Cat を作成する
- 各派生クラスで MakeSound(), Move() をオーバーライドする
- メインメソッドでポリモーフィズムをデモンストレーションする

### 問題 8-2 (難易度: 4)
従業員管理システムを作成してください。

**要求事項:**
- 基底クラス Employee を作成する
- プロパティ: Name, Id, BaseSalary
- 抽象メソッド: CalculateSalary()
- 派生クラス: FullTimeEmployee, PartTimeEmployee を作成する
- FullTimeEmployee: 基本給に賞与を加算
- PartTimeEmployee: 時給 × 勤務時間
- メインメソッドで各従業員の給与を計算する
