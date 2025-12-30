namespace LeetCode.LC2901至LC3000;

public class LC2999统计强大整数的数目
{
    public long NumberOfPowerfulInt(long start, long finish, int limit,
        string s, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return NumberOfPowerfulInt1(start, finish, limit, s);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private long NumberOfPowerfulInt1(long start, long finish, int limit, string s)
    {
        int count = 0;
        
        long num = long.Parse(s);
        long power = (long)Math.Pow(10, s.Length);

        bool isStrong;
        long x, digit;
        for (int i = 0;; i++)
        {
            x = i * power + num;
            if (x < start)
            {
                continue;
            }
            
            if (x > finish)
            {
                break;
            }

            isStrong = true;
            while (x > 0)
            {
                digit = x % 10;
                if (digit > limit)
                {
                    isStrong = false;
                    break;
                }
                x /= 10;
            }
            
            if (isStrong)
            {
                count++;
            }
        }
        
        return count;
    }

    private long NumberOfPowerfulInt2(long start, long finish, int limit, string s)
    {
        int count = 0;
        
        long num = long.Parse(s);
        long power = (long)Math.Pow(10, s.Length);

        bool isStrong;
        long x, digit;
        for (int i = 0;; i++)
        {
            x = i * power + num;
            if (x < start)
            {
                continue;
            }
            
            if (x > finish)
            {
                break;
            }

            isStrong = true;
            while (x > 0)
            {
                digit = x % 10;
                if (digit > limit)
                {
                    isStrong = false;
                    break;
                }
                x /= 10;
            }
            
            if (isStrong)
            {
                count++;
            }
        }
        
        return count;
    }
}