using System.Globalization;
using UnityEngine;
using Unity.Netcode;
using System;

namespace KC
{
    public class WorldLevelManager : MonoBehaviour
    {
        public static WorldLevelManager instance;
        public NetworkVariable<bool> completeGame = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        [Header("Datos GLobales")]
        [SerializeField] int gameID;
        [SerializeField] Levels currentLevel;
        [SerializeField] int totalDeadths = 0;
        [SerializeField] int totalEnemiesInRoom = 0;
        [SerializeField] int totalQuestions = 0;
        [SerializeField] int totalQuestionsCorrectAnswers = 0;
        [SerializeField] int totalQuestionsIncorrectAnswers = 0;
        [Header("Tiempo")]
        [SerializeField] private TimeSpan timePlayLevel = TimeSpan.Zero; // Tiempo jugado en el nivel actual
        private DateTime? startTime = null; // Momento en que el temporizador del nivel se inició
        private bool isTimerRunning = false;

        [Header("Level Atemps")]
        public NetworkVariable<int> level_01 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> level_02 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> level_03 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> level_04 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> level_05 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> level_06 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header("Level Status")]
        public NetworkVariable<bool> level_01_status = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool> level_02_status = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool> level_03_status = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool> level_04_status = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool> level_05_status = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool> level_06_status = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

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
            LoadDataWorldandLevels();
        }

        public void LoadDataWorldandLevels()
        {

        }

        public void ChangeCurrentLevel(Levels Selectedlevel)
        {
            switch (Selectedlevel) {
                case Levels.Level_01:
                    Debug.Log("Seleccionado level 1");
                    break;
                case Levels.Level_02:
                    Debug.Log("Seleccionado level 2");
                    break;
                case Levels.Level_03:
                    Debug.Log("Seleccionado level 3");
                    break;
                case Levels.Level_04:
                    Debug.Log("Seleccionado level 4");
                    break;
                case Levels.Level_05:
                    Debug.Log("Seleccionado level 5");
                    break;
                case Levels.Level_06:
                    Debug.Log("Seleccionado level 6");
                    break;
            }
        }

        public void AddNumberOfDead()
        {
            totalDeadths ++;
        }
        public void AddEnemiesInRoom(int enemies)
        {
            totalEnemiesInRoom = enemies;
        }

        public void SubstractEnemiesInRoom()
        {
            totalEnemiesInRoom--;
        }

        public bool CheckEnemiesInRoom()
        {
            if (totalEnemiesInRoom > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddCountTotalQuestions()
        {
            totalQuestions++;
        }
        public void AddCountQuestionsAnswers(bool Answer)
        {
            switch(Answer)
            {
                case true:
                    totalQuestionsCorrectAnswers++;
                    break;
                case false:
                    totalQuestionsIncorrectAnswers++;
                    break;
                default:
            }
        }

        public void StartLevelTimer()
        {
            if (isTimerRunning) return; // Evita iniciar el temporizador si ya está corriendo

            startTime = DateTime.Now;
            isTimerRunning = true;
            Debug.Log("Temporizador de nivel iniciado.");
        }

        public void StopLevelTimer()
        {
            if (!isTimerRunning) return; // Evita detener el temporizador si no está corriendo

            if (startTime.HasValue)
            {
                // Calcula el tiempo transcurrido desde el inicio del nivel
                timePlayLevel = DateTime.Now - startTime.Value;
            }

            startTime = null;
            isTimerRunning = false;
            Debug.Log($"Temporizador de nivel detenido. Tiempo del nivel: {FormatTimePlayLevel()}");
        }

        public string FormatTimePlayLevel()
        {
            // Devuelve el tiempo jugado en formato legible
            return $"{timePlayLevel.Hours:D2}h {timePlayLevel.Minutes:D2}m {timePlayLevel.Seconds:D2}s";
        }
    }
}
