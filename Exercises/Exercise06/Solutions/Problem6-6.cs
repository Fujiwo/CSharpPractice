using System;

class Program
{
    static void Main()
    {
        // 問題 6-6: 配列操作の各種メソッド
        int[] testArray = {64, 34, 25, 12, 22, 11, 90, 25, 12};
        
        Console.WriteLine("元の配列:");
        PrintArray(testArray);
        
        // ソートのテスト
        int[] sortedArray = (int[])testArray.Clone();
        BubbleSort(sortedArray);
        Console.WriteLine("\nソート後:");
        PrintArray(sortedArray);
        
        // 重複削除のテスト
        int[] uniqueArray = RemoveDuplicates(testArray);
        Console.WriteLine("\n重複削除後:");
        PrintArray(uniqueArray);
        
        // 配列を逆順にするテスト
        int[] reversedArray = (int[])testArray.Clone();
        ReverseArray(reversedArray);
        Console.WriteLine("\n逆順:");
        PrintArray(reversedArray);
        
        // 検索のテスト
        int searchValue = 25;
        int index = LinearSearch(testArray, searchValue);
        Console.WriteLine($"\n値 {searchValue} のインデックス: {index}");
        
        searchValue = 99;
        index = LinearSearch(testArray, searchValue);
        Console.WriteLine($"値 {searchValue} のインデックス: {index} (見つからない場合は-1)");
    }
    
    static void BubbleSort(int[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    // 要素を交換
                    int temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }
    }
    
    static int[] RemoveDuplicates(int[] array)
    {
        int[] tempArray = new int[array.Length];
        int uniqueCount = 0;
        
        for (int i = 0; i < array.Length; i++)
        {
            bool isDuplicate = false;
            for (int j = 0; j < uniqueCount; j++)
            {
                if (array[i] == tempArray[j])
                {
                    isDuplicate = true;
                    break;
                }
            }
            if (!isDuplicate)
            {
                tempArray[uniqueCount] = array[i];
                uniqueCount++;
            }
        }
        
        // 実際のサイズに合わせて配列を作成
        int[] result = new int[uniqueCount];
        for (int i = 0; i < uniqueCount; i++)
        {
            result[i] = tempArray[i];
        }
        return result;
    }
    
    static void ReverseArray(int[] array)
    {
        int start = 0;
        int end = array.Length - 1;
        
        while (start < end)
        {
            // 要素を交換
            int temp = array[start];
            array[start] = array[end];
            array[end] = temp;
            
            start++;
            end--;
        }
    }
    
    static int LinearSearch(int[] array, int value)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == value)
            {
                return i;
            }
        }
        return -1; // 見つからない場合
    }
    
    static void PrintArray(int[] array)
    {
        Console.Write("[");
        for (int i = 0; i < array.Length; i++)
        {
            Console.Write(array[i]);
            if (i < array.Length - 1)
            {
                Console.Write(", ");
            }
        }
        Console.WriteLine("]");
    }
}