using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    public static CursorManager instance;
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
