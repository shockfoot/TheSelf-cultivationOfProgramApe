namespace LeetCode.LC0601至LC0700;

public class LC0682棒球比赛
{
    public int CalPoints(string[] operations, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return CalPoints1(operations);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private int CalPoints1(string[] operations)
    {
        if (operations is not { Length: > 0 })
        {
            return 0;
        }
        
        int[] scores = new int[operations.Length];
        int currentScoreIndex = 0;
        
        int score = 0, n = operations.Length;
        string operation;
        bool isDelete;
        for (int i = 0; i < n; i++)
        {
            operation = operations[i];
            isDelete = false;
            if (!int.TryParse(operation, out int currentScore))
            {
                switch (operation[0])
                {
                    case '+':
                        currentScore = scores[currentScoreIndex - 1] + scores[currentScoreIndex - 2];
                        break;
                    case 'D':
                        currentScore = scores[currentScoreIndex - 1] * 2;
                        break;
                    case 'C':
                        isDelete = true;
                        score -= scores[currentScoreIndex - 1];
                        currentScoreIndex--; 
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            if (isDelete == false)
            {
                score += currentScore;
                scores[currentScoreIndex++] = currentScore;
            }
        }
        
        return score;
    }
}