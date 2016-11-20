using UnityEngine;
using System.Collections;

namespace UU.GameHam
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance {get {return _instance;}}
        private static AudioManager _instance = null;
        private AudioSource audioSource;
        // Use this for initialization
        void Awake()
        {
            _instance = this;
            audioSource = GetComponent<AudioSource>();
        }
        
        public void PlayOneShot (AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}