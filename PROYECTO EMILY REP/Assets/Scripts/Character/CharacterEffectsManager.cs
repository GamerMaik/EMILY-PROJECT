using UnityEngine;

namespace KC
{
    public class CharacterEffectsManager : MonoBehaviour
    {
        //EFECTOS INSTANTANEOS (Curacion, recibir da�o)

        //EFECTOS QUE PASAN CON EL PASAR DEL TIEMPO (veneno, nivel de xp)

        //EFECTOS ESTATICOS (A�adir o eliminar mejoras)

        CharacterManager character;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        public virtual void ProccessInstantEffect(InstantCharacterEffect effect)
        {
            effect.ProccessEffect(character);
        }

    }
}
