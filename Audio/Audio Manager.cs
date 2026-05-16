using UnityEngine;
using UnityEngine.SceneManagement;

namespace SUG_UnityCore
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : Singleton<AudioManager, SingletonGlobal>
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = gameObject.GetComponent<AudioSource>();
            if (_audioSource == null) _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            _audioSource.loop = false;
        }

        public AudioClip Play(string path)
        {
            AudioClip c = TryLoadAudioClip(path);
            if (c == null) return null;

            Stop();
            _audioSource.clip = c;
            _audioSource.Play();
            return c;
        }

        public void Play(AudioClip c)
        {
            if (c == null) return;

            Stop();
            _audioSource.clip = c;
            _audioSource.Play();
        }

        public void Stop()
        {
            _audioSource.Stop();
            _audioSource.clip = null;
        }

        public void DoSomething() 
        {
            ILogger logger = new UnityLogger();
            logger.Log($"[AudioManager] DoSomething...");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        }

        public AudioClip TryLoadAudioClip(string path) => Resources.Load<AudioClip>(path);

        public bool IsPlaying() => _audioSource.isPlaying;
    }
}