using UnityEngine;

namespace KC
{
    public class WorldSoundFXManager : MonoBehaviour
    {
        public static WorldSoundFXManager instance;

        [Header("Action SounFX")]
        public AudioClip rollsFX;
        private void Awake()
        {
            if (instance == null )
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
