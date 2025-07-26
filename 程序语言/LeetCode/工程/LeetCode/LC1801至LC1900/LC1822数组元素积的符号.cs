namespace LeetCode.LC1801至LC1900;

public class LC1822数组元素积的符号
{
    public int ArraySign(int[] nums, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return ArraySign1(nums);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private int ArraySign1(int[] nums)
    {
        if (nums is not { Length: > 0 })
        {
            return 0;
        }
        
        int sign = 1;
        for (int i = nums.Length - 1; i >= 0; i--)
        {
            if (nums[i] == 0)
            {
                return 0;
            }

            if (nums[i] < 0)
            {
                sign = -sign;
            }
        }
        
        return sign;
    }
}