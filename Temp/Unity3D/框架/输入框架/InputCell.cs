namespace ZhengxingFarmwork.InputSystem
{
    /// <summary>
    /// 标识UI单元对应的按键类型
    /// </summary>
    public enum KeyType
    {
        Key,
        ValueKey,
        AxisPosKey,
        AxisNegKey
    }

    /// <summary>
    /// 设置输入的UI单元
    /// </summary>
    public class InputCell : MonoBehaviour, IPointerClickHandler
    {
        /// <summary>
        /// 当前UI单元对应的按键类型
        /// </summary>
        public KeyType keyType;
        /// <summary>
        /// 当前UI单元对应的按键名
        /// </summary>
        public string keyName;
        /// <summary>
        /// 当前UI单元下显示KeyCode的Text
        /// </summary>
        private Text keyCodeText;
        /// <summary>
        /// 当前UI单元下显示KeyCode的背景Imgae
        /// </summary>
        private Image keyCodeImage;
        /// <summary>
        /// 设置按键的委托
        /// </summary>
        private Action<KeyCode> SetKey;

        private void Awake()
        {
            InputCellManager.AddCell(this);
            keyCodeImage = GetComponentInChildren<Image>();
            keyCodeText = keyCodeImage.GetComponentInChildren<Text>(); // 此处Text的对象是Image的对象的子物体
        }

        /// <summary>
        /// 点击UI时
        /// </summary>
        public void OnPointerClick(PointerEventData eventData)
        {
            // 提示按下设置的键位
            keyCodeText.text = "Press Key";
            // 根据按键类型调用相应的委托
            switch (keyType)
            {
                case KeyType.Key:
                    SetKey = (key) => InputManager.SetKey(keyName, key);
                    break;
                case KeyType.ValueKey:
                    SetKey = (valueKey) => InputManager.SetValueKey(keyName, valueKey);
                    break;
                case KeyType.AxisPosKey:
                    SetKey = (posKey) => InputManager.SetAxisPosKey(keyName, posKey);
                    break;
                case KeyType.AxisNegKey:
                    SetKey = (negKey) => InputManager.SetAxisNegKey(keyName, negKey);
                    break;
            }

            InputManager.StartSetKey(SetKey, (key) => keyCodeText.text = key.ToString());
        }

        /// <summary>
        /// 设置Text显示的KeyCode
        /// </summary>
        public void SetKeyText(KeyCode keyCode)
        {
            keyCodeText.text = keyCode.ToString();
        }
    }
}