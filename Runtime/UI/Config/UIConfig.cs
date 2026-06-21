using System;
using System.Collections.Generic;
using UnityEngine;

namespace SUG_UnityCore
{
    // GUI 交互音效配置文件
    [CreateAssetMenu(fileName = "UI_AudioConfig", menuName = "Game/UI/AudioConfig")]
    public class UIInteractionSound : ScriptableObject
    {
        [Serializable] public class InteractionAudioRule
        {
            public InteractionTrigger trigger;
            public ControlType types;
            public AudioClip clip;
        }
        
        public List<InteractionAudioRule> rules;
    }
}
