using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SUG.UnityCore
{
    /// <summary>
    /// UGUI的UI控件交互接口
    /// </summary>
    public interface IInteractive : IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        
    }
}