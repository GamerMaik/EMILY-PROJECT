using System.Collections.Generic;
using UnityEngine;

namespace KC
{
    public class AdviceManager : MonoBehaviour
    {
        public static AdviceManager instance;

        private void Awake()
        {
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

        private List<string> adviceList = new List<string>
        {
            "Scrum fomenta la colaboraci�n mediante reuniones diarias y retrospectivas.",
            "XP enfatiza la programaci�n en parejas y entregas frecuentes.",
            "TDD te permite construir c�digo confiable y con menos errores.",
            "DSDM se centra en entregas r�pidas con un enfoque iterativo.",
            "Lean Development minimiza desperdicios y promueve entregas r�pidas.",
            "Crystal valora la comunicaci�n y la adaptabilidad seg�n el tama�o del equipo."
        };

        public string SelectRandomListAdvice()
        {
            int randomIndex = Random.Range(0, adviceList.Count);
            return adviceList[randomIndex];
        }
    }
}
