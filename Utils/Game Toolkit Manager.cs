using System;
using System.Collections;
using UnityEngine;

public class GameToolkitManager : MonoBehaviour
{
    /// <summary>
    /// 直接资源加载
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public T TryLoadObject<T>(string path) where T : UnityEngine.Object
    {
        T obj = Resources.Load<T>(path);
        return obj;
    }

    /// <summary>
    /// 异步加载
    /// <returns></returns>
    public void TryLoadObjectAsync<T>(string path, Action<T> onLoad) where T : UnityEngine.Object
    {
        StartCoroutine(LoadObjectAsyncCoroutine<T>(path, onLoad));
    }

    private IEnumerator LoadObjectAsyncCoroutine<T>(string path, Action<T> onLoad) where T : UnityEngine.Object
    {
        ResourceRequest request = Resources.LoadAsync<T>(path);
        yield return request;

        if (request.asset != null)
        {
            onLoad?.Invoke(request.asset as T);
        }
    }

    /// <summary>
    /// 生成物体到场景
    /// </summary>
    public T InstantiateInTheScene<T>(GameObject prefab, Transform parent, 
        Vector3 position, Vector3 eulerAngles, Vector3 scale) where T : MonoBehaviour
    {
        if (!prefab) return null;
        GameObject go = GameObject.Instantiate(prefab, parent);
        go.transform.localPosition      = position;
        go.transform.localEulerAngles   = eulerAngles;
        go.transform.localScale         = scale;
        return go?.GetComponent<T>();
    }
}
