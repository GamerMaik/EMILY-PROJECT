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
            "Crystal valora la comunicación y la adaptabilidad según el tamaño del equipo.",
            "Scrum utiliza el concepto de Sprints para entregar valor incremental al cliente.",
            "XP promueve pruebas automatizadas y mejora continua del código.",
            "TDD facilita la detección temprana de errores a través de pruebas antes de codificar.",
            "DSDM prioriza las necesidades del negocio mediante un enfoque orientado a objetivos.",
            "Lean Development fomenta la mejora continua eliminando tareas que no aportan valor.",
            "Crystal adapta procesos según la criticidad del proyecto y la cultura del equipo.",
            "Kanban utiliza tableros visuales para gestionar el flujo de trabajo de manera eficiente.",
            "FDD se enfoca en construir funcionalidad real a través de pequeños entregables específicos.",
            "Scrum of Scrums coordina equipos grandes distribuidos para mantener la agilidad.",
            "XP incorpora prácticas como la integración continua para mantener el código actualizado.",
            "TDD sigue el ciclo de Red-Green-Refactor para desarrollar funcionalidades.",
            "Scrum fomenta la transparencia a través de artefactos como el Burndown Chart.",
            "Scrum emplea roles definidos como Product Owner, Scrum Master y el Equipo de Desarrollo.",
            "DSDM promueve la participación activa del usuario final durante todo el proyecto.",
            "Scrum utiliza el Product Backlog para gestionar y priorizar tareas.",
            "Scrum realiza la planificación del Sprint al inicio de cada ciclo.",
            "XP utiliza historias de usuario para definir requisitos desde la perspectiva del cliente.",
            "FDD sigue un proceso de cinco pasos que incluye modelado, diseño y construcción de características.",
            "XP promueve la propiedad colectiva del código para mejorar la calidad y el mantenimiento.",
            "Crystal enfatiza la importancia de tener un entorno de trabajo cómodo para el equipo.",
            "Lean Development utiliza el mapeo de flujo de valor para identificar y eliminar desperdicios.",
            "DSDM establece tiempos fijos y recursos para asegurar la entrega dentro del presupuesto.",
            "TDD mejora el diseño del software al forzar una arquitectura basada en pruebas.",
            "Crystal utiliza colores para diferenciar metodologías según la complejidad del proyecto (e.g., Crystal Clear, Crystal Orange).",
            "TDD facilita la documentación del comportamiento del sistema mediante pruebas automatizadas.",
            "FDD utiliza un repositorio de características para gestionar y rastrear el progreso del proyecto.",
            "DSDM utiliza el MoSCoW para priorizar requisitos (Must have, Should have, Could have, Won't have).",
            "Lean Development implementa el principio de 'Justo a Tiempo' para optimizar recursos.",
            "Kanban facilita la identificación de cuellos de botella mediante métricas visuales.",
            "Lean Development fomenta la calidad incorporada mediante la mejora continua.",
            "Crystal adapta las prácticas y procesos según las necesidades específicas del proyecto.",
            "Kanban limita el trabajo en progreso (WIP) para mejorar la eficiencia del flujo.",
            "Kanban promueve la mejora continua a través de revisiones periódicas del proceso.", 
            "FDD enfatiza la planificación a nivel de características para mantener el enfoque en el cliente.",
        };

        public string SelectRandomListAdvice()
        {
            int randomIndex = Random.Range(0, adviceList.Count);
            return adviceList[randomIndex];
        }
    }
}
