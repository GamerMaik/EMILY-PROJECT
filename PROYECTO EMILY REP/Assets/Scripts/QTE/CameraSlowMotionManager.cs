using System.Collections;
using UnityEngine;

namespace KC
{
    public class CameraSlowMotionManager : MonoBehaviour
    {
        public static CameraSlowMotionManager instance;
        //[Header("Controls")]
        //[SerializeField] float timeScaleAmmount = 0f;
        [Header("Debug")]
        public bool isSlowMotionActive = false;

        private void Awake()
        {
            //Solo puede haber uno de estos administradores de juegos guardados del mundo en la escena a la vez, Si existe otro, se destruye
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }


        public void ActivateSlowMotion(int timeScale)
        {
            isSlowMotionActive = true;
            Time.timeScale = timeScale;
        }
        public void DeactivateSlowMotion()
        {
            isSlowMotionActive = false;
            Time.timeScale = 1f; // Restaura el tiempo normal
        }

        public void ActiveSlowMotionForTime(float timeScale, int seconds)
        {
            isSlowMotionActive = true;
            Time.timeScale = timeScale;
            StartCoroutine(TimeSlowActive(seconds));
        }
        private IEnumerator TimeSlowActive(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            isSlowMotionActive = false;
            Time.timeScale = 1f;
        }
    }
}
