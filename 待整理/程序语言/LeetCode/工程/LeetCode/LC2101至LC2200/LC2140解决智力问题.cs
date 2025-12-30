namespace LeetCode.LC2101至LC2200;

public class LC2140解决智力问题
{
    public long MostPoints(int[][] questions, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return MostPoints1(questions);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private long MostPoints1(int[][] questions)
    {
        if (questions == null || questions.Length <= 0)
        {
            return 0;
        }

        if (questions.Length == 1)
        {
            return questions[0][0];
        }

        int length = questions.Length;
        long[] dp = new long[length + 1];
        for (int i = length - 1; i >= 0; i--)
        {
            dp[i] = Common.Math.Max(dp[i + 1], questions[i][0] + dp[Common.Math.Min(length, i + questions[i][1] + 1)]);
        }
        
        return dp[0];
    }
}
