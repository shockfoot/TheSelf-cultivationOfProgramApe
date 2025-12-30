namespace LeetCode.LC1501至LC1600;

public class LC1534统计好三元组
{
    public int CountGoodTriplets(int[] arr, int a, int b, int c, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return CountGoodTriplets1(arr, a, b, c);
            case 2:
                return CountGoodTriplets2(arr, a, b, c);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private int CountGoodTriplets1(int[] arr, int a, int b, int c)
    {
        if (arr.Length <= 2)
        {
            return 0;
        }
        
        int count = 0, n = arr.Length;
        for (int i = 0; i < n - 2; i++)
        {
            for (int j = i + 1; j < n - 1; j++)
            {
                for (int k = j + 1; k < n; k++)
                {
                    if (IsGoodTriplet(arr, i, j, k, a, b, c))
                    {
                        count++;
                    }
                }
            }
        }
        
        return count;
    }
    
    private bool IsGoodTriplet(int[] arr, int i, int j, int k, int a, int b, int c)
    {
        int n = arr.Length;
        if (i >= 0 && j > i && k > j && k < n)
        {
            return IsGoodTriplet(arr[i], arr[j], arr[k], a, b, c);
        }
        
        return false;
    }
    
    private bool IsGoodTriplet(int x, int y, int z, int a, int b, int c)
    {
        return Math.Abs(x - y) <= a && Math.Abs(y - z) <= b && Math.Abs(x - z) <= c;
    }
    
    private int CountGoodTriplets2(int[] arr, int a, int b, int c)
    {
        if (arr.Length <= 2)
        {
            return 0;
        }
        
        int count = 0, n = arr.Length;
        int[] sum = new int[1001];
        for (int j = 0; j < n - 1; j++)
        {
            for (int k = j + 1; k < n; k++)
            {
                if (Math.Abs(arr[j] - arr[k]) <= b)
                {
                    int lj = arr[j] - a, rj = arr[j] + a;
                    int lk = arr[k] - c, rk = arr[k] + c;
                    int l = Math.Max(0, Math.Max(lj, lk));
                    int r = Math.Min(1000, Math.Min(rj, rk));
                    if (l <= r)
                    {
                        if (l == 0)
                        {
                            count += sum[r];
                        }
                        else
                        {
                            count += sum[r] - sum[l - 1];
                        }
                    }
                }
            }
            for (int i = arr[j]; i <= 1000; i++)
            {
                sum[i]++;
            }
        }
        
        return count;
    }
}