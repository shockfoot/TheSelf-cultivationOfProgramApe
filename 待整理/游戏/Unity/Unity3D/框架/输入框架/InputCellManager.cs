namespace ZhengxingFarmwork.InputSystem
{
    /// <summary>
    /// InputCell管理器
    /// </summary>
    public class InputCellManager
    {
        /// <summary>
        /// 存储所有InputCell的列表
        /// </summary>
        private static List<InputCell> cells = new List<InputCell>();

        /// <summary>
        /// 添加InputCell
        /// </summary>
        public static void AddCell(InputCell cell)
        {
            if (cells.Contains(cell))
                return;
            cells.Add(cell);
        }

        public static SetAllCellsKeyText()
        {
            foreach (InputCell cell in cells)
            {
                switch (cell.keyType)
                {
                    case KeyType.Key:
                        cell.SetKeyText(InputManager.GetKeyCode(cell.keyName));
                        break;
                    case KeyType.ValueKey:
                        cell.SetKeyText(InputManager.GetValueKeyCode(cell.keyName));
                        break;
                    case KeyType.AxisPosKey:
                        cell.SetKeyText(InputManager.GetAxisPosKeyCode(cell.keyName));
                        break;
                    case KeyType.AxisNegKey:
                        cell.SetKeyText(InputManager.GetAxisNegKeyCode(cell.keyName));
                        break;
                }
            }
        }
    }
}