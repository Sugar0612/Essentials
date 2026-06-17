using System;
using JetBrains.Annotations;
using SUG_UnityCore.Event;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SUG_UnityCore
{
    /// <summary>
    /// UI特效基类，提供了基本的特效接口，所有UI特效都应该继承自这个类
    /// </summary>
    public abstract class EffectBase : MonoBehaviour
    {
        // —— Runtime variable ——
        [Header("Dependent object.")]
        [SerializeField] protected ControlBase _dependent;

        // ===================
        // Life cycle
        // ===================
        protected virtual void Start() 
        {
            if (_dependent == null)  _dependent = GetComponent<ControlBase>();
            _dependent.onTrigger += OnTrigger;
        }

        // ===================
        // Initialized
        // ===================
        public virtual void OnTrigger(InteractionTrigger trigger, ControlType types)
        {
            if (trigger == InteractionTrigger.HoverEnter) HoverEnter(types);
            else if (trigger == InteractionTrigger.HoverExit) HoverExit(types);
            else if (trigger == InteractionTrigger.DeSelect) DeSelect(types);
            else if (trigger == InteractionTrigger.Selected) Selected(types);
            else if (trigger == InteractionTrigger.UnSelctable) UnSelectable(types);
        }

        // ===================
        // Virtual interface
        // ===================
        public virtual void DeSelect(ControlType types) { } // 点击取消了
        public virtual void Selected(ControlType types) { } // 点击成功
        public virtual void UnSelectable(ControlType types) { } // 点击失败
        public virtual void HoverEnter(ControlType types) { } // 光标悬浮进入
        public virtual void HoverExit(ControlType types) { } // 光标悬浮退出
    }
}