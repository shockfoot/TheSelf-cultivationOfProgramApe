namespace LeetCode.LC3301至LC3400;

public class LC3396使数组元素互不相同所需的最少操作次数
{
    public int MinimumOperations(int[] nums, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return MinimumOperations1(nums);
            case 2:
                return MinimumOperations2(nums);
            case 3:
                return MinimumOperations3(nums);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private int MinimumOperations1(int[] nums)
    {
        if (nums is not { Length: > 1 })
        {
            return 0;
        }

        int times = 0, n = nums.Length;
        for (int i = 0; i < n; i += 3)
        {
            if (CheckUnique(nums, i))
            {
                return times;
            }
            
            times++;
        }
        
        return times;
    }

    private bool CheckUnique(int[] nums, int start)
    {
        int n = nums.Length;
        HashSet<int> set = new HashSet<int>(n - start);
        for (int i = start; i < n; i++)
        {
            if (!set.Add(nums[i]))
            {
                return false;
            }
        }
        
        return true;
    }
    
    private int MinimumOperations2(int[] nums)
    {
        return RemoveAndCheck(nums, 0);
    }

    private int RemoveAndCheck(int[] nums, int times)
    {
        if (nums is not { Length: > 1 })
        {
            return times;
        }
        
        int n = nums.Length;
        if (n - times * 3 <= 0)
        {
            return times;
        }

        int start = times * 3;
        if (n - start <= 0)
        {
            return times;
        }
        
        HashSet<int> set = new HashSet<int>(n - start);
        bool isUnique = true;
        for (int i = start; i < n; i++)
        {
            if (!set.Add(nums[i]))
            {
                isUnique = false;
                break;
            }
        }
        
        return isUnique ? times : RemoveAndCheck(nums, ++times);
    }

    private int MinimumOperations3(int[] nums)
    {
        if (nums is not { Length: > 1 })
        {
            return 0;
        }
        
        int n = nums.Length;
        HashSet<int> set = new HashSet<int>(n);
        for (int i = n - 1; i >= 0; i--)
        {
            if (!set.Add(nums[i]))
            {
                return i / 3 + 1;
            }
        }
        
        return 0;
    }
}