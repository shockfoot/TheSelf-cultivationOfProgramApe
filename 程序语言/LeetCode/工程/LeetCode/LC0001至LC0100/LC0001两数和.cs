namespace LeetCode.LC0001至LC0100;

public class LC0001两数和
{
    public int[] TwoSum(int[] nums, int target, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return TwoSum1(nums, target);
            case 2:
                return TwoSum2(nums, target);
            case 3:
                return TwoSum3(nums, target);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private int[] TwoSum1(int[] nums, int target)
    {
        // 输入合法性检查
        if (nums == null || nums.Length < 2)
        {
            return null;
        }

        int n = nums.Length;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                // 同一个数不能多次使用
                if (i == j) continue;
                // 是否满足目标值
                if (nums[i] + nums[j] == target)
                {
                    return new int[] { i, j };
                }
            }
        }

        return null;
    }

    private int[] TwoSum2(int[] nums, int target)
    {
        // 输入合法性检查
        if (nums == null || nums.Length < 2)
        {
            return null;
        }

        int n = nums.Length;
        for (int i = 0; i < n - 1; i++)
        {
            // 仅在当前数后面的数中匹配
            for (int j = i + 1; j < n; j++)
            {
                // 是否满足目标值
                if (nums[i] + nums[j] == target)
                {
                    return new int[] { i, j };
                }
            }
        }

        return null;
    }

    private int[] TwoSum3(int[] nums, int target)
    {
        // 输入合法性检查
        if (nums == null || nums.Length < 2)
        {
            return null;
        }

        int n = nums.Length;
        // 字典映射值和索引
        Dictionary<int, int> num2IndexDic = new Dictionary<int, int>(n);
        for (int i = 0; i < n; i++)
        {
            if (num2IndexDic.ContainsKey(target - nums[i]))
            {
                return new int[] { num2IndexDic[target - nums[i]], i };
            }
            if (!num2IndexDic.ContainsKey(nums[i]))
            {
                num2IndexDic.Add(nums[i], i);
            }
        }

        return null;
    }
}