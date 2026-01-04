namespace ZhengxingFarmwork.InputSystem
{
    /// <summary>
    /// 输入系统管理器
    /// </summary>
    public class InputManager
    {
        /// <summary>
        /// 默认输入设置保存位置
        /// </summary>
        private static string m_DefaultSettingPath = "/Resources/DefaultInputSetting.json";
        /// <summary>
        /// 自定义输入设置保存位置
        /// </summary>
        private static string m_CustomSettingPath = "/Resources/CustomInputSetting.json";
        /// <summary>
        /// InputData数据
        /// </summary>
        private static InputData m_InputData;
        /// <summary>
        /// true时正在设置键位
        /// </summary>
        private static bool m_SettingInput;
        /// <summary>
        /// 设置键位的委托
        /// </summary>
        private static Action<KeyCode> SetKeyHandle;
        /// <summary>
        /// 显示键位的委托
        /// </summary>
        private static Action<KeyCode> DisplayKeyHandle;

        public InputManager(InputData inputData)
        {
            m_InputData = inputData;
            SaveDefaultSetting();
            LoadCustomSetting();
        }

        /// <summary>
        /// 更新检测
        /// </summary>
        public void Update()
        {
            m_InputData.AcceptInput();
            if (m_SettingInput)
            {
                foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        if (SetKeyHandle != null)
                            SetKeyHandle(keyCode);
                        if (DisplayKeyHandle != null)
                            DisplayKeyHandle(keyCode);
                        m_SettingInput = false;
                        SetKeyHandle = null;
                        DisplayKeyHandle = null;
                    }
                }
            }
        }

        /// <summary>
        /// 判断Key是否持续按下
        /// </summary>
        /// <param name="name">按键名</param>
        /// <returns>是否持续按下</returns>
        public static bool GetKey(string name)
        {
            return m_InputData.GetKeyDown(name);
        }
        /// <summary>
        /// 判断Key是否按下
        /// </summary>
        /// <param name="name">按键名</param>
        /// <returns>是否按下</returns>
        public static bool GetKeyDown(string name)
        {
            return m_InputData.GetKeyDown(name);
        }
        /// <summary>
        /// 判断Key是否双击
        /// </summary>
        /// <param name="name">按键名</param>
        /// <returns>是否双击</returns>
        public static bool GetKeyDownTwice(string name)
        {
            return m_InputData.GetKeyDownTwice(name);
        }
        /// <summary>
        /// 获取ValueKey的值
        /// </summary>
        /// <param name="name">按键名</param>
        /// <returns>按键的值</returns>
        public static float GetValue(string name)
        {
            return m_InputData.GetValue(name);
        }
        /// <summary>
        /// 获取AxisKey的值
        /// </summary>
        /// <param name="name">按键名</param>
        /// <returns>按键的值</returns>
        public static float GetAxis(string name)
        {
            return m_InputData.GetAxis(name);
        }

        /// <summary>
        /// 设置Key
        /// </summary>
        /// <param name="name">按键名</param>
        /// <param name="keyCode">要绑定的键位</param>
        public static void SetKey(string name, KeyCode keyCode)
        {
            m_InputData.SetKey(name, keyCode);
        }
        /// <summary>
        /// 设置ValueKey
        /// </summary>
        /// <param name="name">按键名</param>
        /// <param name="keyCode">要绑定的键位</param>
        public static void SetValueKey(string name, KeyCode keyCode)
        {
            m_InputData.SetValueKey(name, keyCode);
        }
        /// <summary>
        /// 设置AxisKey
        /// </summary>
        /// <param name="name">按键名</param>
        /// <param name="posKey">要绑定的正键位</param>
        /// <param name="negKey">要绑定的负键位</param>
        public static void SetAxisKey(string name, KeyCode posKey, KeyCode negKey)
        {
            m_InputData.SetAxisKey(name, posKey, negKey);
        }
        /// <summary>
        /// 设置AxisKey的正键位
        /// </summary>
        /// <param name="name">按键名</param>
        /// <param name="posKey">要绑定的正键位</param>
        public static void SetAxisPosKey(string name, KeyCode posKey)
        {
            m_InputData.SetAxisPosKey(name, posKey);
        }
        /// <summary>
        /// 设置AxisKey的负键位
        /// </summary>
        /// <param name="name">按键名</param>
        /// <param name="negKey">要绑定的负键位</param>
        public static void SetAxisNegKey(string name, KeyCode negKey)
        {
            m_InputData.SetAxisNegKey(name, negKey);
        }

        /// <summary>
        /// 开始设置按键
        /// </summary>
        /// <param name="setKey">设置按键的方法</param>
        /// <param name="displayKey">显示按键的方法</param>
        public static void StartSetKey(Action<KeyCode> setKey, Action<KeyCode> displayKey)
        {
            m_SettingInput = true;
            SetKeyHandle = setKey;
            DisplayKeyHandle = displayKey;
        }

        /// <summary>
        /// 保存默认设置
        /// </summary>
        public static void SaveDefaultSetting()
        {
            m_InputData.SaveInputSetting(m_DefaultSettingPath);
        }
        /// <summary>
        /// 加载默认设置
        /// </summary>
        public static void LoadDefaultSetting()
        {
            m_InputData.LoadInputSetting(m_DefaultSettingPath);
        }
        /// <summary>
        /// 保存自定义设置
        /// </summary>
        public static void SaveCustomSetting()
        {
            m_InputData.SaveInputSetting(m_CustomSettingPath);
        }
        /// <summary>
        /// 加载自定义设置
        /// </summary>
        public static void LoadCustomSetting()
        {
            m_InputData.LoadInputSetting(m_CustomSettingPath);
        }

        /// <summary>
        /// 根据按键名获取绑定的Key
        /// </summary>
        /// <param name="name">按键名</param>
        /// <returns>绑定的键位</returns>
        public static KeyCode GetKeyCode(string name)
        {
            Key key = m_InputData.GetKeyObject(name);
            if (key != null)
                return key.keyCode;
            return null;
        }
        /// <summary>
        /// 根据按键名获取绑定的ValueKey
        /// </summary>
        /// <param name="name">按键名</param>
        /// <returns>绑定的键位</returns>
        public static KeyCode GetValueKeyCode(string name)
        {
            ValueKey valueKey = m_InputData.GetValueKeyObject(name);
            if (valueKey != null)
                return valueKey.keyCode;
            return null;
        }
        /// <summary>
        /// 根据按键名获取绑定的AxisKey的正键位
        /// </summary>
        /// <param name="name">按键名</param>
        /// <returns>绑定的键位</returns>
        public static KeyCode GetAxisPosKeyCode(string name)
        {
            AxisKey axisKey = m_InputData.GetAxisKeyObject(name);
            if (axisKey != null)
                return axisKey.posKeyCode;
            return null;
        }
        /// <summary>
        /// 根据按键名获取绑定的AxisKey的负键位
        /// </summary>
        /// <param name="name">按键名</param>
        /// <returns>绑定的键位</returns>
        public static KeyCode GetAxisNegKeyCode(string name)
        {
            AxisKey axisKey = m_InputData.GetAxisKeyObject(name);
            if (axisKey != null)
                return axisKey.negKeyCode;
            return null;
        }
    }
}