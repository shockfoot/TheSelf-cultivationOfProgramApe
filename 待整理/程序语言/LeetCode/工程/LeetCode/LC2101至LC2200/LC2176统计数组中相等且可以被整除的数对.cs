namespace LeetCode.LC2101至LC2200;

public class LC2176统计数组中相等且可以被整除的数对
{
    public int CountPairs(int[] nums, int k, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return CountPairs1(nums, k);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private int CountPairs1(int[] nums, int k)
    {
        if (nums is not { Length: > 0 })
        {
            return 0;
        }

        int count = 0;
        int n = nums.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (nums[i] == nums[j] &&
                    i * j % k == 0)
                {
                    count++;
                }
            }
        }
        
        return count;
    }
}