using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        // 問題 11-3: テキスト分析ツール
        Console.WriteLine("テキスト分析ツール");
        Console.WriteLine("==================");
        
        string sampleText = @"
        プログラミングは創造的な活動です。コンピュータと人間の間の橋渡しをする重要な技術です。
        C#は強力なプログラミング言語であり、様々なアプリケーション開発に使用されています。
        オブジェクト指向プログラミングの概念を理解することで、より効率的で保守性の高いコードを書くことができます。
        継承、ポリモーフィズム、カプセル化といった概念は、複雑なソフトウェアシステムを構築する上で不可欠です。
        LINQ（Language Integrated Query）は、データ操作を簡潔に記述できる便利な機能です。
        例外処理により、プログラムの安定性と信頼性を向上させることができます。
        ";
        
        Console.WriteLine("分析対象のテキスト:");
        Console.WriteLine(sampleText.Trim());
        Console.WriteLine("\n" + new string('=', 50));
        
        // テキストを単語に分割
        var words = ExtractWords(sampleText);
        
        // 分析1: 単語の出現頻度
        Console.WriteLine("\n=== 単語の出現頻度（上位10位） ===");
        var wordFrequency = words
            .GroupBy(word => word.ToLower())
            .Select(g => new { Word = g.Key, Count = g.Count() })
            .OrderByDescending(w => w.Count)
            .Take(10);
        
        foreach (var freq in wordFrequency)
        {
            Console.WriteLine($"{freq.Word}: {freq.Count}回");
        }
        
        // 分析2: 最も長い単語と短い単語
        Console.WriteLine("\n=== 単語の長さ分析 ===");
        var distinctWords = words.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        
        var longestWord = distinctWords.OrderByDescending(w => w.Length).First();
        var shortestWord = distinctWords.OrderBy(w => w.Length).First();
        var averageLength = distinctWords.Average(w => w.Length);
        
        Console.WriteLine($"最も長い単語: {longestWord} ({longestWord.Length}文字)");
        Console.WriteLine($"最も短い単語: {shortestWord} ({shortestWord.Length}文字)");
        Console.WriteLine($"平均文字数: {averageLength:F1}文字");
        
        // 分析3: 特定の長さの単語をフィルタリング
        Console.WriteLine("\n=== 5文字以上の単語 ===");
        var longWords = distinctWords
            .Where(w => w.Length >= 5)
            .OrderByDescending(w => w.Length)
            .ThenBy(w => w);
        
        foreach (var word in longWords)
        {
            Console.WriteLine($"{word} ({word.Length}文字)");
        }
        
        // 分析4: 文字数でグループ化した単語の統計
        Console.WriteLine("\n=== 文字数別統計 ===");
        var lengthGroups = distinctWords
            .GroupBy(w => w.Length)
            .Select(g => new
            {
                Length = g.Key,
                Count = g.Count(),
                Words = g.OrderBy(w => w).ToList(),
                Percentage = (g.Count() * 100.0 / distinctWords.Count)
            })
            .OrderBy(g => g.Length);
        
        foreach (var group in lengthGroups)
        {
            Console.WriteLine($"{group.Length}文字: {group.Count}個 ({group.Percentage:F1}%)");
            Console.WriteLine($"  単語: {string.Join(", ", group.Words)}");
        }
        
        // 分析5: 文字別の出現頻度
        Console.WriteLine("\n=== 文字出現頻度（上位10位） ===");
        var charFrequency = sampleText
            .Where(c => char.IsLetter(c))
            .GroupBy(c => c)
            .Select(g => new { Char = g.Key, Count = g.Count() })
            .OrderByDescending(c => c.Count)
            .Take(10);
        
        foreach (var freq in charFrequency)
        {
            Console.WriteLine($"{freq.Char}: {freq.Count}回");
        }
        
        // 分析6: 高度な分析
        Console.WriteLine("\n=== 高度な分析 ===");
        
        // 特定のパターンの単語を検索
        var programmingTerms = words
            .Where(w => w.Contains("プログラム") || w.Contains("コード") || w.Contains("システム"))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();
        
        Console.WriteLine($"プログラミング関連用語: {string.Join(", ", programmingTerms)}");
        
        // 母音の多い単語
        var vowelCounts = distinctWords
            .Select(w => new
            {
                Word = w,
                VowelCount = w.Count(c => "あいうえおアイウエオ".Contains(c))
            })
            .Where(w => w.VowelCount > 0)
            .OrderByDescending(w => w.VowelCount)
            .Take(5);
        
        Console.WriteLine("\n母音の多い単語（上位5位）:");
        foreach (var item in vowelCounts)
        {
            Console.WriteLine($"{item.Word}: {item.VowelCount}個の母音");
        }
        
        // 全体統計
        Console.WriteLine("\n=== 全体統計 ===");
        Console.WriteLine($"総文字数: {sampleText.Length}文字");
        Console.WriteLine($"総単語数: {words.Count}語");
        Console.WriteLine($"ユニーク単語数: {distinctWords.Count}語");
        Console.WriteLine($"平均単語長: {words.Average(w => w.Length):F1}文字");
        
        // 文の数を数える
        var sentences = sampleText.Split(new char[] { '。', '.' }, StringSplitOptions.RemoveEmptyEntries);
        Console.WriteLine($"文の数: {sentences.Length}文");
        Console.WriteLine($"1文あたりの平均単語数: {words.Count / (double)sentences.Length:F1}語");
    }
    
    static List<string> ExtractWords(string text)
    {
        // 日本語と英語の単語を抽出
        // ひらがな、カタカナ、漢字、英字の連続を単語として認識
        var pattern = @"[ひらがなカタカナ漢字a-zA-Zー]+";
        var regex = new Regex(pattern);
        
        // Unicode範囲を使った正規表現パターン
        var japanesePattern = @"[\u3040-\u309F\u30A0-\u30FF\u4E00-\u9FAF\u3005\u3006\u30FC\u30FEa-zA-Z]+";
        var japaneseRegex = new Regex(japanesePattern);
        
        var matches = japaneseRegex.Matches(text);
        var words = new List<string>();
        
        foreach (Match match in matches)
        {
            string word = match.Value.Trim();
            if (word.Length > 0)
            {
                words.Add(word);
            }
        }
        
        return words;
    }
}