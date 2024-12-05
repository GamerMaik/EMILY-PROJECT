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
            "Scrum fomenta la colaboración mediante reuniones diarias y retrospectivas.",
            "XP enfatiza la programación en parejas y entregas frecuentes.",
            "TDD te permite construir código confiable y con menos errores.",
            "DSDM se centra en entregas rápidas con un enfoque iterativo.",
            "Lean Development minimiza desperdicios y promueve entregas rápidas.",
            "Crystal valora la comunicación y la adaptabilidad según el tamaño del equipo."
        };

        public string SelectRandomListAdvice()
        {
            int randomIndex = Random.Range(0, adviceList.Count);
            return adviceList[randomIndex];
        }
    }
}
