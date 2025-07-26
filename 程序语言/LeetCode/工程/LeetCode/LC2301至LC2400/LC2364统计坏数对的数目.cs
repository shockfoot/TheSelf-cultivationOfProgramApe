namespace LeetCode.LC2301至LC2400;

public class LC2364统计坏数对的数目
{
    public long CountBadPairs(int[] nums, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return CountBadPairs1(nums);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private long CountBadPairs1(int[] nums)
    {
        if (nums is not { Length: > 0 })
        {
            return 0;
        }
        
        long result = 0;
        
        Dictionary<int, int> dict = new ();
        int n = nums.Length, key, count;
        for (int i = 0; i < n; i++)
        {
            count = 0;
            key = nums[i] - i;
            dict.TryGetValue(key, out count);
            // 减去非坏数对
            result += i - count;
            dict[key] = count + 1;
        }
        
        return result;
    }

    private long CountBadPairs2(int[] nums)
    {
        if (nums is not { Length: > 0 })
        {
            return 0;
        }
        
        int n = nums.Length;
        for (int i = 0; i < n; i++)
        {
            nums[i] -= i;
        }
        
        Array.Sort(nums);
        
        long result = 0;
        for (int i = 0, j = 0; j < n; j++)
        {
            if (nums[i] != nums[j])
            {
                i = j;
            }

            result += i;
        }
        
        return result;
    }
}