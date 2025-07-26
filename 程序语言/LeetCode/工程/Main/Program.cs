using LeetCode.LC0001至LC0100;
using LeetCode.LC2901至LC3000;
using LeetCode.LC3301至LC3400;

namespace Main;

internal class Program
{
    static void Main(string[] args)
    {
        var p = new LC2962统计最大元素出现至少K次的子数组();
        Console.WriteLine(p.CountSubarrays(new int[]
        {
            1, 3, 2, 3, 3
        }, 2, 2));
    }
}