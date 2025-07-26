namespace LeetCode.LC0801至LC0900;

public class LC0896单调数列
{
    public bool IsMonotonic(int[] nums, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return IsMonotonic1(nums);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private bool IsMonotonic1(int[] nums)
    {
        if (nums is not { Length: > 0 })
        {
            return false;
        }
        
        bool isIncreasing = true;
        bool isDecreasing = true;
        int n = nums.Length;
        for (int i = 1; i < n; i++)
        {
            if (nums[i] < nums[i - 1])
            {
                isIncreasing = false;
            }
            else if (nums[i] > nums[i - 1])
            {
                isDecreasing = false;
            }
        }
        
        return isIncreasing || isDecreasing;
    }
}