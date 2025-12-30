namespace Common
{
    public struct Math
    {
        public static int Max(int a, int b)
        {
            return a >= b ? a : b;
        }
        public static long Max(long a, long b)
        {
            return a >= b ? a : b;
        }
        public static float Max(float a, float b)
        {
            return a >= b ? a : b;
        }
        public static double Max(double a, double b)
        {
            return a >= b ? a : b;
        }

        public static int Min(int a, int b)
        {
            return a <= b ? a : b;
        }
        public static long Min(long a, long b)
        {
            return a <= b ? a : b;
        }
        public static float Min(float a, float b)
        {
            return a <= b ? a : b;
        }
        public static double Min(double a, double b)
        {
            return a <= b ? a : b;
        }
    }
}
