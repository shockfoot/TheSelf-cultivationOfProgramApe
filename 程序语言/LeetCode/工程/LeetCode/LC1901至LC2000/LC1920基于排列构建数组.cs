namespace LeetCode.LC1901至LC2000;

public class LC1920基于排列构建数组
{
    public int[] BuildArray(int[] nums, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return BuildArray1(nums);
            case 2:
                return BuildArray2(nums);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private int[] BuildArray1(int[] nums)
    {
        if (nums is not { Length: > 1 })
        {
            return nums;
        }

        int n = nums.Length;
        int[] result = new int[n];
        for (int i = 0; i < n; i++)
        {
            result[i] = nums[nums[i]];
        }

        return result;
    }

    private int[] BuildArray2(int[] nums)
    {
        if (nums is not { Length: > 1 })
        {
            return nums;
        }

        int n = nums.Length;
        for (int i = 0; i < n; i++)
        {
            if (nums[i] < 0)
            {
                continue;
            }

            int x = nums[i];
            int cur = i;
            while(nums[cur] != i)
            {
                int next = nums[cur];
                nums[cur] = ~nums[next];
                cur = next;
            }
            nums[cur] = ~x;
        }

        for (int i = 0; i < n; i++)
        {
            nums[i] = ~nums[i];
        }

        return nums;
    }
}