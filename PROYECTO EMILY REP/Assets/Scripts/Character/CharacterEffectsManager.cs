using UnityEngine;

namespace KC
{
    public class CharacterEffectsManager : MonoBehaviour
    {
        //EFECTOS INSTANTANEOS (Curacion, recibir daño)

        //EFECTOS QUE PASAN CON EL PASAR DEL TIEMPO (veneno, nivel de xp)

        //EFECTOS ESTATICOS (Añadir o eliminar mejoras)

        CharacterManager character;
        [Header("VFX")]
        [SerializeField] GameObject bloodSplatterVFX;
        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        public virtual void ProccessInstantEffect(InstantCharacterEffect effect)
        {
            effect.ProccessEffect(character);
        }

        public void PlayBloodSplatterVFX(Vector3 contactPoint)
        {
            if(bloodSplatterVFX != null)
            {
                GameObject bloodSplatter = Instantiate(bloodSplatterVFX, contactPoint, Quaternion.identity);
            }
            else
            {
                GameObject bloodSplatter = Instantiate(WorldCharacterEffectsManager.instance.bloodSplatterVFX, contactPoint, Quaternion.identity);
            }
        }

    }
}
