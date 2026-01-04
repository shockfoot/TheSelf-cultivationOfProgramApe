namespace LeetCode.LC0001至LC0100;

public class LC0010正则表达式匹配
{
    public bool IsMatch(string s, string p, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return IsMatch1(s, p);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private bool IsMatch1(string s, string p, byte way = 1)
    {
        int m = s.Length, n = p.Length;
        bool[,] dp = new bool[m + 1, n + 1];
        // s和p都为空字符串时匹配成功
        dp[0, 0] = true;
        for (int i = 0; i <= m; i++)
        {
            // 索引0为空字符串，所以j从1开始匹配
            for (int j = 1; j <= n; j++)
            {
                if (p[j - 1] == '*')
                {
                    dp[i, j] = dp[i, j - 2];
                    if (Matches(s, p, i, j))
                        dp[i, j] = dp[i, j] || dp[i - 1, j];
                }
                else
                {
                    if (Matches(s, p, i, j))
                        dp[i, j] = dp[i - 1, j - 1];
                }
            }
        }
        // 返回结果
        return dp[m, n];
    }

    private bool Matches(string s, string p, int i, int j)
    {
        // s从空字符开始匹配都失败
        if (i == 0) return false;
        return p[j - 1] == '.' || s[i - 1] == p[j - 1];
    }
}