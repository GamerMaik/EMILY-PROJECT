using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    [SerializeField] bool activeCursor = false;

    private void Update()
    {
        if (activeCursor)
        {
            ShowCursor();
        }
        else
        {
            HideCursor();
        }
    }
    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
