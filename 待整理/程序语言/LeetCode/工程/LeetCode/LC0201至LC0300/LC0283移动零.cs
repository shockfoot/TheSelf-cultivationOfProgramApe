using Common;

namespace LeetCode.LC0201至LC0300;

public class LC0283移动零
{
    public void MoveZeroes(int[] nums, byte way = 1)
    {
        switch (way)
        {
            case 1:
                MoveZeroes1(nums);
                break;
            case 2:
                MoveZeroes2(nums);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private void MoveZeroes1(int[] nums)
    {
        if (nums is not { Length: > 1 })
        {
            return;
        }

        int n = nums.Length;
        for (int i = n - 1; i >= 0; i--)
        {
            if (nums[i] != 0)
            {
                continue;
            }

            for (int j = i + 1; j < n; j++)
            {
                if (nums[j] == 0)
                {
                    break;
                }
                
                nums.Swap(j, j - 1);
            }
        }
    }
    
    private void MoveZeroes2(int[] nums)
    {
        if (nums is not { Length: > 1 })
        {
            return;
        }

        int n = nums.Length, right = 0, left = 0;
        while (right < n)
        {
            if (nums[right] != 0)
            {
                nums.Swap(left, right);
                left++;
            }
            right++;
        }
    }
}