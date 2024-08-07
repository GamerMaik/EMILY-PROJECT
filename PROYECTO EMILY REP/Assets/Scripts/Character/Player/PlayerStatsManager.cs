using UnityEngine;


namespace KC
{
    public class PlayerStatsManager : CharacterStatManager
    {
        PlayerManager player;
        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }

        protected override void Start()
        {
            base.Start();

            //Calculamos los valores aqui por que cuando creemos el menú de creacion de personajes
            //ajustaremos los valores dependiendo a la clase 
            //
            CalculateHealthBasedOnVitalityLevel(player.playerNetworkManager.vitality.Value);
            CalculateStaminaBasedOnEnduranceLevel(player.playerNetworkManager.endurance.Value);
        }
    }
}
