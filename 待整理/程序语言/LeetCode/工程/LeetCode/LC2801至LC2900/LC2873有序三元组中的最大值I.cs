namespace LeetCode.LC2801至LC2900;

public class LC2873有序三元组中的最大值I
{
    public long MaximumTripletValue(int[] nums, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return MaximumTripletValue1(nums);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }
    
    /// <summary>
    /// 暴力枚举
    /// </summary>
    private long MaximumTripletValue1(int[] nums)
    {
        if (nums is not { Length: > 2 })
        {
            return 0;
        }

        if (nums.Length == 3)
        {
            return Common.Math.Max(0L, GetTripletValue(nums, 0, 1, 2));
        }

        long maxValue = 0;
        int n = nums.Length;

        for (int k = nums.Length - 1; k >= 2; k--)
        {
            for (int j = k - 1; j >= 1; j--)
            {
                for (int i = j - 1; i >= 0; i--)
                {
                    maxValue = Common.Math.Max(maxValue, GetTripletValue(nums, i, j, k));
                }
            }
        }
        
        return maxValue;
    }
    
    /// <summary>
    /// 贪心
    /// </summary>
    private long MaximumTripletValue2(int[] nums)
    {
        if (nums is not { Length: > 2 })
        {
            return 0;
        }

        if (nums.Length == 3)
        {
            return Common.Math.Max(0L, GetTripletValue(nums, 0, 1, 2));
        }

        long maxValue = 0;
        int maxIIndex, n = nums.Length;

        for (int k = 2; k < n; k++)
        {
            maxIIndex = 0;
            for (int j = 1; j < k; j++)
            {
                maxValue = Common.Math.Max(maxValue, GetTripletValue(nums, maxIIndex, j, k));
                if (nums[maxIIndex] < nums[j])
                {
                    maxIIndex = j;
                }
            }
        }
        
        return maxValue;
    }
    
    /// <summary>
    /// 贪心
    /// </summary>
    private long MaximumTripletValue3(int[] nums)
    {
        if (nums is not { Length: > 2 })
        {
            return 0;
        }

        if (nums.Length == 3)
        {
            return Common.Math.Max(0L, GetTripletValue(nums, 0, 1, 2));
        }

        int n = nums.Length;
        int[] leftMax = new int[n], rightMax = new int[n];
        for (int j = 1; j < n; j++)
        {
            leftMax[j] = Common.Math.Max(leftMax[j - 1], nums[j - 1]);
            rightMax[n - j - 1] = Common.Math.Max(rightMax[n - j], nums[n - j]);
        }
        
        long maxValue = 0;
        for (int j = 0; j < n; j++)
        {
            maxValue = Common.Math.Max(maxValue, GetTripletValue(leftMax[j], nums[j], rightMax[j]));
        }
        return maxValue;
    }
    
    /// <summary>
    /// 贪心
    /// </summary>
    private long MaximumTripletValue4(int[] nums)
    {
        if (nums is not { Length: > 2 })
        {
            return 0;
        }

        if (nums.Length == 3)
        {
            return Common.Math.Max(0L, GetTripletValue(nums, 0, 1, 2));
        }

        int n = nums.Length;

        long maxValue = 0, maxNumsI = 0, maxNumsIMinusJ = 0;
        for (int k = 0; k < n; k++)
        {
            maxValue = Common.Math.Max(maxValue, maxNumsIMinusJ * nums[k]);
            maxNumsIMinusJ = Common.Math.Max(maxNumsIMinusJ, maxNumsI - nums[k]);
            maxNumsI = Common.Math.Max(maxNumsI, nums[k]);
        }
        
        return maxValue;
    }
    
    private long GetTripletValue(int[] nums, int i, int j, int k)
    {
        return GetTripletValue(nums[i], nums[j], nums[k]);
    }

    private long GetTripletValue(int i, int j, int k)
    {
        return 1L * (i - j) * k;
    }
}