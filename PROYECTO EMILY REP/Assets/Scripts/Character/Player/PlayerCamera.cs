using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

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
        [SerializeField] float LockOnTargetFollowSpeed = 0.2f;
        [SerializeField] float setCameraHeightSpeed = 0.05f; 
        [SerializeField] float unlockedCameraHeight = 1.65f;
        [SerializeField] float lockedCameraHeight = 2.0f;
        private Coroutine cameraLockOnHeightCoroutine;
        private List<CharacterManager> availanbleTargets = new List<CharacterManager>();
        public CharacterManager nearestLockOnTarget;
        public CharacterManager leftLockOnTarget;
        public CharacterManager rightLockOnTarget;

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
            if (player.playerNetworkManager.isLokedOn.Value)
            {
                //Fija la camara en el objetivo
                Vector3 rotationDirection = player.playerCombatManager.currentTarget.characterCombatManager.lookOnTransform.position - transform.position;
                rotationDirection.Normalize();
                rotationDirection.y = 0;
                Quaternion targetRotation = Quaternion.LookRotation(rotationDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, LockOnTargetFollowSpeed);

                //fija la camara en el pivote del objetivo
                rotationDirection = player.playerCombatManager.currentTarget.characterCombatManager.lookOnTransform.position - cameraPivotTransform.position;
                rotationDirection.Normalize();

                targetRotation = Quaternion.LookRotation(rotationDirection);
                cameraPivotTransform.transform.rotation = Quaternion.Slerp(cameraPivotTransform.rotation, targetRotation, LockOnTargetFollowSpeed);

                //Guardamos la rotacion para no hacer cosas extrañas con la camara
                leftAndRightLookAngle = transform.eulerAngles.y;
                upAndDownLookAngle = transform.eulerAngles.x;
            }
            //Rotacion Normal
            else
            {
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


                    if (viewableAngle > minimumViewableAngle && viewableAngle< maximumViewableAngle)
                    {
                        RaycastHit hit;
                        if (Physics.Linecast(player.playerCombatManager.lookOnTransform.position,
                            lookOnTarget.characterCombatManager.lookOnTransform.position,
                            out hit, WorldUtilityManager.Instance.GetEnviroLayers()))
                        {
                            continue;
                        }
                        else
                        {
                            availanbleTargets.Add(lookOnTarget);
                        }
                    }
                }
            }
            //iteramos sobre toda la lista de objetivos potenciales para verificar cual es el mas cercano
            for (int k = 0; k < availanbleTargets.Count; k++)
            {
                if (availanbleTargets[k] != null)
                {
                    float distanceFromTarget = Vector3.Distance(player.transform.position, availanbleTargets[k].transform.position);


                    if (distanceFromTarget < shortDistance)
                    {
                        shortDistance = distanceFromTarget;
                        nearestLockOnTarget = availanbleTargets[k];
                    }
                    //Si ya estamos bloqueados cambiamos entre los objetivos disponibles
                    if (player.playerNetworkManager.isLokedOn.Value)
                    {
                        Vector3 relativeEnemyPosition = player.transform.InverseTransformPoint(availanbleTargets[k].transform.position);
                        var distanceFormLeftTarget = relativeEnemyPosition.x;
                        var distanceFromRightTarget = relativeEnemyPosition.x;

                        if (availanbleTargets[k] == player.playerCombatManager.currentTarget)
                            continue;

                        if (relativeEnemyPosition.x <= 0.00 && distanceFormLeftTarget > shortDistanceOfLeftTargert)
                        {
                            shortDistanceOfLeftTargert = distanceFormLeftTarget;
                            leftLockOnTarget = availanbleTargets[k];
                        }
                        else if (relativeEnemyPosition.x >= 00 && distanceFromRightTarget < shortDistanceOfRightTargert)
                        {
                            shortDistanceOfRightTargert = distanceFromRightTarget;
                            rightLockOnTarget = availanbleTargets[k];
                        }
                    }
                }
                else
                {
                    ClearLockOnTargets();
                    player.playerNetworkManager.isLokedOn.Value = false;
                }
            }
        }

        public void SetLockCameraHeight()
        {
            if (cameraLockOnHeightCoroutine != null)
            {
                StopCoroutine(cameraLockOnHeightCoroutine);
            }
            cameraLockOnHeightCoroutine = StartCoroutine(SetCameraHeight());
        }

        public void ClearLockOnTargets()
        {
            nearestLockOnTarget = null;
            leftLockOnTarget = null;
            rightLockOnTarget = null;
            availanbleTargets.Clear();
        }

        public IEnumerator WaithThenFindNewTarget()
        {
            while (player.isPerformingAction)
            {
                yield return null;
            }

            ClearLockOnTargets();
            HandleLocatingLockOnTarget();

            if (nearestLockOnTarget != null)
            {
                player.playerCombatManager.SetTarget(nearestLockOnTarget);
                player.playerNetworkManager.isLokedOn.Value = true;
            }

            yield return null;
        }

        public IEnumerator SetCameraHeight()
        {
            float duation = 1;
            float timer = 0;

            Vector3 velocity = Vector3.zero;
            Vector3 newLockedCameraHeight = new Vector3(cameraPivotTransform.transform.localPosition.x, lockedCameraHeight);
            Vector3 newUnLockedCameraHeight = new Vector3(cameraPivotTransform.transform.localPosition.x, unlockedCameraHeight);

            while (timer < duation)
            {
                timer += Time.deltaTime;

                if (player != null)
                {
                    if (player.playerCombatManager.currentTarget !=  null)
                    {
                        cameraPivotTransform.transform.localPosition = 
                            Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition, newLockedCameraHeight, ref velocity, setCameraHeightSpeed);
                        cameraPivotTransform.transform.localRotation = 
                            Quaternion.Slerp(cameraPivotTransform.transform.localRotation, Quaternion.Euler(0, 0, 0), LockOnTargetFollowSpeed);
                    }
                    else
                    {
                        cameraPivotTransform.transform.localPosition =
                            Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition, newUnLockedCameraHeight, ref velocity, setCameraHeightSpeed);
                    }
                }
                yield return null;
            }

            if (player != null)
            {
                if (player.playerCombatManager.currentTarget != null)
                {
                    cameraPivotTransform.transform.localPosition = newLockedCameraHeight;
                    cameraPivotTransform.transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    cameraPivotTransform.transform.localPosition = newUnLockedCameraHeight;
                }
            }
            yield return null;
        }
    }
}
