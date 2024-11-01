using UnityEngine;

namespace KC
{
    public class PlayerBodyManager : MonoBehaviour
    {
        [Header("Hai Object")]
        [SerializeField] public GameObject hair;
        [SerializeField] public GameObject facialHair;

        [Header("Male")]
        [SerializeField] public GameObject maleHead;            //Modelo por defecto de la cabeza
        [SerializeField] public GameObject[] maleBody;          //Modelo por defecto de la parte de arriba (torso, brazo derecho, brazo izquierdo)
        [SerializeField] public GameObject[] maleArms;          //Modelo por defecto de la parte de arriba (antebrazo derecho, antebrazo izquierdo, mano derecha, mano izquierda )
        [SerializeField] public GameObject[] maleLegs;          //Modelo por defecto de la parte de abajo (Pierna derecha y pierna izquierd)
        [SerializeField] public GameObject maleEyebrows;        //Rasgos faciales
        [SerializeField] public GameObject maleFacialhair;      //Rasgos faciales

        [Header("Female")]
        [SerializeField] public GameObject femaleHead;
        [SerializeField] public GameObject[] femaleBody;
        [SerializeField] public GameObject[] femaleArms;
        [SerializeField] public GameObject[] femaleLegs;
        [SerializeField] public GameObject femaleEyebrows;
        public void EnableHead()
        {
            maleHead.SetActive(true);
            femaleHead.SetActive(true);

            maleEyebrows.SetActive(true);
            femaleEyebrows.SetActive(true);
        }
        public void DisableHead()
        {
            maleHead.SetActive(false);
            femaleHead.SetActive(false);

            maleEyebrows.SetActive(false);
            femaleEyebrows.SetActive(false);
        }
        public void EnableHair()
        {
            hair.SetActive(true);
        }
        public void DisableHair()
        {
            hair.SetActive(false);
        }
        public void EnableFacialHair()
        {
            facialHair.SetActive(true);
        }
        public void DisableFacialHair()
        {
            facialHair.SetActive(false);
        }

        public void EnableBody()
        {
            foreach (var model in maleBody)
            {
                model.SetActive(true);
            }
            foreach (var model in femaleBody)
            {
                model.SetActive(true);
            }
        }

        public void DisableBody()
        {
            foreach (var model in maleBody)
            {
                model.SetActive(false);
            }
            foreach (var model in femaleBody)
            {
                model.SetActive(false);
            }
        }
    }
}
