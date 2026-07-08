using UnityEngine;

namespace SUG.Essentials
{
    public interface IAudioService
    {
        public void Play(AudioClip clip);
        public AudioClip Play(string path);
        public void Stop();
        public void Pause();
        public AudioClip TryLoadAudioClip(string path);
        public bool IsPlaying();
        public void SetAudioSource(AudioSource source);
    }
}