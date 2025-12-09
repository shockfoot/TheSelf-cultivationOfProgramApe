namespace ZhengxingFarmwork.InputSystem
{
    /// <summary>
    /// 按键触发类型
    /// </summary>
    public enum KeyTrigger
    {
        /// <summary>
        /// 单击
        /// </summary>
        Once,
        /// <summary>
        /// 双击
        /// </summary>
        Double,
        /// <summary>
        /// 持续
        /// </summary>
        Continuity
    }

    /// <summary>
    /// 点击键
    /// </summary>
    [Serializable]
    public class Key
    {
        /// <summary>
        /// 按键名
        /// </summary>
        public string keyName;
        /// <summary>
        /// 按键触发类型
        /// </summary>
        public KeyTrigger trigger;
        /// <summary>
        /// 按键是否被按下
        /// </summary>
        [HideInInspector]
        public bool isDown;
        /// <summary>
        /// 按键是否被双击
        /// </summary>
        [HideInInspector]
        public bool isDoubleDown;
        /// <summary>
        /// 按键是否开始计算第二次被按下
        /// </summary>
        [HideInInspector]
        public bool acceptDooubleDown;
        /// <summary>
        /// 判定双击的间隔
        /// </summary>
        public float pressInterval = 1f;
        /// <summary>
        /// 实际两次按下间隔
        /// </summary>
        [HideInInspector]
        public float realInterval = 1f;
        /// <summary>
        /// 映射的键位
        /// </summary>
        public KeyCode keyCode;
        /// <summary>
        /// 启用
        /// </summary>
        [HideInInspector]
        public bool enable = true;

        /// <summary>
        /// 设置键位
        /// </summary>
        /// <param name="key">要绑定的实际键位</param>
        public void SetKey(KeyCode key)
        {
            keyCode = key;
        }
        /// <summary>
        /// 启用或禁用该键
        /// </summary>
        /// <param name="enable">是否启用</param>
        public void SetEnable(bool isEnable)
        {
            enable = isEnable;
            isDown = false;
            isDoubleDown = false;
        }
    }

    /// <summary>
    /// 值键
    /// </summary>
    [Serializable]
    public class ValueKey
    {
        /// <summary>
        /// 按键名
        /// </summary>
        public string keyName;
        /// <summary>
        /// 按键范围
        /// </summary>
        public Vector2 range = new Vector2(0, 1);
        /// <summary>
        /// 按键值
        /// </summary>
        [HideInInspector]
        public float value;
        /// <summary>
        /// 按键值增加速度
        /// </summary>
        public float addSpeed = 3f;
        /// <summary>
        /// 映射的键位
        /// </summary>
        public KeyCode keyCode;
        /// <summary>
        /// 启用
        /// </summary>
        [HideInInspector]
        public bool enable = true;

        /// <summary>
        /// 设置键位
        /// </summary>
        /// <param name="key">要绑定的实际键位</param>
        public void SetKey(KeyCode key)
        {
            keyCode = key;
        }
        /// <summary>
        /// 启用或禁用该键
        /// </summary>
        /// <param name="enable">是否启用</param>
        public void SetEnable(bool isEnable)
        {
            enable = isEnable;
            value = 0f;
        }
    }

    /// <summary>
    /// 轴键
    /// </summary>
    [Serializable]
    public class AxisKey
    {
        /// <summary>
        /// 按键名
        /// </summary>
        public string keyName;
        /// <summary>
        /// 按键范围
        /// </summary>
        public Vector2 range = new Vector2(-1, 1);
        /// <summary>
        /// 按键值
        /// </summary>
        [HideInInspector]
        public float value;
        /// <summary>
        /// 按键值增加速度
        /// </summary>
        public float addSpeed = 3f;
        /// <summary>
        /// 映射的正键位
        /// </summary>
        public KeyCode posKeyCode;
        /// <summary>
        /// 映射的负键位
        /// </summary>
        public KeyCode negKeyCode;
        /// <summary>
        /// 启用
        /// </summary>
        [HideInInspector]
        public bool enable = true;

        /// <summary>
        /// 设置键位
        /// </summary>
        /// <param name="posKey">要绑定的实际正键位</param>
        /// <param name="negKey">要绑定的实际负键位</param>
        public void SetKey(KeyCode posKey, KeyCode negKey)
        {
            posKeyCode = posKey;
            negKeyCode = negKey;
        }
        /// <summary>
        /// 设置正键位
        /// </summary>
        /// <param name="posKey">要绑定的实际正键位</param>
        public void SetPosKey(KeyCode posKey)
        {
            posKeyCode = posKey;
        }
        /// <summary>
        /// 设置负键位
        /// </summary>
        /// <param name="negKey">要绑定的实际负键位</param>
        public void SetNegKey(KeyCode negKey)
        {
            posKeyCode = posKey;
        }
        /// <summary>
        /// 启用或禁用该键
        /// </summary>
        /// <param name="enable">是否启用</param>
        public void SetEnable(bool isEnable)
        {
            enable = isEnable;
            value = 0f;
        }
    }
}