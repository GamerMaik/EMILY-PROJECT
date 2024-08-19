using UnityEngine;

public class TransitionsManager : MonoBehaviour
{
    [SerializeField] GameObject PanelFade;

    public void PanelDesactive()
    {
        PanelFade.SetActive(false);
    }

    public void PanelActive()
    {
        PanelFade.SetActive(true);
    }
}
