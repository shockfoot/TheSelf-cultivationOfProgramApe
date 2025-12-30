namespace LeetCode.LC0701至LC0800;

public class LC0709转换成小写字母
{
    public string ToLowerCase(string s, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return ToLowerCase1(s);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private string ToLowerCase1(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return s;
        }
        
        char[] a = s.ToCharArray();
        int distance = 'a' - 'A';
        int n = a.Length;
        for (int i = 0; i < n; i++)
        {
            if (a[i] >= 'A' && a[i] <= 'Z')
            {
                a[i] = (char)(a[i] | distance);
            }
        }
        
        return new string(a);
    }
}