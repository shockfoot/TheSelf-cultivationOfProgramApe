namespace LeetCode.LC0001至LC0100;

public class LC0058最后一个单词的长度
{
    public int LengthOfLastWord(string s, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return LengthOfLastWord1(s);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private int LengthOfLastWord1(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return 0;
        }

        int count = 0;
        for (int i = s.Length - 1; i >= 0; i--)
        {
            if (s[i] == ' ')
            {
                if (count == 0)
                {
                    continue;
                }
                
                break;
            }
            
            count++;
        }
        
        return count;
    }
}