namespace LeetCode.LC2501至LC2600;

public class LC2563统计公平数对的数目
{
    public long CountFairPairs(int[] nums, int lower, int upper, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return CountFairPairs1(nums, lower, upper);
            case 2:
                return CountFairPairs2(nums, lower, upper);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private long CountFairPairs1(int[] nums, int lower, int upper)
    {
        Array.Sort(nums);

        long result = 0;

        for (int j = 0; j < nums.Length; j++)
        {
            int r = LowerBound(nums, j, upper - nums[j] + 1);
            int l = LowerBound(nums, j, lower - nums[j]);
            result += r - l;
        }

        return result;
    }

    private int LowerBound(int[] nums, int right, int target)
    {
        int left = -1;
        while (left + 1 < right)
        {
            int mid = left + (right - left) / 2;
            if (nums[mid] < target)
            {
                left = mid;
            }
            else
            {
                right = mid;
            }
        }

        return right;
    }

    private long CountFairPairs2(int[] nums, int lower, int upper)
    {
        Array.Sort(nums);

        long result = 0;
        int n = nums.Length;
        int left = n, right = n;

        for (int j = 0; j < nums.Length; j++)
        {
            while (left > 0 && nums[left - 1] >= lower - nums[j])
            {
                left--;
            }
            while (right > 0 && nums[right - 1] > upper - nums[j])
            {
                right--;
            }
            result += Math.Min(right, j) - Math.Min(left, j);
        }

        return result;
    }
}