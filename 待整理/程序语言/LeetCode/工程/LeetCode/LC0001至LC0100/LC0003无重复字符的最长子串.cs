using Math = Common.Math;

namespace LeetCode.LC0001至LC0100;

public class LC0003无重复字符的最长子串
{
    public int LengthOfLongestSubstring(string s, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return LengthOfLongestSubstring1(s);
            case 2:
                return LengthOfLongestSubstring2(s);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private int LengthOfLongestSubstring1(string s)
    {
        // 输入合法判断
        if (s == null || s.Length == 0)
        {
            return 0;
        }

        int maxLength = 0, curLength = 0;
        int l = s.Length;
        // 存储子串已出现的字符
        HashSet<char> substringCharacters = new HashSet<char>(l);
        for (int i = 0; i < l; i++)
        {
            // 清空上一个子串的数据
            substringCharacters.Clear();
            substringCharacters.Add(s[i]);
            curLength = 1;
            for (int j = i + 1; j < l; j++)
            {
                // 子串出现重复字符退出
                if (substringCharacters.Contains(s[j])) break;

                substringCharacters.Add(s[j]);
                curLength++;
            }
            // 记录最大长度
            maxLength = Math.Max(maxLength, curLength);
        }

        return maxLength;
    }

    private int LengthOfLongestSubstring2(string s)
    {
        // 输入合法判断
        if (s == null || s.Length == 0)
        {
            return 0;
        }

        int maxLength = 0, right = 0, left = 0, n = s.Length;
        // 存储子串已出现的字符
        HashSet<char> substringCharacters = new HashSet<char>(n);
        while (right < n)
        {
            // 右侧新字符重复时移动左指针，收窄窗口
            if (substringCharacters.Contains(s[right]))
            {
                // 收窄前记录最大长度
                maxLength = Math.Max(maxLength, substringCharacters.Count);
                substringCharacters.Remove(s[left]);
                left++;
            }
            // 不重复时移动右指针，扩大窗口
            else
            {
                substringCharacters.Add(s[right]);
                right++;
            }
        }

        return Math.Max(maxLength, substringCharacters.Count);
    }
}