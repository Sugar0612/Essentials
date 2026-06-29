using SUG.UnityCore;
using UnityEngine;

namespace SUG.UnityCore
{
    public class TriggerSoundEffect : EffectBase
    {
        // —— Config variable ——
        private UIInteractionSound _soundCfg;

        // =====================
        // Core function
        // =====================
        private AudioClip GetClip(InteractionTrigger trigger, ControlType t)
        {
            foreach (var rule in _soundCfg.rules)
            {
                if (rule == null || rule.trigger != trigger) continue;
                if ((rule.types & t) != 0) return rule.clip;
            }

            return null;
        }

        private void PlayClip(InteractionTrigger trigger, ControlType types)
        {
            if (_soundCfg == null) _soundCfg = ConfigManager.Get().GetConfig<UIInteractionSound>();
            AudioClip c = GetClip(trigger, types);
            AudioManager.Get().Play(c);
        }

        // =====================
        // Override function
        // =====================
        public override void Play() => PlayClip(_currInterTrigger, _currControlType);
    }
}