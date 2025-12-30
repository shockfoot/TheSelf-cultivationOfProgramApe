namespace LC0101至LC0200;

public class LC0136只出现一次的数字
{
    public int SingleNumber(int[] nums, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return SingleNumber1(nums);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private int SingleNumber1(int[] nums)
    {
        if (nums == null || nums.Length <= 0)
        {
            return 0;
        }
        
        if (nums.Length == 1)
        {
            return nums[0];
        }

        int rel = 0;
        
        int n = nums.Length;
        for (int i = 0; i < n; i++)
        {
            rel ^= nums[i];
        }

        return rel;
    }
}