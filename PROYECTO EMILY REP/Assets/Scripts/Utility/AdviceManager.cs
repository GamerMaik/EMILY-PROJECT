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
            "Crystal valora la comunicaci�n y la adaptabilidad seg�n el tama�o del equipo.",
            "Scrum utiliza el concepto de Sprints para entregar valor incremental al cliente.",
            "XP promueve pruebas automatizadas y mejora continua del c�digo.",
            "TDD facilita la detecci�n temprana de errores a trav�s de pruebas antes de codificar.",
            "DSDM prioriza las necesidades del negocio mediante un enfoque orientado a objetivos.",
            "Lean Development fomenta la mejora continua eliminando tareas que no aportan valor.",
            "Crystal adapta procesos seg�n la criticidad del proyecto y la cultura del equipo.",
            "Kanban utiliza tableros visuales para gestionar el flujo de trabajo de manera eficiente.",
            "FDD se enfoca en construir funcionalidad real a trav�s de peque�os entregables espec�ficos.",
            "Scrum of Scrums coordina equipos grandes distribuidos para mantener la agilidad.",
            "XP incorpora pr�cticas como la integraci�n continua para mantener el c�digo actualizado.",
            "TDD sigue el ciclo de Red-Green-Refactor para desarrollar funcionalidades.",
            "Scrum fomenta la transparencia a trav�s de artefactos como el Burndown Chart.",
            "Scrum emplea roles definidos como Product Owner, Scrum Master y el Equipo de Desarrollo.",
            "DSDM promueve la participaci�n activa del usuario final durante todo el proyecto.",
            "Scrum utiliza el Product Backlog para gestionar y priorizar tareas.",
            "Scrum realiza la planificaci�n del Sprint al inicio de cada ciclo.",
            "XP utiliza historias de usuario para definir requisitos desde la perspectiva del cliente.",
            "FDD sigue un proceso de cinco pasos que incluye modelado, dise�o y construcci�n de caracter�sticas.",
            "XP promueve la propiedad colectiva del c�digo para mejorar la calidad y el mantenimiento.",
            "Crystal enfatiza la importancia de tener un entorno de trabajo c�modo para el equipo.",
            "Lean Development utiliza el mapeo de flujo de valor para identificar y eliminar desperdicios.",
            "DSDM establece tiempos fijos y recursos para asegurar la entrega dentro del presupuesto.",
            "TDD mejora el dise�o del software al forzar una arquitectura basada en pruebas.",
            "Crystal utiliza colores para diferenciar metodolog�as seg�n la complejidad del proyecto (e.g., Crystal Clear, Crystal Orange).",
            "TDD facilita la documentaci�n del comportamiento del sistema mediante pruebas automatizadas.",
            "FDD utiliza un repositorio de caracter�sticas para gestionar y rastrear el progreso del proyecto.",
            "DSDM utiliza el MoSCoW para priorizar requisitos (Must have, Should have, Could have, Won't have).",
            "Lean Development implementa el principio de 'Justo a Tiempo' para optimizar recursos.",
            "Kanban facilita la identificaci�n de cuellos de botella mediante m�tricas visuales.",
            "Lean Development fomenta la calidad incorporada mediante la mejora continua.",
            "Crystal adapta las pr�cticas y procesos seg�n las necesidades espec�ficas del proyecto.",
            "Kanban limita el trabajo en progreso (WIP) para mejorar la eficiencia del flujo.",
            "Kanban promueve la mejora continua a trav�s de revisiones peri�dicas del proceso.", 
            "FDD enfatiza la planificaci�n a nivel de caracter�sticas para mantener el enfoque en el cliente.",
        };

        public string SelectRandomListAdvice()
        {
            int randomIndex = Random.Range(0, adviceList.Count);
            return adviceList[randomIndex];
        }
    }
}
