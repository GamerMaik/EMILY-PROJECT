using UnityEngine;

namespace KC
{
    public class CameraSlowMotionManager : MonoBehaviour
    {
        public CameraSlowMotionManager instance;
        [Header("Controls")]
        [SerializeField] float timeScaleAmmount = 0.3f;
        [Header("Debug")]
        public bool isSlowMotionActive = false;

        private void Awake()
        {
            //Solo puede haber uno de estos administradores de juegos guardados del mundo en la escena a la vez, Si existe otro, se destruye
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            if (isSlowMotionActive)
            {
                ActivateSlowMotion();
            }
            else
            {
                DeactivateSlowMotion();
            }
        }

        public void ActivateSlowMotion()
        {
            isSlowMotionActive = true;
            Time.timeScale = timeScaleAmmount;
        }
        public void DeactivateSlowMotion()
        {
            isSlowMotionActive = false;
            Time.timeScale = 1f; // Restaura el tiempo normal
        }
    }
}
