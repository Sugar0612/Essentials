using UnityEngine;

namespace SUG_UnityCore.UI
{
    public class UIBase : MonoBehaviour
    {
        [Header("UI层级")]
        public UILevel uiLevel;

        [Header("是不是世界空间UI")]
        public bool isWorldUI;

        public virtual void Init() { }
        public virtual void Show() => gameObject.SetActive(true);
        public virtual void Hide() => gameObject.SetActive(false);
        public virtual void Close() { Hide(); Destroy(gameObject); }
    }
}