namespace LeetCode.LC3201至LC3300;

public class LC3272统计好整数的数目
{
    public long CountGoodIntegers(int n, int k, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return CountGoodIntegers1(n, k);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private long CountGoodIntegers1(int n, int k)
    {
        HashSet<string> set = new HashSet<string>();
        
        int low = (int)Math.Pow(10, (n - 1) / 2);
        int high = low * 10;
        int skip = n & 1;

        // 枚举k回文整数
        for (int i = low; i < high; i++)
        {
            string s = i.ToString();
            s += new string(s.Reverse().Skip(skip).ToArray());
            long palindromicInteger = long.Parse(s);
            if (palindromicInteger % k == 0)
            {
                char[] chars = s.ToCharArray();
                Array.Sort(chars);
                set.Add(new string(chars));
            }
        }
        
        long[] factorial = new long[n + 1];
        factorial[0] = 1;
        for (int i = 1; i <= n; i++)
        {
            factorial[i] = factorial[i - 1] * i;
        }

        long ans = 0;
        foreach (var s in set)
        {
            // 各数字出现次数
            int[] count = new int[10];
            foreach (var c in s)
            {
                count[c - '0']++;
            }
            
            // 计算排列组合
            long tot = (n - count[0]) * factorial[n - 1];
            foreach (var c in count)
            {
                tot /= factorial[c];
            }
            
            ans += tot;
        }
        
        return ans;
    }
}