using UnityEngine;

namespace SUG.Essentials
{
    public static class RuntimeBridge
    {
        private static GameObject _root;
            
        public static Transform Root { get; private set; }

        public static UIContext UI { get; private set; }

        public static void Initialize()
        {
            if (_root != null) return;

            _root = new GameObject("[Essentials-Runtime]");
            Object.DontDestroyOnLoad(_root);

            var go = new GameObject("[UIContext]");
            go.transform.SetParent(_root.transform);
            UI = go.AddComponent<UIContext>();

            Root = _root.transform;
        }
    }
}