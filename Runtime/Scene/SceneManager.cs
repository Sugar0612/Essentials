using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace SUG.Essentials
{
    public sealed class SceneManager : MonoBehaviour, IGlobalService, ISceneManagementService
    {
        private string _currScene;
        public string currScene { get => _currScene; set => _currScene = value; }

        public void LoadSceneAsync(string scene)
        {
            StartCoroutine(LoadSceneDelayed(scene));
        }

        private IEnumerator LoadSceneDelayed(string scene)
        {
            if (currScene != null)
                UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(currScene);

            yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

            currScene = scene;
        }

        public void UnloadSceneAsync(string scene)
        {
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(currScene);
        }
    }
}
