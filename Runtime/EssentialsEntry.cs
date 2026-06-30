using UnityEngine;
using VContainer;

namespace SUG.Essentials
{
    internal static class EssentialsEntry
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Boot()
        {
            Debug.Log("[Essentials] Boot Start");

            Essentials.Initialize();
        }
    }
}