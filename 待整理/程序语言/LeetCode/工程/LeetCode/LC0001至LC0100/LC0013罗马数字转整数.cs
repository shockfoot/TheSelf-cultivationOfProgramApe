namespace LeetCode.LC0001至LC0100;

public class LC0013罗马数字转整数
{
    public int RomanToInt(string s, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return RomanToInt1(s);
            case 2:
                return RomanToInt2(s);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private int RomanToInt1(string s)
    {
        Dictionary<char, int> Symbols = new Dictionary<char, int>()
        {
            { 'I', 1 }, { 'V', 5 }, { 'X', 10 }, { 'L', 50 },
            { 'C', 100 }, { 'D', 500 }, { 'M', 1000 },
        };
        int rel = 0;
        int max = 0;
        for (int i = s.Length - 1; i >= 0; i--)
        {
            int cur = Symbols[s[i]];
            if (cur >= max)
            {
                rel += cur;
                max = cur;
            }
            else
            {
                rel -= cur;
            }
        }
        return rel;
    }

    private int RomanToInt2(string s)
    {
        int rel = 0;
        for (int i = s.Length - 1; i >= 0; i--)
        {
            switch (s[i])
            {
                case 'I':
                    // 如果rel大于等于5则说明之前出现过V或X，此时出现的I应该减去
                    rel += (rel >= 5 ? -1 : 1);
                    break;
                case 'V':
                    rel += 5;
                    break;
                case 'X':
                    // 如果rel大于等于50则说明之前出现过L或C，此时出现的X应该减去
                    rel += (rel >= 50 ? -1 : 1) * 10;
                    break;
                case 'L':
                    rel += 50;
                    break;
                case 'C':
                    // 如果rel大于等于500则说明之前出现过D或M，此时出现的C应该减去
                    rel += (rel >= 500 ? -1 : 1) * 100;
                    break;
                case 'D':
                    rel += 500;
                    break;
                case 'M':
                    rel += 1000;
                    break;
            }
        }
        return rel;
    }
}