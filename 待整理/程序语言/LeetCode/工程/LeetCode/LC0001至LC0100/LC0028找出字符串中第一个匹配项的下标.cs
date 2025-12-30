namespace LeetCode.LC0001至LC0100;

public class LC0028找出字符串中第一个匹配项的下标
{
    public int StrStr(string haystack, string needle, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return StrStr1(haystack, needle);
            case 2:
                return StrStr2(haystack, needle);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private int StrStr1(string haystack, string needle)
    {
        if (string.IsNullOrEmpty(haystack) || string.IsNullOrEmpty(needle))
        {
            return -1;
        }

        int rel = -1;
        int n = haystack.Length, m = needle.Length;
        bool hasMatched;
        int tempI;
        for (int i = 0; i <= n - m; i++)
        {
            hasMatched = true;
            tempI = i;
            for (int j = 0; j < m; j++, tempI++)
            {
                if (haystack[tempI] != needle[j])
                {
                    hasMatched = false;
                    break;
                }
            }

            if (hasMatched)
            {
                rel = i;
                break;
            }
        }
        
        return rel;
    }
    
    /// <summary>
    /// KMP算法
    /// </summary>
    private int StrStr2(string haystack, string needle)
    {
        if (string.IsNullOrEmpty(haystack) || string.IsNullOrEmpty(needle))
        {
            return -1;
        }

        int[] next = BuildNext(needle);
        int n = haystack.Length, m = needle.Length;
        for (int i = 0, j = 0; i < n;)
        {
            // 字符匹配成功
            if (haystack[i] == needle[j])
            {
                i++;
                j++;
            }
            // 字符匹配失败
            else if (j > 0)
            {
                j = next[j - 1];
            }
            // 第一个字符就匹配失败
            else
            {
                i++;
            }

            // 模式串匹配成功
            if (j == m)
            {
                return i - j;
            }

            // 模式串剩余部分超出主串剩余部分，匹配失败
            if (m - j > n - i)
            {
                break;
            }
        }

        return -1;
    }

    private int[] BuildNext(string needle)
    {
        int n = needle.Length, prefixLength = 0;
        int[] next = new int[n];
        next[0] = prefixLength;
        for (int i = 1; i < n;)
        {
            if (needle[prefixLength] == needle[i])
            {
                prefixLength++;
                next[i] = prefixLength;
                i++;
            }
            else
            {
                if (prefixLength == 0)
                {
                    next[i] = prefixLength;
                    i++;
                }
                else
                {
                    prefixLength = next[prefixLength - 1];
                }
            }
        }
        
        return next;
    }
}