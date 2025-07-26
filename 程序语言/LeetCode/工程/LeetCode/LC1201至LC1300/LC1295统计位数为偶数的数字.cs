namespace LeetCode.LC1201至LC1300;

public class LC1295统计位数为偶数的数字
{
    public int FindNumbers(int[] nums, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return FindNumbers1(nums);
            case 2:
                return FindNumbers2(nums);
            case 3:
                return FindNumbers3(nums);
            case 4:
                return FindNumbers4(nums);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private int FindNumbers1(int[] nums)
    {
        if (nums is not { Length: > 0 })
        {
            return 0;
        }
        
        int result = 0, n = nums.Length, count, temp;
        for (int i = 0; i < n; i++)
        {
            temp = nums[i];
            count = 0;
            while (temp > 0)
            {
                temp /= 10;
                count++;
            }
            if (count % 2 == 0)
            {
                result++;
            }
        }
        
        return result;
    }

    private int FindNumbers2(int[] nums)
    {
        if (nums is not { Length: > 0 })
        {
            return 0;
        }
        
        int result = 0, n = nums.Length;
        string temp;
        for (int i = 0; i < n; i++)
        {
            temp = nums[i].ToString();
            if (temp.Length % 2 == 0)
            {
                result++;
            }
        }
        
        return result;
    }

    private int FindNumbers3(int[] nums)
    {
        if (nums is not { Length: > 0 })
        {
            return 0;
        }
        
        int result = 0, n = nums.Length, count;
        for (int i = 0; i < n; i++)
        {
            count = (int)(Math.Log10(nums[i]) + 1);
            if (count % 2 == 0)
            {
                result++;
            }
        }
        
        return result;
    }

    private int FindNumbers4(int[] nums)
    {
        if (nums is not { Length: > 0 })
        {
            return 0;
        }
        
        int result = 0, n = nums.Length, temp;
        for (int i = 0; i < n; i++)
        {
            temp = nums[i];
            if ((temp >= 10 && temp <= 99)
                || (temp >= 1000 && temp <= 9999)
                || (temp >= 100000 && temp <= 999999))
            {
                result++;
            }
        }
        
        return result;
    }
}