namespace LeetCode.LC0001至LC0100;

public class LC0009回文数
{
    public bool IsPalindrome(int x, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return IsPalindrome1(x);
            case 2:
                return IsPalindrome2(x);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private bool IsPalindrome1(int x)
    {
        // 排除特殊情况
        if (x == 0) return true;
        if (x < 0 || (x != 0 && x % 10 == 0)) return false;

        // 转换为字符串
        string s = x.ToString();
        // 双指针
        int left = 0, right = s.Length - 1;
        while (left < right)
        {
            if (s[left++] != s[right--]) return false;
        }
        return true;
    }

    private bool IsPalindrome2(int x)
    {
        // 排除特殊情况
        if (x == 0) return true;
        if (x < 0 || (x != 0 && x % 10 == 0)) return false;

        int revertedNumber = 0;
        while (x > revertedNumber)
        {
            revertedNumber = revertedNumber * 10 + x % 10;
            x /= 10;
        }
        // 需要排除奇数长度时最中间的数字
        return x == revertedNumber || x == revertedNumber / 10;
    }
}