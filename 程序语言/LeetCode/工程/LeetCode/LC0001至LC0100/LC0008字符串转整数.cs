namespace LeetCode.LC0001至LC0100;

public class LC0008字符串转整数
{
    public int MyAtoi(string s, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return MyAtoi1(s);
            case 2:
                return MyAtoi2(s);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }
    
    private int MyAtoi1(string s)
    {
        s = s.Trim();
        // 特殊情况
        if (s.Length == 0) return 0;
        if (!char.IsDigit(s[0]) && s[0] != '-' &&
            s[0] != '+')
            return 0;

        int length = s.Length;
        int rel = 0;
        // 判断正负
        bool isNegative = s[0] == '-';
        // 如果第一个字符是符号则从第二个字符开始转换
        int index = char.IsDigit(s[0]) ? 0 : 1;
        // 如果是数字则进行转换
        while (index < length && char.IsDigit(s[index]))
        {
            // 判断是否溢出
            if (isNegative)
            {
                // 负数溢出
                if (-rel < int.MinValue / 10 || (-rel == int.MinValue / 10 && s[index] > '8'))
                    return int.MinValue;
            }
            else
            {
                // 正数溢出
                if (rel > int.MaxValue / 10 || (rel == int.MaxValue / 10 && s[index] >= '8'))
                    return int.MaxValue;
            }
            // 推进结果位数
            rel = rel * 10 + (s[index++] - '0');
        }
        return isNegative ? -rel : rel;
    }

    private int MyAtoi2(string s)
{
    // 创建状态机
    FSM fsm = new FSM();
    int length = s.Length;
    for (int i = 0; i < length; i++)
    {
        // 逐字符解析
        fsm.GetChar(s[i]);
    }
    // 如果发生溢出则直接返回边界值，否则处理符号
    return fsm.IsOverflow ? fsm.Result : (fsm.Result * fsm.Sign);
}

/// <summary>
/// 有限状态机
/// </summary>
private class FSM
{
    /// <summary>
    /// 状态枚举
    /// </summary>
    private enum EState
    {
        Start,
        Signed,
        Number,
        End
    }

    public readonly char Block = ' ';
    public readonly char NegativeSing = '-';
    public readonly char PositiveSing = '+';

    private int result;
    private int sign;
    /// <summary>
    /// 是否溢出。因为不能之间返回结果，因此设置标记，如果溢出了则result直接存界值，
    /// 后续状态转移不做逻辑处理，结果自带符号
    /// </summary>
    private bool isOverflow;
    private EState currentState;
    private Dictionary<EState, EState[]> map;

    public int Result { get { return result; } }
    public int Sign { get { return sign; } }
    public bool IsOverflow { get { return isOverflow; } }

    public FSM()
    {
        result = 0;
        sign = 1;
        isOverflow = false;
        currentState = EState.Start;
        map = new Dictionary<EState, EState[]>()
        {
            { EState.Start, new EState[] { EState.Start, EState.Signed, EState.Number, EState.End } },
            { EState.Signed, new EState[] { EState.End, EState.End, EState.Number, EState.End } },
            { EState.Number, new EState[] { EState.End, EState.End, EState.Number, EState.End } },
            { EState.End, new EState[] { EState.End, EState.End, EState.End, EState.End } }
        };
    }

    /// <summary>
    /// 解析字符
    /// </summary>
    public void GetChar(char c)
    {
        // 根据字符进入相应的状态
        currentState = map[currentState][GetStateConditionIndex(c)];
        switch (currentState)
        {
            case EState.Signed:
                sign = c == PositiveSing ? 1 : -1;
                break;
            case EState.Number:
                // 没有溢出才进行逻辑处理
                if (isOverflow == false)
                {
                    // 判断是否溢出
                    if (sign == -1)
                    {
                        // 负数溢出
                        if (-result < int.MinValue / 10 || (-result == int.MinValue / 10 && c > '8'))
                        {
                            result = int.MinValue;
                            isOverflow = true;
                            break;
                        }
                    }
                    else
                    {
                        // 正数溢出
                        if (result > int.MaxValue / 10 || (result == int.MaxValue / 10 && c >= '8'))
                        {
                            result = int.MaxValue;
                            isOverflow = true;
                            break;
                        }
                    }
                    // 推进结果位数
                    result = result * 10 + (c - '0');
                }
                break;
        }
    }

    /// <summary>
    /// 获取条件映射索引
    /// </summary>
    private int GetStateConditionIndex(char c)
    {
        int index = 3;
        if (c == Block)
        {
            index = 0;
        }
        else if (c == NegativeSing || c == PositiveSing)
        {
            index = 1;
        }
        else if (char.IsDigit(c))
        {
            index = 2;
        }
        return index;
    }
}
}