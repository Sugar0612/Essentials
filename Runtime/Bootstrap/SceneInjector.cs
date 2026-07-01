using UnityEngine;
using UnityEngine.SceneManagement;

namespace SUG.Essentials
{
    internal static class SceneInjector
    {
        public static void Initialize()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            InjectScene(scene);
        }

        private static void InjectScene(Scene scene)
        {
            var roots = scene.GetRootGameObjects();

            foreach (var root in roots)
            {
                var behaviours = root.GetComponentsInChildren<MonoBehaviour>(true);

                foreach (var behaviour in behaviours)
                {
                    Essentials.Inject(behaviour);
                }
            }
        }
    }
}