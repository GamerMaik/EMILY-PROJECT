using System.Collections.Generic;
using UnityEngine;

namespace KC
{
    [System.Serializable]
    //Esta será una referencia de archivo como platilla para ver que cosas queremos guardar o cargar
    public class CharacterSaveData
    {
        [Header("Character Name")]
        public string characterName = "";

        [Header("Body Types")]
        public bool isMale = true;

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

        [Header("Sites of Grace")]
        public SerializableDictionary<int, bool> sitesOfGrace;

        [Header("Bosses")]
        public SerializableDictionary<int, bool> bossesAwakened; //El int representa al Id del jefe  y el bool si los jefes estan despiertos
        public SerializableDictionary<int, bool> bossesDefeated; //El int representa al Id del jefe  y el bool si los jefes estan muertos

        [Header("Items")]
        public SerializableDictionary<int, bool> worldItemsLooted;

        [Header("Inventory")]
        public SerializableDictionary<int, int> inventoryItems;

        [Header("Equipmnet")]
        public int headEquipment;
        public int bodyEquipment;
        public int legEquipment;
        public int handEquipment;

        public int rightWeaponIndex;
        public int rightWeapon01;
        public int rightWeapon02;
        public int rightWeapon03;

        public int leftWeaponIndex;
        public int leftWeapon01;
        public int leftWeapon02;
        public int leftWeapon03;

        public CharacterSaveData()
        {
            sitesOfGrace = new SerializableDictionary<int, bool>();
            bossesAwakened = new SerializableDictionary<int, bool>();
            bossesDefeated = new SerializableDictionary<int, bool>();
            worldItemsLooted = new SerializableDictionary<int, bool>();
            inventoryItems = new SerializableDictionary<int, int>();
        }
    }
}
