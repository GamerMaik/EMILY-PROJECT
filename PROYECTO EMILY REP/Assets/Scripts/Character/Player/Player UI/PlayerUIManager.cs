using Unity.Netcode;
using UnityEngine;

namespace KC
{
    public class PlayerUIManager : MonoBehaviour
    {
        public static PlayerUIManager instance;

        [Header("NETWORK JOIN")]
        [SerializeField] private bool startGameAsClient;
        [HideInInspector] public PlayerUIHudManager playerUIHudManager;
        [HideInInspector] public PlayerUIPopUpManager playerUIPopUpManager;

        [Header("UI Flags")]
        public bool menuWindowIsOpen = false; //Inventario, equipo, otros menus
        public bool popUpWindowIsOpen = false; //Mensajes aleatorios, dialogos etc.

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

            playerUIHudManager = GetComponentInChildren<PlayerUIHudManager>();
            playerUIPopUpManager = GetComponentInChildren<PlayerUIPopUpManager>();
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (startGameAsClient)
            {
                startGameAsClient = false;
                //Primero debemos cerrar la RED como HOST para iniciarla como cliente
                NetworkManager.Singleton.Shutdown();
                //Luego se reinicia, como cliente
                NetworkManager.Singleton.StartClient();
            }
        }
    }
}
