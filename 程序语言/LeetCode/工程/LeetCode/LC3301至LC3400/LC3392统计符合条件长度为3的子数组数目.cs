namespace LeetCode.LC3301至LC3400;

public class LC3392统计符合条件长度为3的子数组数目
{
    public int CountSubarrays(int[] nums)
    {
        if (nums is not { Length: > 0 })
        {
            return 0;
        }
        
        int n = nums.Length;
        int result = 0;
        for (int i = 1; i < n - 1; i++)
        {
            if (nums[i] == (nums[i - 1] + nums[i + 1]) * 2)
            {
                result++;
            }
        }
        
        return result;
    }
}