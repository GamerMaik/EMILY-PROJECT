using UnityEngine;

namespace KC
{
    public class CharacterFootStepSFXMaker : MonoBehaviour
    {
        CharacterManager character;

        AudioSource audioSource;
        GameObject steppedOnObject;

        private bool hasTouchedGround = false;
        private bool hasPlayedFootStepSFX = false;

        [SerializeField] float distanceToGround = 0.05f;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            character = GetComponent<CharacterManager>();
        }

        private void FixedUpdate()
        {
            CheckForSteps();
        }

        private void CheckForSteps()
        {
            if (character == null)
                return;

            if (!character.characterNetworkManager.isMoving.Value)
                return;

            RaycastHit hit;

            if(Physics.Raycast(transform.position, character.transform.TransformDirection(Vector3.down),out hit, distanceToGround, WorldUtilityManager.Instance.GetEnviroLayers()))
            {
                hasTouchedGround = true;

                if (!hasPlayedFootStepSFX)
                    steppedOnObject = hit.transform.gameObject;
            }
            else
            {
                hasTouchedGround= false;
                hasPlayedFootStepSFX = false;
                steppedOnObject = null;
            }

            if(hasTouchedGround && !hasPlayedFootStepSFX)
            {
                hasPlayedFootStepSFX= true;
                PlayFootStepSoundFX();
            }
        }

        private void PlayFootStepSoundFX() 
        {
            character.characterSoundFXManager.PlayFootStep();
        }
    }
}
