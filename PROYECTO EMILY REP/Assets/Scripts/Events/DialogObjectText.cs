using System.Collections.Generic;
using UnityEngine;

namespace KC
{
    [CreateAssetMenu(fileName = "Dialog Object", menuName = "Dialog/DialogObject")]
    public class DialogObjectText : ScriptableObject
    {
        public string dialogTitle; // Título opcional para la ventana de diálogo
        public List<string> dialogLines; // Lista de líneas de diálogo

        public List<AudioClip> dialogAudioClips; // Lista de clips de audio correspondientes

        // Validación para asegurarse de que las listas tengan la misma longitud
        private void OnValidate()
        {
            if (dialogAudioClips.Count != dialogLines.Count)
            {
                Debug.LogWarning("La cantidad de clips de audio no coincide con la cantidad de líneas de diálogo.");
            }
        }
    }
}
