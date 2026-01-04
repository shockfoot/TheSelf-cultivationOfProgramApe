namespace LeetCode.LC0601至LC0700;

public class LC0657机器人能否返回原点
{
    public bool JudgeCircle(string moves, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return JudgeCircle1(moves);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private bool JudgeCircle1(string moves)
    {
        if (string.IsNullOrEmpty(moves))
        {
            return true;
        }
        
        if (moves.Length % 2 != 0)
        {
            return false;
        }
        
        int positionX = 0, positionY = 0;
        foreach (var move in moves)
        {
            switch (move)
            {
                case 'U':
                    positionY++;
                    break;
                case 'D':
                    positionY--;
                    break;
                case 'L':
                    positionX--;
                    break;
                case 'R':
                    positionX++;
                    break;
            }
        }
        
        return positionX == 0 && positionY == 0;
    }
}