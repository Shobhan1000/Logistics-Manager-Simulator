using UnityEngine;

public class ExitAppFunction : MonoBehaviour
{
    public GameObject GUIName;

    public void DisableApp()
    {
        GUIName.SetActive(false);
    }
}
