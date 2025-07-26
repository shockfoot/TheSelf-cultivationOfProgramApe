namespace LeetCode.LC2901至LC3000;

public class LC2962统计最大元素出现至少K次的子数组
{
    public long CountSubarrays(int[] nums, int k, byte way = 1)
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

    private long CountSubarrays1(int[] nums, int k)
    {
        if (nums is not { Length: > 0 })
        {
            return 0;
        }

        int n = nums.Length;
        int max = nums[0];
        
        for (int i = 1; i < n; i++)
        {
            if (max < nums[i])
            {
                max = nums[i];
            }
        }

        long result = 0;
        int count = 0;
        for (int i = 0; i < n; i++)
        {
            count = 0;
            for (int j = i; j < n; j++)
            {
                if (nums[j] == max)
                {
                    count++;
                }
                
                if (count >= k)
                {
                    result++;
                }
            }
        }

        return result;
    }

    private long CountSubarrays2(int[] nums, int k)
    {
        if (nums is not { Length: > 0 })
        {
            return 0;
        }

        int n = nums.Length;
        int max = nums[0];
        for (int i = 1; i < n; i++)
        {
            if (max < nums[i])
            {
                max = nums[i];
            }
        }
        
        long result = 0;
        int count = 0;
        for (int left = 0, i = 0; i < n; i++)
        {
            if (nums[i] == max)
            {
                count++;
            }

            while (count == k)
            {
                if (nums[left] == max)
                {
                    count--;
                }
                left++;
            }
            
            result += left;
        }

        return result;
    }
}