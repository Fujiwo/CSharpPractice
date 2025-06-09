# C# Practice 静的ウェブサイト

このフォルダには、C# Practice練習問題集を表示する静的ウェブサイトが含まれています。

## 概要

この静的ウェブサイトは、C#練習問題とその解答例を見やすく表示するために作成されました。MarkdownとC#コードを自動的に解析して、読みやすい形式でウェブ表示します。

## 特徴

- **レスポンシブデザイン**: モバイルデバイスでも見やすい
- **Markdownサポート**: README.mdファイルを自動的にHTMLに変換
- **C# シンタックスハイライト**: コードを色分けして見やすく表示
- **ナビゲーション**: 12の演習を簡単に閲覧可能
- **解答の表示/非表示**: 学習者が段階的に学習できるよう、解答を隠すことが可能

## ファイル構成

```
website/
├── index.html              # メインのHTMLファイル
├── css/
│   ├── style.css          # メインのスタイルシート
├── js/
│   ├── app.js             # メインのJavaScriptアプリケーション
├── data/
│   └── exercises.json     # 演習データ（自動生成）
├── extract-data.py        # データ抽出スクリプト
└── README.md              # このファイル
```

## セットアップと使用方法

### 1. データの更新

演習データを更新するには、以下のスクリプトを実行してください：

```bash
cd website
python3 extract-data.py
```

このスクリプトは、`../Exercises/` フォルダからすべての演習データを読み取り、`data/exercises.json` に保存します。

### 2. ウェブサイトの起動

静的ウェブサイトを表示するには、HTTPサーバーが必要です：

```bash
# Python 3を使用
cd website
python3 -m http.server 8080

# Node.jsを使用（もしインストールされている場合）
npx serve .

# または任意のウェブサーバーで website/ フォルダを公開
```

その後、ブラウザで `http://localhost:8080` にアクセスしてください。

### 3. デプロイ

静的ファイルなので、任意のウェブホスティングサービスにデプロイ可能です：

- GitHub Pages
- Netlify
- Vercel
- Firebase Hosting
- その他の静的ホスティングサービス

## 開発

### データ構造

`data/exercises.json` には以下の構造でデータが保存されています：

```json
[
  {
    "id": "exercise01",
    "directory": "Exercise01",
    "title": "Exercise 01: 基本的なコンソール出力 (難易度: 1)",
    "difficulty": 1,
    "description": "C#の基本的なコンソール出力を学習します。",
    "readme": "# Exercise 01...",
    "solutions": [
      {
        "filename": "Problem1-1.cs",
        "content": "using System;..."
      }
    ]
  }
]
```

### カスタマイズ

- **スタイル**: `css/style.css` を編集してデザインをカスタマイズ
- **機能**: `js/app.js` を編集して機能を追加
- **レイアウト**: `index.html` を編集してページ構造を変更

## 技術仕様

- **HTML5**: セマンティックなマークアップ
- **CSS3**: Flexbox、Grid、カスタムプロパティを使用
- **JavaScript ES6+**: モジュール、Promise、非同期処理
- **レスポンシブデザイン**: モバイルファースト
- **アクセシビリティ**: WAI-ARIA準拠

## ライセンス

このウェブサイトは元のC# Practice リポジトリと同じMITライセンスの下で公開されています。