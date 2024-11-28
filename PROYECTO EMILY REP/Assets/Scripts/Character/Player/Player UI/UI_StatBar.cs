using UnityEngine;
using UnityEngine.UI;

namespace KC
{
    public class UI_StatBar : MonoBehaviour
    {
        protected Slider slider;
        protected RectTransform rectTransform;

        [Header("Bar Options")]
        [SerializeField] protected bool scaleBarLengthWithStats = true;
        [SerializeField] protected float widthScaleMultiplier = 1;
        //aca se implementará el tamaño de la barra dependiendo al nivel de estamina

        protected virtual void Awake()
        {
            slider = GetComponent<Slider>();
            rectTransform = GetComponent<RectTransform>();
        }

        protected virtual void Start()
        {
            
        }

        public virtual void SetStat(float newValue)
        {
            slider.value = newValue;
        }

        public virtual void SetMaxStat(float maxValue)
        {
            slider.maxValue = maxValue;
            slider.value = maxValue;

            if (scaleBarLengthWithStats )
            {
                rectTransform.sizeDelta = new Vector2(maxValue * widthScaleMultiplier, rectTransform.sizeDelta.y);
                //Reposiciona las barras segun su grupo de layouts configurado
                PlayerUIManager.instance.playerUIHudManager.RefreshHUD();
            }
        }


    }
}
