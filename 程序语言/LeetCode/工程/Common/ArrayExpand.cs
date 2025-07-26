namespace Common;

public static class ArrayExpand
{
    public static void Swap<T>(this T[] array, int index1, int index2)
    {
        if (array == null
            || index1 < 0 || index1 >= array.Length
            || index2 < 0 || index2 >= array.Length
            || index1 == index2)
        {
            return;
        }
        
        T temp = array[index1];
        array[index1] = array[index2];
        array[index2] = temp;
    }

    public static int BinarySearch<T>(this T[] array, T key) where T : IComparable<T>
    {
        int rel = -1;
        
        int start = 0, end = array.Length - 1;
        int mid, compareResult;
        while (start <= end)
        {
            // 防止溢出
            mid = start + (end - start) >> 1;
            compareResult = key.CompareTo(array[mid]);
            if (compareResult < 0)
            {
                start = mid + 1;
            }
            else if (compareResult > 0)
            {
                end = mid - 1;
            }
            else
            {
                rel = mid;
                break;
            }
        }
        
        return rel;
    }
}