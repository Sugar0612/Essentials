using System.Collections.Generic;
using UnityEngine;

namespace SUG_UnityCore.UI
{
    public sealed class UIManager : Singleton<UIManager, SingletonGlobal>
    {
        [Header("UI根节点")]
        [Tooltip("屏幕空间UI的根")]
        public Transform screenRoot;

        [Tooltip("世界空间UI的根")]
        public Transform worldRoot;

        private readonly Dictionary<string, UIBase> _uiCache = new();
        private readonly Dictionary<UILevel, int> _uiSortingOrder = new()
        {
            { UILevel.Background, 0 },
            { UILevel.Normal, 10 },
            { UILevel.Top, 20 },
            { UILevel.Tip, 30 },
        };

        private ILogger logger = new UnityLogger();

        public T OpenUI<T>() where T : UIBase
        {
            string uiName = typeof(T).Name;
            if (_uiCache.TryGetValue(uiName, out var ui)) { ui.Show(); return ui as T; }

            // 加载预制体
            if (!UIPathConfig.UIPaths.TryGetValue(typeof(T), out var prefabPath)) { logger.LogError($"没有 {typeof(T).Name} 的路径！"); return null; }

            GameObject prefab = Resources.Load<GameObject>(prefabPath);
            if (!prefab) { logger.LogError($"UI不存在：{prefabPath}"); return null; }

            UIBase baseUI = prefab.GetComponent<UIBase>();
            if (!baseUI) { logger.LogError($"UI缺少UIBase：{prefabPath}"); return null; }

            Transform parent = baseUI.isWorldUI ? worldRoot : screenRoot;
            GameObject uiObj = Instantiate(prefab, parent);
            uiObj.name = uiName;

            // 层级设置
            int sortingOrder = _uiSortingOrder[baseUI.uiLevel];
            //SetupUILayer(uiObj, sortingOrder);

            T targetUI = uiObj.GetComponent<T>();
            targetUI.Init();
            targetUI.Show();

            _uiCache.Add(uiName, targetUI);
            return targetUI;
        }

        public T OpenUI<T>(string prefabPath) where T : UIBase
        {
            string uiName = typeof(T).Name;
            if (_uiCache.TryGetValue(uiName, out var ui))
            {
                ui.Show();
                return ui as T;
            }

            // 加载预制体
            GameObject prefab = Resources.Load<GameObject>(prefabPath);
            if (!prefab) { logger.LogError($"UI不存在：{prefabPath}"); return null; }

            UIBase baseUI = prefab.GetComponent<UIBase>();
            if (!baseUI) { logger.LogError($"UI缺少UIBase：{prefabPath}"); return null; }

            Transform parent = baseUI.isWorldUI ? worldRoot : screenRoot;
            GameObject uiObj = Instantiate(prefab, parent);
            uiObj.name = uiName;

            // 层级设置
            int sortingOrder = _uiSortingOrder[baseUI.uiLevel];
            //SetupUILayer(uiObj, sortingOrder);

            T targetUI = uiObj.GetComponent<T>();
            targetUI.Init();
            targetUI.Show();

            _uiCache.Add(uiName, targetUI);
            return targetUI;
        }

        /// <summary>
        /// 给UI设置层级，不打断合批
        /// </summary>
        private void SetupUILayer(GameObject uiObj, int sortingOrder)
        {
            // 给当前UI根节点加一个临时Canvas，只控制排序，不打断合批
            Canvas c = uiObj.GetComponent<Canvas>();
            if (!c) c = uiObj.AddComponent<Canvas>();
            c.overrideSorting = true; 
            c.sortingOrder = sortingOrder;

            if (!uiObj.GetComponent<CanvasRenderer>())
                uiObj.AddComponent<CanvasRenderer>();
        }

        public void CloseUI<T>(bool destroy = false) where T : UIBase
        {
            string name = typeof(T).Name;
            if (_uiCache.TryGetValue(name, out var ui))
            {
                ui.Hide();
                if (destroy)
                {
                    Destroy(ui.gameObject);
                    _uiCache.Remove(name);
                }
            }
        }

        public void HideAll()
        {
            foreach (var ui in _uiCache.Values) ui.Hide();
        }

        public void ClearCache() => _uiCache.Clear();
    }
}