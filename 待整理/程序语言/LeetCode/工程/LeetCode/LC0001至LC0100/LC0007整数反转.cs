using System.Text;

namespace LeetCode.LC0001至LC0100;

public class LC0007整数反转
{
    public int Reverse(int x, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return Reverse1(x);
            case 2:
                return Reverse2(x);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }
    
    private int Reverse1(int x)
    {
        if (x == 0)
        {
            return 0;
        }

        StringBuilder sb = new StringBuilder(11);
        // 先存储符号
        if (x < 0)
        {
            sb.Append("-");
            x = -x;
        }

        // 反转存储每一位
        while (x != 0)
        {
            sb.Append(x % 10);
            x /= 10;
        }

        int ans = 0;
        // 如果溢出则什么都不做
        try
        {
            ans = int.Parse(sb.ToString());
        }
        catch { }

        return ans;
    }
    
    private int Reverse2(int x)
    {
        int ans = 0;
        while (x != 0)
        {
            // 溢出
            if (ans < int.MinValue / 10 || ans > int.MaxValue / 10)
            {
                return 0;
            }

            ans = ans * 10 + x % 10;
            x /= 10;
        }

        return ans;
    }
}