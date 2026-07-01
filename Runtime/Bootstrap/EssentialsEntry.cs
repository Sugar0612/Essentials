using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SUG.Essentials
{
    internal static class EssentialsEntry
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Boot()
        {
            Essentials.Initialize();
        }
    }
}