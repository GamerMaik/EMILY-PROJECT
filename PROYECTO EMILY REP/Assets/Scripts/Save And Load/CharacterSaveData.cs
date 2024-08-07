using UnityEngine;

namespace KC
{
    [System.Serializable]
    //Esta será una referencia de archivo como platilla para ver que cosas queremos guardar o cargar
    public class CharacterSaveData
    {
        [Header("Character Name")]
        public string characterName = "Character";

        [Header("Time Played")]
        public float secondPlayed;

        [Header("World Cordinates")]
        public float xPosition;
        public float yPosition;
        public float zPosition;

        [Header("Resources")]
        public int currentHealth;
        public float currentStamina;

        [Header("Stats")]
        public int vitality;
        public int endurance;
    }
}
