namespace LeetCode.LC0301至LC0400;

public class LC0389找不同
{
    public char FindTheDifference(string s, string t, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return FindTheDifference1(s, t);
            case 2:
                return FindTheDifference2(s, t);
            case 3:
                return FindTheDifference3(s, t);
            case 4:
                return FindTheDifference3(s, t);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private char FindTheDifference1(string s, string t)
    {
        if (string.IsNullOrEmpty(s))
        {
            return t[0];
        }
        
        int n = t.Length;
        char c;
        Dictionary<char, int> set = new Dictionary<char, int>(26);
        for (int i = 0; i < n; i++)
        {
            c = t[i];
            if (set.ContainsKey(c))
            {
                set[c]++;
            }
            else
            {
                set.Add(c, 1);
            }
        }

        n = s.Length;
        for (int i = 0; i < n; i++)
        {
            c = s[i];
            if (set.ContainsKey(c))
            {
                set[c]--;
                if (set[c] <= 0)
                {
                    set.Remove(c);
                }
            }
        }

        return set.First().Key;
    }

    private char FindTheDifference2(string s, string t)
    {
        if (string.IsNullOrEmpty(s))
        {
            return t[0];
        }
        
        int[] charCount = new int[26];
        
        int n = s.Length;
        for (int i = 0; i < n; i++)
        {
            charCount[s[i] - 'a']++;
        }

        n = t.Length;
        char c;
        for (int i = 0; i < n; i++)
        {
            c = t[i];
            charCount[c - 'a']--;
            if (charCount[c - 'a'] < 0)
            {
                return c;
            }
        }

        return ' ';
    }

    private char FindTheDifference3(string s, string t)
    {
        if (string.IsNullOrEmpty(s))
        {
            return t[0];
        }

        int sum = 0;

        int n = s.Length;
        for (int i = 0; i < n; i++)
        {
            sum += s[i];
        }

        n = t.Length;
        for (int i = 0; i < n; i++)
        {
            sum -= t[i];
        }

        return (char)(-sum);
    }

    private char FindTheDifference4(string s, string t)
    {
        if (string.IsNullOrEmpty(s))
        {
            return t[0];
        }

        int rel = 0;
        
        int n = s.Length;
        for (int i = 0; i < n; i++)
        {
            rel ^= s[i];
        }

        n = t.Length;
        for (int i = 0; i < n; i++)
        {
            rel ^= t[i];
        }

        return (char)rel;
    }
}