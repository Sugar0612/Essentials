using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SUG.Essentials
{
    public sealed class SceneManager : MonoBehaviour, IGlobalService, ISceneService
    {
        private string _currScene;

        // Event SceneManager.sceneLoaded和SceneManager.sceneUnloaded的上层接口
        public event Action<UnityEngine.SceneManagement.Scene, LoadSceneMode> sceneLoaded;
        public event Action<UnityEngine.SceneManagement.Scene> sceneUnloaded;

        public string currScene { get => _currScene; set => _currScene = value; }

        private void Awake()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += (s, l) => sceneLoaded?.Invoke(s,l);
            UnityEngine.SceneManagement.SceneManager.sceneUnloaded += (s) => sceneUnloaded?.Invoke(s);
        }

        public void LoadSceneAsync(string scene)
        {
            StartCoroutine(LoadSceneDelayed(scene, LoadSceneMode.Additive));
        }

        private IEnumerator LoadSceneDelayed(string sceneName, LoadSceneMode mode)
        {
            if (!string.IsNullOrEmpty(currScene))
            {
                AsyncOperation unload = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(currScene);
                if (unload != null) yield return unload;
            }

            AsyncOperation load = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, mode);
            yield return load;

            // 通过Path获取Scene名字，将加载场景设置为主场景。
            string sceneShortName = Path.GetFileNameWithoutExtension(sceneName);
            Scene scene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneShortName);
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(scene);

            currScene = sceneName;
        }

        public void UnloadSceneAsync(string scene)
        {
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(currScene);
        }

        public Scene GetActiveScene() => UnityEngine.SceneManagement.SceneManager.GetActiveScene();
    }
}
