namespace LeetCode.LC0201至LC0300;

public class LC0242有效的字母异位词
{
    public bool IsAnagram(string s, string t, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return IsAnagram1(s, t);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }
    
    private bool IsAnagram1(string s, string t)
    {
        if (string.IsNullOrEmpty(s) && string.IsNullOrEmpty(t))
        {
            return true;
        }
        
        if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(t) || s.Length != t.Length)
        {
            return false;
        }
        
        int[] charCount = new int[26];
        int n = s.Length;
        for (int i = 0; i < n; i++)
        {
            charCount[s[i] - 'a']++;
        }

        char c;
        for (int i = 0; i < n; i++)
        {
            c = t[i];
            charCount[c - 'a']--;
            if (charCount[c - 'a'] < 0)
            {
                return false;
            }
        }
        
        return true;
    }
}