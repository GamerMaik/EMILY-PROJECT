using UnityEngine;

namespace KC
{
    public class AIXtremeSoundFXManager : CharacterSoundFXManager
    {
        [Header("Xtreme Sword Whooshes")]
        public AudioClip[] xtremeSwordWhooshes;

        [Header("xtreme Sword Impact")]
        public AudioClip[] xtremeSwordImpacts;

        [Header("xtreme Stomp Impact")]
        public AudioClip[] xtremeStompImpacts;

        public virtual void PlaySwordImpactSoundFX()
        {
            if (xtremeSwordImpacts.Length > 0)
               PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(xtremeSwordImpacts));
        }
        public virtual void PlayStompImpactSoundFX()
        {
            if (xtremeStompImpacts.Length > 0)
                PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(xtremeSwordImpacts));
        }
    }
}
