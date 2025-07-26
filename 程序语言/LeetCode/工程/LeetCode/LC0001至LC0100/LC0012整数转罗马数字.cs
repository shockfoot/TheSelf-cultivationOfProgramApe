using System.Text;

namespace LeetCode.LC0001至LC0100;

public class LC0012整数转罗马数字
{
    public string IntToRoman(int num, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return IntToRoman1(num);
            case 2:
                return IntToRoman2(num);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private string IntToRoman1(int num)
    {
        Dictionary<int, string> Symbols = new Dictionary<int, string>()
        {
            { 1000, "M" }, { 900, "CM" }, { 500, "D" }, { 400, "CD" }, { 100, "C" },
            { 90, "XC" }, { 50, "L" }, { 40, "XL" }, { 10, "X" },
            { 9, "IX" }, { 5, "V" }, {4, "IV" }, { 1, "I" }
        };
        StringBuilder roman = new StringBuilder();
        // 从大到小遍历符号
        foreach (KeyValuePair<int, string> kv in Symbols)
        {
            int value = kv.Key;
            // 拼接尽可能大的符号
            while (num >= value)
            {
                num -= value;
                roman.Append(kv.Value);
            }
            // 退出
            if (num == 0)
                break;
        }
        return roman.ToString();
    }

    private string IntToRoman2(int num)
    {
        // 所有数字表示
        string[][] map = new string[][]
        {
            new string[] { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" },
            new string[] { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" },
            new string[] { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" },
            new string[] { "", "M", "MM", "MMM", },
        };
        StringBuilder roman = new StringBuilder();
        roman.Append(map[3][num / 1000]);
        roman.Append(map[2][num % 1000 / 100]);
        roman.Append(map[1][num % 100 / 10]);
        roman.Append(map[0][num % 10]);
        return roman.ToString();
    }
}