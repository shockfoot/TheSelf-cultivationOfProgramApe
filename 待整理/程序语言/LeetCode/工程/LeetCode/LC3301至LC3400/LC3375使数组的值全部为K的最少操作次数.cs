namespace LeetCode.LC3301至LC3400;

public class LC3375使数组的值全部为K的最少操作次数
{
    public int MinOperations(int[] nums, int k, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return MinOperations1(nums, k);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private int MinOperations1(int[] nums, int k)
    {
        if (nums is not { Length: > 0 })
        {
            return -1;
        }
        
        int n = nums.Length;
        HashSet<int> numbers = new (n);
        for (int i = 0; i < n; i++)
        {
            if (nums[i] < k)
            {
                return -1;
            }
            else if (nums[i] > k)
            {
                numbers.Add(nums[i]);
            }
        }
        
        return numbers.Count;
    }
}