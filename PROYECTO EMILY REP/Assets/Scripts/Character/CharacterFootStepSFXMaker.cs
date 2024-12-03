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

        [SerializeField] float distanceToGround = 1f;

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
            Vector3 rayOrigin = transform.position;
            Vector3 rayDirection = character.transform.TransformDirection(Vector3.down);

            // Dibujar varios rayos para depuración
            Debug.DrawRay(rayOrigin, Vector3.down * distanceToGround, Color.green); // Global
            Debug.DrawRay(rayOrigin, rayDirection * distanceToGround, Color.yellow); // Local
            Debug.DrawRay(rayOrigin, rayDirection * distanceToGround, Color.red);
            // Asegúrate de que el rayo impacta
            if (Physics.Raycast(rayOrigin, rayDirection, out hit, distanceToGround, WorldUtilityManager.Instance.GetEnviroLayers()))
            {
                hasTouchedGround = true;

                if (!hasPlayedFootStepSFX)
                    steppedOnObject = hit.transform.gameObject;
            }
            else
            {
                hasTouchedGround = false;
                hasPlayedFootStepSFX = false;
                steppedOnObject = null;
            }

            if (hasTouchedGround && !hasPlayedFootStepSFX)
            {
                hasPlayedFootStepSFX = true;
                PlayFootStepSoundFX();
            }
        }

        private void PlayFootStepSoundFX() 
        {
            character.characterSoundFXManager.PlayFootStep();
        }
    }
}
