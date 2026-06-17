using UnityEngine;
using SUG_UnityCore;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using TMPro;

namespace SUG_UnityCore
{
    public abstract class ControlBase : Interactable
    {
        public event Action<InteractionTrigger, ControlType> onTrigger;

        public void RaiseTrigger(InteractionTrigger trigger, ControlType type = ControlType.Normal)
        {
            onTrigger?.Invoke(trigger, type);
        }
    }

    public enum InteractionTrigger
    {
        HoverEnter,  // 鼠标移动进入
        HoverExit, // 鼠标移动退出
        DeSelect, // 取消选中
        UnSelctable, // 无法选中/选择失败
        Selected, // 选中/选择成功
    }

    [Flags]
    public enum ControlType
    {
        None = 0, // 无
        Start = 1 << 0, // 开始类型
        Normal = 1 << 1, // 一般类型
    }

    // For example:
    /*
        using SUG_UnityCore;

        public class NormalButton : ControlBase
        {
            // ===================
            // Event
            // ===================
            protected override void OnHoverEnter()
            {
                RaiseTrigger(InteractionTrigger.HoverEnter, ControlType.Normal);
            }

            protected override void OnHoverExit()
            {
                RaiseTrigger(InteractionTrigger.HoverExit, ControlType.Normal);
            }

            protected override void OnClickEnter()
            {
                RaiseTrigger(InteractionTrigger.Selected, ControlType.Normal);
            }
        }

    */
}
