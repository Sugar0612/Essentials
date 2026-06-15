using UnityEngine;

namespace SUG_UnityCore
{
    /// <summary>
    /// UI特效基类，提供了基本的特效接口，所有UI特效都应该继承自这个类
    /// </summary>
    public abstract class EffectBase : MonoBehaviour
    {
        // —— Runtime variable ——
        private Interactable _dependent;

        // ===================
        // Life cycle
        // ===================
        protected virtual void Start() 
        {
            if (_dependent == null) 
            {
                _dependent = GetComponent<Interactable>();
                if (_dependent == null) _dependent = gameObject.AddComponent<Interactable>();
            }
        }

        // ===================
        // Initialized
        // ===================
        protected virtual void Initialized(Interactable interactable) =>  _dependent = interactable;
    }
}