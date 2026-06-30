using UnityEngine;

namespace SUG.Essentials
{
    public class UIContext : MonoBehaviour
    {
        public GameObject Create(GameObject prefab, Transform parent)
        {
            var go = Object.Instantiate(prefab, parent);
            return go;
        }

        public void Destroy(GameObject go)
        {
            Object.Destroy(go);
        }
    }
}