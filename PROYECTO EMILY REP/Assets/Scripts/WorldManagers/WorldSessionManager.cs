using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;

namespace KC
{
    public class WorldSessionManager : MonoBehaviour
    {
        public static WorldSessionManager Instance;
        [Header("Panels Form")]
        [SerializeField] GameObject ViewFormsGlobal;
        [SerializeField] GameObject loginUserPanel;
        [SerializeField] GameObject registerUserPanel;
        [SerializeField] GameObject resetPaswordPanel;

        [Header("Register Panel Inputs")]
        [SerializeField] TMP_InputField registerInputName;
        [SerializeField] TMP_InputField registerInputEmail;
        [SerializeField] TMP_InputField registerInputPassword;
        [SerializeField] TMP_InputField registerInputConfirmPassword;

        [Header("Login Panel Inputs")]
        [SerializeField] TMP_InputField loginInputEmail;
        [SerializeField] TMP_InputField loginInputPassword;

        [Header("Reset Panel Inputs")]
        [SerializeField] TMP_InputField resetInputEmail;

        [Header("Title Menu")]
        [SerializeField] GameObject TittleScrenMainMenu;
        [SerializeField] GameObject NameSpaceObject;
        [SerializeField] TextMeshProUGUI nameText;
        [Space]
        [Header("PLAYER SESION DATA")]
        [SerializeField] public string nameUserSesion = "";
        [SerializeField] public string idUserSession = "";
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
        #region Register
        public void RegisterNewUser()
        {
            var request = new RegisterPlayFabUserRequest
            {
                DisplayName = registerInputName.text,
                Email = registerInputEmail.text,
                Password = registerInputConfirmPassword.text,

                RequireBothUsernameAndEmail = false
            };

            PlayFabClientAPI.RegisterPlayFabUser(request, OnregisterSuccses, OnError);
        }

        private void OnError(PlayFabError error)
        {
            TitleScreenManager.Instance.ShowAlertPopUp("ERROR AL REGISTRAR USUARIO","Registro No realizado\n" + error.ErrorMessage);
            Debug.Log(error.GenerateErrorReport());
        }

        private void OnregisterSuccses(RegisterPlayFabUserResult result)
        {
            TitleScreenManager.Instance.ShowAlertPopUp("REGISTRO EXITOSO", "Registro de usuario se realizó correctamente");
            registerUserPanel.SetActive(false);
            resetPaswordPanel.SetActive(false);
            loginUserPanel.SetActive(true);
            Debug.Log("Registro de usuario Exitoso");
        }
        #endregion

        #region login

        public void LoginUser()
        {
            var request = new LoginWithEmailAddressRequest
            {
                Email = loginInputEmail.text,
                Password = loginInputPassword.text,

                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams()
                {
                    GetPlayerProfile = true,
                    GetPlayerStatistics = true
                }
            };

            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSucces, OnLoginError);
        }

        private void OnLoginError(PlayFabError error)
        {
            TitleScreenManager.Instance.ShowAlertPopUp("ERROR", "Cuenta bloqueada\nComuniquese con el Administrador\n" + error.ErrorMessage);
            ViewFormsGlobal.SetActive(true);
            registerUserPanel.SetActive(false);
            resetPaswordPanel.SetActive(false);
            loginUserPanel.SetActive(true);
            Debug.Log(error.GenerateErrorReport());
        }

        private void OnLoginSucces(LoginResult result)
        {
            TitleScreenManager.Instance.ShowAlertPopUp("Exito", "Se inició Sesion correctamente");
            registerUserPanel.SetActive(false);
            resetPaswordPanel.SetActive(false);
            loginUserPanel.SetActive(false);
            nameUserSesion = result.InfoResultPayload.PlayerProfile.DisplayName;
            idUserSession = result.InfoResultPayload.PlayerProfile.PlayerId;
            nameText.text = "|Usuario: " + result.InfoResultPayload.PlayerProfile.DisplayName+"|";
            StartGameBeforeLogin();

            Debug.Log("Login de usuario Exitoso");
        }

        public void StartGameBeforeLogin()
        {
            TitleScreenManager.Instance.StartNetworkAsHost();
            TittleScrenMainMenu.SetActive(true);
            NameSpaceObject.SetActive(true);
        }
        #endregion

        #region Reset Password
        public void ResetPassword()
        {
            var request = new SendAccountRecoveryEmailRequest
            {
                Email = resetInputEmail.text,
                TitleId = "13A0F"
            };

            PlayFabClientAPI.SendAccountRecoveryEmail(request, OnRecoverySuccess, OnRecoveryError);
        }

        private void OnRecoveryError(PlayFabError error)
        {
            TitleScreenManager.Instance.ShowAlertPopUp("Opss...", "Fallo al restablecer la contraseña\n" + "NO SE ENCONTRÓ EL CORREO");
            Debug.Log(error.GenerateErrorReport());
        }

        private void OnRecoverySuccess(SendAccountRecoveryEmailResult result)
        {
            TitleScreenManager.Instance.ShowAlertPopUp("Exito", "Se envio el enlace de recuperacion al correo");
            registerUserPanel.SetActive(false);
            resetPaswordPanel.SetActive(false);
            loginUserPanel.SetActive(true);
        }
        #endregion

        #region SaveDataPlayer
        public void SaveGameSlotsPlayer(CharacterSlots slot, CharacterSaveData characterData)
        {
            // Generar el JSON de los datos actuales del personaje
            string saveDataJson = JsonUtility.ToJson(characterData);

            // Crear una clave única para el slot
            string key = $"CharacterSaveData_{slot}";

            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                {
                    { key, saveDataJson}
                }
            };
            PlayFabClientAPI.UpdateUserData(request, OnDataSendSucces, OnErrorDataSend);
        }

        private void OnErrorDataSend(PlayFabError error)
        {
            Debug.Log("Error al enviar los datos" + error.ErrorMessage);
        }

        private void OnDataSendSucces(UpdateUserDataResult result)
        {
            Debug.Log("Se enviaron los datos correctamente");
        }
        #endregion

        #region GetGameSlotPlayer
        public void GetSaveGamesForCharacterSlotKey(CharacterSlots slot, System.Action<CharacterSaveData> onDataRetrieved)
        {
            string key = $"CharacterSaveData_{slot}";
            PlayFabClientAPI.GetUserData(new GetUserDataRequest(),
                result =>
                {
                    if (result.Data != null && result.Data.ContainsKey(key))
                    {
                        // Obtener el JSON almacenado
                        string saveDataJson = result.Data[key].Value;

                        // Convertir el JSON a un objeto de tipo CharacterSaveData
                        CharacterSaveData characterData = JsonUtility.FromJson<CharacterSaveData>(saveDataJson);
                        Debug.Log($"Datos cargados para el slot {key} exitosamente.");
                        onDataRetrieved?.Invoke(characterData); // Llamar al callback con los datos cargados
                    }
                    else
                    {
                        Debug.LogWarning($"No se encontraron datos para el slot {key}.");
                        onDataRetrieved?.Invoke(null); // Indicar que no se encontraron datos
                    }
                },
                error =>
                {
                    Debug.LogError("Error al cargar datos de PlayFab: " + error.GenerateErrorReport());
                    onDataRetrieved?.Invoke(null); // Indicar que hubo un error
                });
        }
        #endregion

        #region DeleteGameSlotPlayer
        public void DeleteSlot(CharacterSlots slot)
        {
            string key = $"CharacterSaveData_{slot}";

            PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
            {
                KeysToRemove = new List<string> { key }
            },
            result =>
            {
                Debug.Log($"Datos eliminados para el slot {key}");
                gameObject.SetActive(false); // Oculta el slot en el UI
            },
            error =>
            {
                Debug.LogError($"Error al eliminar datos del slot {key}: {error.GenerateErrorReport()}");
            });
        }
        #endregion

        public void GetQuestionsLevleBasedType(LevelType levelType)
        {
            string key = $"{levelType}Questions";

            PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(),
                result =>
                {
                    if (result.Data != null && result.Data.ContainsKey(key))
                    {
                        string jsonData = result.Data[key];
                        PlayFabQuestionData questionData = JsonUtility.FromJson<PlayFabQuestionData>(jsonData);

                        ShowRandomQuestionsManager.instance.LoadQuestionsFromPlayFabData(questionData);
                        Debug.Log($"Preguntas cargadas para {levelType}");
                    }
                    else
                    {
                        Debug.LogWarning($"No se encontraron preguntas para {levelType}");
                    }
                },
                error =>
                {
                    Debug.LogError($"Error al cargar preguntas de {levelType}: {error.GenerateErrorReport()}");
                });
        }

    }
}
