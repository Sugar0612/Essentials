using UnityEngine;
using SUG_UnityCore;
using System;
using UnityEngine.EventSystems;

public abstract class ButtonBase : MonoBehaviour, IInteractive
{
    public event Action onHoverEnter;
    public event Action onHoverExit;
    public event Action onClickEnter;

    public void OnPointerEnter(PointerEventData eventData) => onHoverEnter?.Invoke();
    public void OnPointerExit(PointerEventData eventData) => onHoverExit?.Invoke();
    public void OnPointerClick(PointerEventData eventData) => onClickEnter?.Invoke();
}
