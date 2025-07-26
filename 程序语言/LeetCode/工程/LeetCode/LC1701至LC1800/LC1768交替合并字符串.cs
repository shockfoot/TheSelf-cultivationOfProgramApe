namespace LeetCode.LC1701至LC1800;

public class LC1768交替合并字符串
{
    public string MergeAlternately(string word1, string word2, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return MergeAlternately1(word1, word2);
            case 2:
                return MergeAlternately2(word1, word2);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }
    
    private string MergeAlternately1(string word1, string word2)
    {
        if (string.IsNullOrEmpty(word1))
        {
            return word2;
        }
        
        if (string.IsNullOrEmpty(word2))
        {
            return word1;
        }
        
        int indexInWord1 = 0, indexInWord2 = 0;
        int lengthOfWord1 = word1.Length, lengthOfWord2 = word2.Length, totalLength = lengthOfWord1 + lengthOfWord2;
        char[] result = new char[totalLength];
        for (int i = 0; i < totalLength; i++)
        {
            if (indexInWord1 >= lengthOfWord1 && indexInWord2 < lengthOfWord2)
            {
                result[i] = word2[indexInWord2++];
                continue;
            }

            if (indexInWord1 < lengthOfWord1 && indexInWord2 >= lengthOfWord2)
            {
                result[i] = word1[indexInWord1++];
                continue;
            }

            if (i % 2 == 0)
            {
                result[i] = word1[indexInWord1++];
            }
            else
            {
                result[i] = word2[indexInWord2++];
            }
        }
        
        return new string(result);
    }
    
    private string MergeAlternately2(string word1, string word2)
    {
        if (string.IsNullOrEmpty(word1))
        {
            return word2;
        }
        
        if (string.IsNullOrEmpty(word2))
        {
            return word1;
        }
        
        int i = 0, indexInWord = 0;
        int lengthOfWord1 = word1.Length, lengthOfWord2 = word2.Length, totalLength = lengthOfWord1 + lengthOfWord2;
        char[] result = new char[totalLength];
        while (i < totalLength)
        {
            if (indexInWord < lengthOfWord1)
            {
                result[i++] = word1[indexInWord];
            }

            if (indexInWord < lengthOfWord2)
            {
                result[i++] = word2[indexInWord];
            }

            indexInWord++;
        }
        
        return new string(result);
    }
}