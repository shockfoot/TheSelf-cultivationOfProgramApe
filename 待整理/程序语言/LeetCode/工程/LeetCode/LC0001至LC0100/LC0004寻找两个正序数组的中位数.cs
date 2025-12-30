namespace LeetCode.LC0001至LC0100;

public class LC0004寻找两个正序数组的中位数
{
    public double FindMedianSortedArrays(int[] nums1, int[] nums2, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return FindMedianSortedArrays1(nums1, nums2);
            case 2:
                return FindMedianSortedArrays2(nums1, nums2);
            case 3:
                return FindMedianSortedArrays3(nums1, nums2);
            case 4:
                return FindMedianSortedArrays4(nums1, nums2);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }
    
    private double FindMedianSortedArrays1(int[] nums1, int[] nums2)
    {
        int m = nums1.Length, n = nums2.Length, l = m + n;
        // 使用两个指针将两数组合并成一个新数组
        int[] total = new int[l];
        int pointer1 = 0, pointer2 = 0;
        for (int i = 0; i < l; i++)
        {
            if (pointer1 < m && (pointer2 >= n || nums1[pointer1] <= nums2[pointer2]))
            {
                total[i] = nums1[pointer1];
                pointer1++;
            }
            else
            {
                total[i] = nums2[pointer2];
                pointer2++;
            }
        }
        
        // 直接定位到中位数
        pointer1 = l / 2;
        double rel = total[pointer1];
        if (l % 2 == 0)
        {
            rel += total[pointer1 - 1];
            rel = rel / 2d;
        }
        
        return rel;
    }
    
    private double FindMedianSortedArrays2(int[] nums1, int[] nums2)
    {
        int m = nums1.Length, n = nums2.Length, l = m + n;
        // 不开辟数组，但需要两个值保存中位数
        int left = 0, right = 0;
        int pointer1 = 0, pointer2 = 0;
        for (int i = 0; i <= l / 2; i++)
        {
            // 每次遍历时记录左边的值
            left = right;
            if (pointer1 < m && (pointer2 >= n || nums1[pointer1] <= nums2[pointer2]))
            {
                right = nums1[pointer1];
                pointer1++;
            }
            else
            {
                right = nums2[pointer2];
                pointer2++;
            }
        }
        
        double rel = right;
        if (l % 2 == 0)
        {
            rel += left;
            rel = rel / 2d;
        }
        
        return rel;
    }
    
    private double FindMedianSortedArrays3(int[] nums1, int[] nums2)
    {
        int m = nums1.Length, n = nums2.Length, l = m + n;
        if (l % 2 == 0)
        {
            int midIndex2 = l / 2, midIndex1 = midIndex2 - 1;
            return (GetKth(nums1, nums2, midIndex1 + 1) + GetKth(nums1, nums2, midIndex2+1)) / 2d;
        }
        else
        {
            int midIndex1 = l / 2;
            return GetKth(nums1, nums2, midIndex1 + 1);
        }
    }

    private int GetKth(int[] nums1, int[] nums2, int k)
    {
        int m = nums1.Length, n = nums2.Length;
        // 已经排除后的索引
        int index1 = 0, index2 = 0;
        while (true)
        {
            // nums1已经全被排除
            if (index1 == m)
            {
                return nums2[index2 + k - 1];
            }
            // nums2已经全被排除
            if (index2 == n)
            {
                return nums1[index1 + k - 1];
            }
            // k为1
            if (k == 1)
            {
                return Math.Min(nums1[index1], nums2[index2]);
            }
            // 正常情况
            int half = k / 2;
            // 下一个比较的索引，需要判断是否越界
            int newIndex1 = Math.Min(index1 + half, m) - 1;
            int newIndex2 = Math.Min(index2 + half, n) - 1;
            int pivot1 = nums1[newIndex1], pivot2 = nums2[newIndex2];
            if (pivot1 <= pivot2)
            {
                k -= newIndex1 - index1 + 1;
                index1 = newIndex1 + 1;
            }
            else
            {
                k -= newIndex2 - index2 + 1;
                index2 = newIndex2 + 1;
            }
        }
    }
    
    private double FindMedianSortedArrays4(int[] nums1, int[] nums2)
    {
        // 保证nums1的长度小于nums2
        if (nums1.Length > nums2.Length)
        {
            return FindMedianSortedArrays4(nums2, nums1);
        }

        int m = nums1.Length, n = nums2.Length, l = m + n;
        int iMin = 0, iMax = m;
        while (iMin <= iMax)
        {
            int i = (iMin + iMax) / 2;
            int j = (m + n + 1) / 2 - i;
            // i要增加
            if (j != 0 && i != m && nums2[j - 1] > nums1[i]) iMin++;
            // i要减小
            else if (i != 0 && j != n && nums1[i - 1] > nums2[j]) iMax--;
            else
            {
                int maxLeft = 0;
                if (i == 0) maxLeft = nums2[j - 1];
                else if (j == 0) maxLeft = nums1[i - 1];
                else maxLeft = Math.Max(nums1[i - 1], nums2[j - 1]);
                // 奇数个只有一个中位数
                if (l % 2 == 1) return maxLeft;

                int minRight = 0;
                if (i == m) minRight = nums2[j];
                else if (j == n) minRight = nums1[i];
                else minRight = Math.Min(nums1[i], nums2[j]);
                return (maxLeft + minRight) / 2d;
            }
        }
        
        return 0;
    }
}