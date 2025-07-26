namespace LeetCode.LC0001至LC0100;

public class LC0005最长回文子串
{
    public string LongestPalindrome(string s, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return LongestPalindrome1(s);
            case 2:
                return LongestPalindrome2(s);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }
    
    private string LongestPalindrome1(string s)
    {
        if (s == null || s.Length < 2)
        {
            return s;
        }

        int length = s.Length;
        // 最长回文串的起始位置和长度
        int relStart = 0, maxLength = 1;
        // dp[i,j]表示子串s[i..j]是否是回文串
        bool[,] dp = new bool[length,length];
        // 所有长度为1的子串都是回文串
        for (int i = 0; i < length; i++)
        {
            dp[i, i] = true;
        }
        // 枚举所有子串：从长度为2的开始
        for (int l = 2; l <= length; l++)
        {
            // 左边界
            for (int i = 0; i < length; i++)
            {
                // 由长度和左边界可以确定右边界
                int j = i + l - 1;
                // 右边界越界则退出当前循环
                if (j >= length) break;

                // 两端字符不相同一定不是回文串
                if (s[i] != s[j])
                {
                    dp[i, j] = false;
                }
                else
                {
                    // 两端字符相同长度小于等于3的串是回文串
                    if (j - i < 3)
                    {
                        dp[i, j] = true;
                    }
                    // 和去掉首尾两字母后的串具有相同的回文性
                    else
                    {
                        dp[i, j] = dp[i + 1, j - 1];
                    }
                }
                // 记录最长回文串的长度和位置
                if (dp[i, j] && j - i + 1 > maxLength)
                {
                    relStart = i;
                    maxLength = j - i + 1;
                }
            }
        }
        
        return s.Substring(relStart, maxLength);
    }
    
    private string LongestPalindrome2(string s)
    {
        if (s == null || s.Length == 0)
        {
            return s;
        }

        int length = s.Length;
        // 最长回文串的起始位置和长度
        int start = 0, end = 0;
        // 枚举所有边界情况
        for (int i = 0; i < length; i++)
        {
            // 长度为1时
            int length1 = ExpandAroundCenter(s, i, i, length);
            // 长度为2时
            int length2 = ExpandAroundCenter(s, i, i + 1, length);
            int len = Math.Max(length1, length2);
            // 记录最长回文串的起始和结束索引
            if (len > end - start)
            {
                start = i - (len - 1) / 2;
                end = i + len / 2;
            }
        }
        
        return s.Substring(start, end - start + 1);
    }

    private int ExpandAroundCenter(string s, int left, int right, int length)
    {
        // 当两边字符相同时向两侧扩展
        while (left >= 0 && right < length && s[left] == s[right])
        {
            left--;
            right++;
        }
        
        return right - left - 1;
    }
}