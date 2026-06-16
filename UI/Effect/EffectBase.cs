using System;
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

        // —— Event variable —— 
        public event Action onHoverEnter;
        public event Action onHoverExit;
        public event Action onClickEnter;

        // ===================
        // Life cycle
        // ===================
        protected virtual void Start() 
        {
            if (_dependent == null) 
            {
                _dependent = GetComponent<ControlBase>();
                if (_dependent == null) _dependent = gameObject.AddComponent<ControlBase>();
            }

            EventInit();
            //RegisterEvent();
        }

        // ===================
        // Initialized
        // ===================
        protected virtual void EventInit()
        {
            //Debug.Log("Efffect base event init.");
            // onHoverEnter += () => Debug.Log("Effect base hover enter.");
            // onHoverExit  += () => Debug.Log("Effect base hover exit.");
            // onClickEnter += () => Debug.Log("Effect base click enter.");    
        }

        public void RegisterEvent<T>(Action action) where T : EffectBase
        {
            if (_dependent == null) return;

            _dependent.Subscribe<T>(action);
        }
    }
}