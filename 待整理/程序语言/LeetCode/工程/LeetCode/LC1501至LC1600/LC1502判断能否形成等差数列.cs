namespace LeetCode.LC1501至LC1600;

public class LC1502判断能否形成等差数列
{
    public bool CanMakeArithmeticProgression(int[] arr, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return CanMakeArithmeticProgression1(arr);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private bool CanMakeArithmeticProgression1(int[] arr)
    {
        if (arr is not { Length: >= 2 })
        {
            return false;
        }
        
        if (arr.Length  == 2)
        {
            return true;
        }
        
        Array.Sort(arr);
        int distance = arr[1] - arr[0];
        int n = arr.Length;
        for (int i = 2; i < n; i++)
        {
            if (arr[i] - arr[i - 1] != distance)
            {
                return false;
            }
        }
        
        return true;
    }
}