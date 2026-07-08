using System;
using UnityEngine.SceneManagement;

namespace SUG.Essentials
{
    public interface ISceneService
    {
        public event Action<Scene, LoadSceneMode> sceneLoaded;
        public event Action<Scene> sceneUnloaded;
        public string currScene { get; set; }
        public void LoadSceneAsync(string scene);
        public void UnloadSceneAsync(string scene);

        public Scene GetActiveScene();
    }
}
