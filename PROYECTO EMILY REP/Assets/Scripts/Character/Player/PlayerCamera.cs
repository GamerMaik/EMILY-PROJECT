using UnityEngine;
using UnityEngine.SceneManagement;

namespace KC
{
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera instance;
        public  PlayerManager player;
        public Camera cameraObject;
        [SerializeField] Transform cameraPivotTransform;

        [Header("Camera Settings")]
        [Tooltip("Mientras mayor sea el numero, más tiempo le toma a la camara alcanzar al personaje")]
        private float cameraSmoothSpeed = 1; //Mientras mayor sea el numero, más tiempo le toma a la camara alcanzar al personaje
        [SerializeField] float leftAndRightRotationSpeed = 30;
        [SerializeField] float upAndDownRotationSpeed = 30;
        [SerializeField] float minimumPivot = -30; //El punto más bajo que puedes mirar
        [SerializeField] float maximumPivot = 60;  //El punto más alto que puedes mirar
        [SerializeField] float cameraCollisionRadius = 0.2f;
        [SerializeField] LayerMask collideWithLayers;


        [Header("Camera Values")]
        private Vector3 cameraVelocity;
        private Vector3 cameraObjectPosition;  //Mueve el objeto de la camara a esta posicion
        [SerializeField] float leftAndRightLookAngle;
        [SerializeField] float upAndDownLookAngle;
        private float cameraZPosition;
        private float targetCameraZPosition;

        [Header("Look On")]
        [SerializeField] float lockOnRadius = 20;
        [SerializeField] float minimumViewableAngle = -50;
        [SerializeField] float maximumViewableAngle = 50;
        [SerializeField] float maximumLookOnDistance = 20;
        private void Awake()
        {
            if (instance == null)
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
            cameraZPosition = cameraObject.transform.position.z;
        }

        public void HandleAllCameraActions()
        {
             if (player != null)
             {
                //Seguir al jugador
                HandleFollowTarget();
                //Rotar alrededor del jugador
                HandleRotations();
                //Collisionar con objetos
                HandleCollision();
             }
        }

        private void HandleFollowTarget()
        {
            Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
            transform.position = targetCameraPosition;
        }

        private void HandleRotations()
        {
            //si estamos bloqueados, se fuerza la rotacion al objetivo, de lo contrario giras normalmente

            //Rotacion Normal
            //Rotar de izquierda a derecha
            leftAndRightLookAngle += (PlayerInputManager.instance.cameraHorizontalInput * leftAndRightRotationSpeed) * Time.deltaTime;
            //Rotar de Arriba abajo
            upAndDownLookAngle -= (PlayerInputManager.instance.cameraVerticalInput * upAndDownRotationSpeed) * Time.deltaTime;
            //Bloqueo del angulo de camara al minimo y maximo valor
            upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimumPivot, maximumPivot);

            Vector3 camerRotation = Vector3.zero;
            Quaternion targetRotation;

            //Rotar de Izquierda a derecha
            camerRotation.y = leftAndRightLookAngle;
            targetRotation = Quaternion.Euler(camerRotation);
            transform.rotation = targetRotation;

            //Rotar hacia arriba y abajo
            camerRotation = Vector3.zero;
            camerRotation.x = upAndDownLookAngle;
            targetRotation = Quaternion.Euler(camerRotation);

            cameraPivotTransform.localRotation = targetRotation;
        }

        private void HandleCollision()
        {
            targetCameraZPosition = cameraZPosition;

            RaycastHit hit;
            Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
            direction.Normalize();


            //Verificamos si hay un objeto en frente de la camara desde la direccion deseada
            if (Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetCameraZPosition), collideWithLayers))
            {
                //si el objeto esta cerca o lejos y usamos 
                float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
                targetCameraZPosition = -(distanceFromHitObject - cameraCollisionRadius);
            }

            if (Mathf.Abs(targetCameraZPosition)<cameraCollisionRadius)
            {
                targetCameraZPosition = -cameraCollisionRadius;
            }

            cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition, 0.2f);
            cameraObject.transform.localPosition = cameraObjectPosition;
        }

        public void HandleLocatingLockOnTarget()
        {
            float shortDistance = Mathf.Infinity; //se usa para determinar el objetivo más cercano
            float shortDistanceOfRightTargert = Mathf.Infinity;
            float shortDistanceOfLeftTargert = -Mathf.Infinity;

            Collider[] colliders = Physics.OverlapSphere(player.transform.position, lockOnRadius, WorldUtilityManager.Instance.GetCharacterLayers());

            for (int i = 0; i <colliders.Length; i++ )
            {
                CharacterManager lookOnTarget = colliders[i].GetComponent<CharacterManager>();

                if (lookOnTarget != null)
                {
                    //Verificamos si esta dentro de nuestro campo de deteccion
                    Vector3 lookOnTargetDirection = lookOnTarget.transform.position - player.transform.position;
                    float distanceFromTarget = Vector3.Distance(player.transform.position, lookOnTarget.transform.position);
                    float viewableAngle = Vector3.Angle(lookOnTargetDirection, cameraObject.transform.forward);


                    //Comprobamos si nuestro objetivo actual esta muerto para enfocar el siguiente objetivo 
                    if(lookOnTarget.isDead.Value)
                        continue;

                    if (lookOnTarget.transform.root == player.transform.root)
                        continue;

                    if(distanceFromTarget > maximumLookOnDistance)
                        continue;

                    if (viewableAngle > minimumViewableAngle && viewableAngle< maximumViewableAngle)
                    {
                        RaycastHit hit;
                        if (Physics.Linecast(player.playerCombatManager.lookOnTransform.position, lookOnTarget.characterCombatManager.lookOnTransform.position, out hit, WorldUtilityManager.Instance.GetEnviroLayers()))
                        {
                            continue;
                        }
                        else
                        {
                            Debug.Log("Lo hicimos Bien");
                        }
                    }
                }
            }
        }
    }
}
