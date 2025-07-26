namespace LeetCode.LC0001至LC0100;

public class LC0011盛最多水的容器
{
    public int MaxArea(int[] height, byte way = 1)
    {
        switch(way)
        {
            case 1:
                return MaxArea1(height);
            case 2:
                return MaxArea2(height);
            case 3:
                return MaxArea3(height);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private int MaxArea1(int[] height)
    {
        int rel = 0, length = height.Length;
        for (int i = 0; i < length - 1; i++)
        {
            for (int j = i + 1; j < length; j++)
            {
                rel = Math.Max(rel, Math.Min(height[i], height[j]) * (j - i));
            }
        }
        return rel;
    }
    
    private int MaxArea2(int[] height)
    {
        int length = height.Length, rel = 0, left = 0, right = length - 1;
        while (left < right)
        {
            // 左指针值小时
            if (height[left] <= height[right])
            {
                rel = Math.Max(rel, height[left] * (right - left));
                left++;
            }
            // 右指针值小时
            else
            {
                rel = Math.Max(rel, height[right] * (right - left));
                right--;
            }
            // 用三元运算符
            // rel = height[left] <= height[right] ? Math.Max(rel, (right - left)* height[left++]) :
            // Math.Max(rel, (right - left) * height[right--]);
        }
        return rel;
    }
    
    private int MaxArea3(int[] height)
    {
        int length = height.Length, rel = 0, left = 0, right = length - 1;
        while (left < right)
        {
            if (height[left] <= height[right])
            {
                int shortSlab = height[left];
                rel = Math.Max(rel, shortSlab * (right - left));
                left++;
                while (height[left] <= shortSlab && left < right)
                    left++;
            }
            else
            {
                int shortSlab = height[right];
                rel = Math.Max(rel, shortSlab * (right - left));
                right--;
                while (height[right] <= shortSlab && right > left)
                    right--;
            }
        }
        return rel;
    }
}