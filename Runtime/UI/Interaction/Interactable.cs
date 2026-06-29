using UnityEngine;
using SUG.UnityCore;
using System;
using UnityEngine.EventSystems;

namespace SUG.UnityCore
{
    /// <summary>
    /// 可交互的UI控件基类，提供了基本的交互事件，所有可交互UI控件都应该继承自这个类
    /// </summary>
    public abstract class Interactable : MonoBehaviour, IInteractive
    {
        // —— External event interface ——
        public event Action onHoverEnter;
        public event Action onHoverExit;
        public event Action onClickEnter;

        // ==================
        // Interaface achieve
        // ==================
        public void OnPointerEnter(PointerEventData eventData) => onHoverEnter?.Invoke();

        public void OnPointerExit(PointerEventData eventData) => onHoverExit?.Invoke();

        public void OnPointerClick(PointerEventData eventData) => onClickEnter?.Invoke();

        // Interface
        public void OnPointClick() => onClickEnter?.Invoke();
    }
}