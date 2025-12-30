namespace LeetCode.LC0401至LC0500;

public class LC0459重复的子字符串
{
    public bool RepeatedSubstringPattern(string s, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return RepeatedSubstringPattern1(s);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private bool RepeatedSubstringPattern1(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return false;
        }
        
        int n = s.Length;
        bool isMatch = false;
        for (int i = 1; i * 2 <= n; i++)
        {
            // n是当前前缀的长度的整数倍
            if (n % i == 0)
            {
                isMatch = true;
                for (int j = i; j < n; j++)
                {
                    if (s[j] != s[j - i])
                    {
                        isMatch = false;
                        break;
                    }
                }

                if (isMatch)
                {
                    break;
                }
            }
        }
        
        return isMatch;
    }

    private bool RepeatedSubstringPattern2(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return false;
        }
        
        return ($"{s}{s}").IndexOf(s, 1, StringComparison.Ordinal) != s.Length;
    }
    
    private bool RepeatedSubstringPattern3(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return false;
        }
        
        return Contains($"{s}{s}", s);
    }

    private bool Contains(string main, string pattern)
    {
        int[] next = BuildNext(pattern);
        int n = main.Length, m = pattern.Length;
        for (int i = 1, j = 0; i < n;)
        {
            if (main[i] == pattern[j])
            {
                i++;
                j++;
            }
            else if (j > 0)
            {
                j = next[j - 1];
            }
            else
            {
                i++;
            }
            
            if (j == m)
            {
                return true;
            }
            
            if (m - j > n - i)
            {
                break;
            }
        }
        
        return false;
    }

    private int[] BuildNext(string s)
    {
        int n = s.Length, prefixLength = 0;
        int[] next = new int[n];
        next[0] = prefixLength;
        for (int i = 1; i < n;)
        {
            if (s[prefixLength] == s[i])
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