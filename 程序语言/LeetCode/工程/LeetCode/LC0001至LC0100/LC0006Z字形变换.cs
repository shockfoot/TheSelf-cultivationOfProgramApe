using System.Text;

namespace LeetCode.LC0001至LC0100;

public class LC0006Z字形变换
{
    public string Convert(string s, int numRows, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return Convert1(s, numRows);
            case 2:
                return Convert2(s, numRows);
            case 3:
                return Convert3(s, numRows);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }
    
    private string Convert1(string s, int numRows)
    {
        if (string.IsNullOrEmpty(s))
        {
            return s;
        }

        int length = s.Length;
        if (numRows == 1 || numRows >= length || length <= 1)
        {
            return s;
        }
    
        // 因此每个周期需要的字符数
        int period = numRows * 2 - 2;
        // 向上取整，将最后一个周期视作完整周期
        int cols = (length + period - 1) * (numRows - 1) / period;
        char[,] mat = new char[numRows, cols];
        // 遍历字符串
        for (int i = 0, x = 0, y = 0; i < length; i++)
        {
            mat[x, y] = s[i];
            // 当前字符索引取余后小于行数说明还在往下填
            if (i % period < numRows - 1)
            {
                x++;
            }
            // 否则往右上填
            else
            {
                x--;
                y++;
            }
        }

        // 得到结果
        StringBuilder sb = new StringBuilder(length);
        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (mat[i,j] != 0)
                {
                    sb.Append(mat[i, j]);
                }
            }
        }
    
        return sb.ToString();
    }
    
    private string Convert2(string s, int numRows)
    {
        if (string.IsNullOrEmpty(s))
        {
            return s;
        }

        int length = s.Length;
        if (numRows == 1 || numRows >= length || length <= 1)
        {
            return s;
        }

        StringBuilder[] mat = new StringBuilder[numRows];
        // 初始化每行
        for (int i = 0; i < numRows; i++)
        {
            mat[i] = new StringBuilder();
        }
        // 遍历字符串，使用flag记录是否转向
        for (int i = 0, flag = 1, rowIndex = 0; i < length; i++)
        {
            mat[rowIndex].Append(s[i]);
            rowIndex += flag;
            // 行索引到达行数两端时转向
            if (rowIndex == 0 || rowIndex == numRows - 1)
            {
                flag = -flag;
            }
        }

        // 得到结果
        StringBuilder sb = new StringBuilder(length);
        for (int i = 0; i < numRows; i++)
        {
            sb.Append(mat[i].ToString());
        }

        return sb.ToString();
    }
    
    private string Convert3(string s, int numRows)
    {
        if (string.IsNullOrEmpty(s))
        {
            return s;
        }

        int length = s.Length;
        if (numRows == 1 || numRows >= length || length <= 1)
        {
            return s;
        }

        StringBuilder sb = new StringBuilder(length);
        // 每个周期需要的字符数为
        int period = numRows * 2 - 2;
        // 枚举每一行
        for (int i = 0; i < numRows; i++)
        {
            // 枚举每一行的每一个字符
            for (int j = 0; j + i < length; j += period)
            {
                // 添加周期内的第一个字符
                sb.Append(s[j + i]);
                // 中间行添加周期内的第二个字符
                if (i > 0 && i < numRows - 1 && j + period - i < length)
                {
                    sb.Append(s[j + period - i]);
                }
            }
        }

        return sb.ToString();
    }
}