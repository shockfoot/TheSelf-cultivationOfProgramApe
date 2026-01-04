using System.Numerics;

namespace LeetCode.LC2701至LC2800;

public class LC2716最小化字符串长度
    {
        public int MinimizedStringLength(string s, byte way = 1)
        {
            switch (way)
            {
                case 1:
                    return MaximizedStringLength1(s);
                case 2:
                    return MaximizedStringLength2(s);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private int MaximizedStringLength1(string s)
        {
            if (s.Length <= 1)
            {
                return s.Length;
            }

            HashSet<char> charHash = new HashSet<char>(26);

            foreach (char c in s)
            {
                if (charHash.Contains(c))
                {
                    continue;
                }

                charHash.Add(c);
            }

            return charHash.Count;
        }

        private int MaximizedStringLength2(string s)
        {
            int hash = 0;

            foreach (char c in s)
            {
                hash |= 1 << (c - 'a');
                // c = 'a': c - 'a' = 0, 1 << 0 = 0001, 0000 | 0001 = 0001
                // c = 'b': c - 'a' = 1, 1 << 1 = 0010, 0001 | 0010 = 0011
                // c = 'c': c - 'a' = 2, 1 << 2 = 0100, 0011 | 0100 = 0111
                // c = 'd': c - 'a' = 3, 1 << 3 = 1000, 0111 | 1000 = 1111
            }

            return BitOperations.PopCount((uint)hash);
        }
    }