namespace LeetCode.LC2301至LC2400;

public class LC2302统计得分小于K的子数组数目
{
    public long CountSubarrays(int[] nums, long k, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return CountSubarrays1(nums, k);
            case 2:
                return CountSubarrays2(nums, k);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }
    
    private long CountSubarrays1(int[] nums, long k)
    {
        if (nums is not { Length: > 0 })
        {
            return 0;
        }

        int n = nums.Length;
        long sum = 0, result = 0;
        for (int i = 0; i < n; i++)
        {
            sum = 0;
            for (int j = i; j < n; j++)
            {
                sum += nums[j];
                if (sum * (j - i + 1) < k)
                {
                    result++;
                }
            }
        }

        return result;
    }
    
    private long CountSubarrays2(int[] nums, long k)
    {
        if (nums is not { Length: > 0 })
        {
            return 0;
        }

        int n = nums.Length;
        long result = 0, sum = 0;
        for (int i = 0, j = 0; j < n; j++)
        {
            sum += nums[j];
            while (i <= j && sum * (j - i + 1) >= k)
            {
                sum -= nums[i];
                i++;
            }
            result += j - i + 1;
        }

        return result;
    }
}