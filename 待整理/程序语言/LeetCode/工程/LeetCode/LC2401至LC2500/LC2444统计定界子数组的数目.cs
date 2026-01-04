namespace LeetCode.LC2401至LC2500;

public class LC2444统计定界子数组的数目
{
    public long CountSubarrays(int[] nums, int minK, int maxK, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return CountSubarrays1(nums, minK, maxK);
            case 2:
                return CountSubarrays2(nums, minK, maxK);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private long CountSubarrays1(int[] nums, int minK, int maxK)
    {
        if (nums is not { Length: > 0 })
        {
            return 0;
        }

        int n = nums.Length, max = 0, min = 0;
        long result = 0;
        for (int i = 0; i < n; i++)
        {
            max = 0;
            min = int.MaxValue;
            for (int j = i; j < n; j++)
            {
                if (max < nums[j])
                {
                    max = nums[j];
                }

                if (min > nums[j])
                {
                    min = nums[j];
                }

                if (min < minK || max > maxK)
                {
                    break;
                }

                if (min == minK && max == maxK)
                {
                    result++;
                }
            }
        }

        return result;
    }

    private long CountSubarrays2(int[] nums, int minK, int maxK)
    {
        if (nums is not { Length: > 0 })
        {
            return 0;
        }

        int n = nums.Length, border = -1, lastMax = -1, lastMin = -1;

        long result = 0;
        for (int i = 0; i < n; i++)
        {
            if (nums[i] < minK || nums[i] > maxK)
            {
                border = i;
                lastMax = -1;
                lastMin = -1;
            }

            if (nums[i] == minK)
            {
                lastMin = i;
            }
            if (nums[i] == maxK)
            {
                lastMax = i;
            }

            if (lastMin != -1 && lastMax != -1)
            {
                result += Math.Min(lastMax, lastMin) - border;
            }
        }

        return result;
    }
}