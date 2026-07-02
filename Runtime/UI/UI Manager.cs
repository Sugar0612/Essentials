using System.Collections.Generic;
using UnityEngine;
using static SUG.Essentials.UIPanelConfigSO;

namespace SUG.Essentials
{
    public sealed class UIManager : MonoBehaviour, IUIService, IGlobalService
    {
        private readonly Dictionary<string, UIBase> _uiCache = new();

        private UIPanelConfigSO _panelCfg;

        public T OpenUI<T>() where T : UIBase
        {
            Debug.Log($"OPEN UI : {typeof(T)}");
            string uiName = typeof(T).Name;

            if (_uiCache.TryGetValue(uiName, out var ui))
            {
                ui.Show();
                return ui as T;
            }

            if (_panelCfg == null) _panelCfg = Essentials.Settings.uiSetting.panelCfg; 
            var cfg  = _panelCfg.uiConfigs.Find(x => x.prefab.name == uiName);
            var prefab = cfg?.prefab;

            if (prefab == null) return null;

            //var uiObj = _context.Create(prefab, "");
            var uiObj = Essentials.Instantiate(prefab);
            var targetUI = uiObj.GetComponent<T>();
            targetUI.Init();
            targetUI.Show();

            _uiCache.Add(uiName, targetUI);

            return targetUI;
        }

        public void CloseUI<T>(bool destroy = false) where T : UIBase
        {
            string name = typeof(T).Name;

            if (_uiCache.TryGetValue(name, out var ui))
            {
                ui.Hide();

                if (destroy)
                {
                    _uiCache.Remove(name);
                }
            }
        }

        public void HideAll()
        {
            foreach (var ui in _uiCache.Values)
                ui.Hide();
        }
    }
}