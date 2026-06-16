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
        private Dictionary<Type, Action> _effectDic = new Dictionary<Type, Action>();

        public void Subscribe<T>(Action action) where T : EffectBase
        {
            Type t = typeof(T);
            if (_effectDic.ContainsKey(t))
            {
                _effectDic[t] = action;
                return;
            }

            _effectDic.Add(t, action);
        }

        public void Publish<T>()
        {
            Type t = typeof(T);
            if (_effectDic.ContainsKey(t)) _effectDic[t]?.Invoke();
        }
    }
}
