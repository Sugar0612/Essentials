using System;
using System.Collections.Generic;

namespace SUG_UnityCore.UI
{
    /// <summary>
    /// 所有UI的路径配置，统一管理，再也不用到处写字符串了
    /// </summary>
    public static class UIPathConfig
    {
        public static readonly Dictionary<Type, string> UIPaths = new()
        {
            // ex. { typeof(LoginUI), "UI/LoginUI" },
            { typeof(SelectPanel), "UI/SelectPanel" }
        };
    }
}