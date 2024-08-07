using UnityEngine;

namespace KC
{
    public class CharacterLocomotionManager : MonoBehaviour
    {
        CharacterManager character;

        [Header("Ground Check & Jumping")]
        [SerializeField] protected float gravityForce = -9.18f;
        [SerializeField] LayerMask groundLayer;
        [SerializeField] float groundCheckSphereRadius = 1;

        [SerializeField] protected Vector3 yVelocity; //La fuerza con la que nuestro jugador será impulsado hacia abajo o hacia arriba
        [SerializeField] protected float groundedYVelocity = -20; //la fuerza con la que nuestro jugador se pega al suelo
        [SerializeField] protected float fallStartYVelocity = -5; //la velocidad que aumenta segun caes
        protected bool fallingVelocityHasBeenSet= false;
        protected float inAirTimer = 0;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
            HandleGrounCheck();
        }

        protected virtual void Update()
        {
            HandleGrounCheck();

            if (character.isGrounded)
            {
                //Si no intentamos saltar entra a la condicion
                if (yVelocity.y < 0)
                {
                    inAirTimer = 0;
                    fallingVelocityHasBeenSet = false;
                    yVelocity.y = groundedYVelocity;
                }
            }
            else
            {
                if (!character.characterNetworkManager.isJumping.Value && !fallingVelocityHasBeenSet)
                {
                    fallingVelocityHasBeenSet = true;
                    yVelocity.y = fallStartYVelocity;
                }

                inAirTimer += Time.deltaTime;
                character.animator.SetFloat("inAirTime", inAirTimer);
                yVelocity.y += gravityForce * Time.deltaTime;
            }
            character.characterController.Move(yVelocity * Time.deltaTime);
            //Debug.Log("Posición del personaje: " + character.transform.position);
        }

        protected void HandleGrounCheck()
        {
            character.isGrounded = Physics.CheckSphere(character.transform.position, groundCheckSphereRadius, groundLayer);
        }

        //private void OnDrawGizmosSelected()
        //{
        //    Gizmos.DrawSphere(character.transform.position, groundCheckSphereRadius);
        //}
    }
}
