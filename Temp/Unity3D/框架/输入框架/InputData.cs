namespace ZhengxingFarmwork.InputSystem
{
    [Serializable]
    /// <summary>
    /// 输入设置数据
    /// </summary>
    public class InputData
    {
        /// <summary>
        /// 存储Key的列表
        /// </summary>
        public List<Key> Keys = new List<Key>();
        /// <summary>
        /// 存储ValueKey的列表
        /// </summary>
        public List<ValueKey> ValueKeys = new List<ValueKey>();
        /// <summary>
        /// 存储AxisKey的列表
        /// </summary>
        public List<AxisKey> AxisKeys = new List<AxisKey>();

        /// <summary>
        /// 获取Key
        /// </summary>
        /// <param name="name">要查找的按键名</param>
        public Key GetKeyObject(string name)
        {
            return Keys.Find(key => name.Equals(key.name));
        }
        /// <summary>
        /// 获取ValueKey
        /// </summary>
        /// <param name="name">要查找的按键名</param>
        public ValueKey GetValueKeyObject(string name)
        {
            return ValueKeys.Find(key => name.Equals(key.name));
        }
        /// <summary>
        /// 获取AxisKey
        /// </summary>
        /// <param name="name">要查找的按键名</param>
        public AxisKey GetAxisKeyObject(string name)
        {
            return AxisKeys.Find(key => name.Equals(key.name));
        }

        /// <summary>
        /// 设置Key
        /// </summary>
        /// <param name="name">要设置的按键名</param>
        /// <param name="keyCode">要绑定的实际键位</param>
        public void SetKey(string name, KeyCode keyCode)
        {
            Key key = GetKeyObject(name);
            if (key != null)
            {
                key.SetKey(keyCode);
            }
        }
        /// <summary>
        /// 设置ValueKey
        /// </summary>
        /// <param name="name">要设置的按键名</param>
        /// <param name="keyCode">要绑定的实际键位</param>
        public void SetValueKey(string name, KeyCode keyCode)
        {
            ValueKey valueKey = GetValueKeyObject(name);
            if (valueKey != null)
            {
                valueKey.SetKey(keyCode);
            }
        }
        /// <summary>
        /// 设置AxisKey
        /// </summary>
        /// <param name="name">要设置的按键名</param>
        /// <param name="posKey">要绑定的实际正键位</param>
        /// <param name="negKey">要绑定的实际负键位</param>
        public void SetAxisKey(string name, KeyCode posKey, KeyCode negKey)
        {
            AxisKey axisKey = GetAxisKeyObject(name);
            if (axisKey != null)
            {
                axisKey.SetKey(posKey, negKey);
            }
        }
        /// <summary>
        /// 设置AxisKey的正键位
        /// </summary>
        /// <param name="name">要设置的按键名</param>
        /// <param name="posKey">要绑定的实际正键位</param>
        public void SetAxisPosKey(string name, KeyCode posKey)
        {
            AxisKey axisPosKey = GetAxisKeyObject(name);
            if (axisPosKey != null)
            {
                axisPosKey.SetPosKey(posKey);
            }
        }
        /// <summary>
        /// 设置AxisKey的负键位
        /// </summary>
        /// <param name="name">要设置的按键名</param>
        /// <param name="negKey">要绑定的实际负键位</param>
        public void SetAxisNegKey(string name, KeyCode negKey)
        {
            AxisKey axisNegKey = GetAxisKeyObject(name);
            if (axisNegKey != null)
            {
                axisNegKey.SetNegKey(negKey);
            }
        }

        /// <summary>
        /// 判断Key是否按下
        /// </summary>
        /// <param name="name">按键名</param>
        /// <returns>是否被按下</returns>
        public bool GetKeyDown(string name)
        {
            Key key = GetKeyObject(name);
            if (key != null)
                return key.isDown;
            return false;
        }
        /// <summary>
        /// 判断Key是否双击
        /// </summary>
        /// <param name="name">按键名</param>
        /// <returns>是否被双击</returns>
        public bool GetKeyDownTwice(string name)
        {
            Key key = GetKeyObject(name);
            if (key != null)
                return key.isDoubleDown;
            return false;
        }
        /// <summary>
        /// 获取ValueKey的值
        /// </summary>
        /// <param name="name">按键名</param>
        /// <returns>按键的值</returns>
        public float GetValue(string name)
        {
            ValueKey valueKey = GetValueKeyObject(name);
            if (valueKey != null)
                return valueKey.value;
            return 0f;
        }
        /// <summary>
        /// 获取AxisKey的值
        /// </summary>
        /// <param name="name">按键名</param>
        /// <returns>按键的值</returns>
        public float GetAxis(string name)
        {
            AxisKey axisKey = GetAxisKeyObject(name);
            if (axisKey != null)
                return axisKey.value;
            return 0f;
        }

        /// <summary>
        /// 设置Key的启用
        /// </summary>
        /// <param name="name">按键名</param>
        /// <param name="isEnable">是否启用</param>
        public void SetKeyEnable(string name, bool isEnable)
        {
            Key key = GetKeyObject(name);
            if (key != null)
                key.SetEnable(isEnable);
        }
        /// <summary>
        /// 设置ValueKey的启用
        /// </summary>
        /// <param name="name">按键名</param>
        /// <param name="isEnable">是否启用</param>
        public void SetValueKeyEnable(string name, bool isEnable)
        {
            ValueKey valueKey = GetValueKeyObject(name);
            if (valueKey != null)
                valueKey.SetEnable(isEnable);
        }
        /// <summary>
        /// 设置AxisKey的启用
        /// </summary>
        /// <param name="name">按键名</param>
        /// <param name="isEnable">是否启用</param>
        public void SetAxisKeyEnable(string name, bool isEnable)
        {
            AxisKey axiskey = GetAxisKeyObject(name);
            if (axiskey != null)
                axiskey.SetEnable(isEnable);
        }

        public void AcceptInput()
        {
            UpdateKeys();
            UpdateValueKeys();
            UpdateAxisKeys();
        }
        /// <summary>
        /// 更新Key
        /// </summary>
        private void UpdateKeys()
        {
            for (int i = 0; i < Keys.Count; i++)
            {
                // 启用才检测
                if (Keys[i].enable)
                {
                    Keys[i].isDown = false;
                    Keys[i].isDoubleDown = false;

                    switch(Keys[i].trigger)
                    {
                        case KeyTriggle.Once:
                            if (Input.GetKeyDown(Keys[i].keyCode))
                            {
                                Keys[i].isDown = true;
                            }
                            break;
                        case KeyTriggle.Double:
                            if (Keys[i].acceptDooubleDown)
                            {
                                Keys[i].realInterval += Time.deltaTime;
                                if (Keys[i].realInterval > Keys[i].pressInterval)
                                {
                                    Keys[i].isDoubleDown = false;
                                    Keys[i].realInterval = 0f;
                                }
                                else
                                {
                                    if (Input.GetKeyDown(Keys[i].keyCode))
                                    {
                                        Keys[i].isDoubleDown = true;
                                        Keys[i].realInterval = 0f;
                                    }
                                    else if (Input.GetKeyUp(Keys[i].keyCode)
                                    {
                                        Keys[i].isDoubleDown = false;
                                    }
                                }
                            }
                            else
                            {
                                if (Input.GetKeyUp(Keys[i].keyCode)
                                {
                                    Keys[i].acceptDooubleDown = true;
                                    Keys[i].realInterval = 0f;
                                }
                            }
                            break;
                        case KetTrigger.Continuity:
                            if (Input.GetKey(Keys[i].keyCode))
                            {
                                Keys[i].isDown = true;
                            }
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// 更新ValueKey
        /// </summary>
        private void UpdateValueKeys()
        {
            for (int i = 0; i < ValueKeys.Count; i++)
            {
                if (ValueKeys[i].enable)
                {
                    if (Input.GetKey(ValueKeys[i].keyCode))
                    {
                        ValueKeys[i].value =
                            Mathf.Clamp(ValueKeys[i].value + ValueKeys[i].addSpeed * Time.deltaTime,
                            ValueKeys[i].range.x, ValueKeys[i].range.y);
                    }
                    else
                    {
                        ValueKeys[i].value =
                            Mathf.Clamp(ValueKeys[i].value - ValueKeys[i].addSpeed * Time.deltaTime,
                            ValueKeys[i].range.x, ValueKeys[i].range.y);
                    }
                }
            }
        }
        /// <summary>
        /// 更新AxisKey
        /// </summary>
        private void UpdateAxisKeys()
        {
            for (int i = 0; i < AxisKeys.Count; i++)
            {
                if (AxisKeys[i].enable)
                {
                    if (Input.GetKey(AxisKeys[i].posKeyCode))
                    {
                        AxisKeys[i].value =
                            Mathf.Clamp(AxisKeys[i].value + AxisKeys[i].addSpeed * Time.deltaTime,
                            AxisKeys[i].range.x, AxisKeys[i].range.y);
                    }
                    else if (Input.GetKey(AxisKeys[i].negKeyCode))
                    {
                        AxisKeys[i].value =
                            Mathf.Clamp(AxisKeys[i].value - AxisKeys[i].addSpeed * Time.deltaTime,
                            AxisKeys[i].range.x, AxisKeys[i].range.y);
                    }
                    else
                    {
                        AxisKeys[i].value = Mathf.Lerp(AxisKeys[i].value, 0,
                            AxisKeys[i].addSpeed * Time.deltaTime);
                        if (Mathf.Abs(AxisKeys[i].value) < 0.01f)
                            AxisKeys[i].value = 0f;
                    }
                }
            }
        }

        /// <summary>
        /// 保存键位设置至JSON文件
        /// </summary>
        /// <param name="path">保存路径</param>
        public void SaveInputSetting(string path)
        {
            JsonData json = new JsonData();
            json["Keys"] = new JsonData();
            foreach (Key key in Keys)
            {
                json["Keys"][key.name] = key.keyCode.ToString();
            }
            json["ValueKeys"] = new JsonData();
            foreach (ValueKey valueKey in ValueKeys)
            {
                json["ValueKeys"][valueKey.name] = valueKey.keyCode.ToString();
            }
            json["AxisKeys"] = new JsonData();
            foreach (AxisKeys axisKey in AxisKeys)
            {
                json["AxisKeys"][axisKey.name] = new JsonData();
                json["AxisKeys"][axisKey.name]["Pos"] = axisKey.posKeyCode.ToString();
                json["AxisKeys"][axisKey.name]["Neg"] = axisKey.negKeyCode.ToString();
            }

            string filePath = Application.dataPath + path;
            FileInfo file = new FileInfo(filePath);
            StreamWriter sw = file.CreateText();
            sw.WriteLine(json.ToJson());
            sw.Close();
            sw.Dispose();
        }

        /// <summary>
        /// 从JSON文件加载键位设置
        /// </summary>
        /// <param name="path">保存路径</param>
        public void LoadInputSetting(string path)
        {
            string filePath = Application.dataPath + path;
            if (!File.Exists(filePath))
                return;

            string data = File.ReadAllText(filePath);
            JsonData json = JsonMapper.ToObject(data);
            foreach (Key key in Keys)
            {
                key.keyCode = String2EnumKeyCode(json["Keys"][key.name].ToString());
            }
            foreach (ValueKey valueKey in ValueKeys)
            {
                valueKey.keyCode = String2EnumKeyCode(json["ValueKeys"][valueKey.name].ToString());
            }
            foreach (AxisKeys axisKey in AxisKeys)
            {
                axisKey.posKeyCode = String2EnumKeyCode(json["AxisKeys"][axisKey.name]["Pos"].ToString());
                axisKey.negKeyCode = String2EnumKeyCode(json["AxisKeys"][axisKey.name]["Neg"].ToString());
            }
        }

        /// <summary>
        /// 将字符串转化为KeyCode枚举
        /// </summary>
        /// <param name="key">字符串</param>
        /// <returns>对应KeyCode枚举</returns>
        private KeyCode String2EnumKeyCode(string key)
        {
            return (KeyCode)Enum.Parse(typeof(KeyCode), key);
        }
    }
}