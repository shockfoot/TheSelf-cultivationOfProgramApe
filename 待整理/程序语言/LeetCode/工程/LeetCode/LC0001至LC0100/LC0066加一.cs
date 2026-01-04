namespace LeetCode.LC0001至LC0100;

public class LC0066加一
{
    public int[] PlusOne(int[] digits, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return PlusOne1(digits);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private int[] PlusOne1(int[] digits)
    {
        if (digits is not { Length: > 0 })
        {
            return digits;
        }

        for (int i = digits.Length - 1; i >= 0; i--)
        {
            if (digits[i] == 9)
            {
                digits[i] = 0;
            }
            else
            {
                digits[i]++;
                return digits;
            }
        }
        
        int[] result = new int[digits.Length + 1];
        result[0] = 1;
        
        return result;
    }
}