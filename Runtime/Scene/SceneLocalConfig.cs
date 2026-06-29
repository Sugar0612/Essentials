using System.Collections.Generic;
using SUG.UnityCore;
using UnityEngine;

namespace SUG.UnityCore
{
    public class SceneLocalConfig : ScriptableObject
    {
        public List<UIBase> AutoOpenUITypes = new List<UIBase>();
    }

    [CreateAssetMenu(fileName = "StartSceneConfig", menuName = "Game/Scenes/StartSceneConfig")]
    public class StartSceneConfig : SceneLocalConfig
    {

    }

    [CreateAssetMenu(fileName = "TheorySceneConfig", menuName = "Game/Scenes/TheorySceneConfig")]
    public class TheorySceneConfig : SceneLocalConfig
    {

    }
}