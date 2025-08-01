using UnityEngine;

public class LoadSaveSystem : MonoBehaviour
{
    public GameObject LoadSavePanel;
    public GameObject SettingsPanel;
    public GameObject NewGamePanel;

    public void Start()
    {
        LoadSavePanel.SetActive(false);
        SettingsPanel.SetActive(false);
        NewGamePanel.SetActive(false);

    }
    public void NavigateLoad(string direction)
    {
        switch (direction)
        {
            case "Load Screen":
                LoadSavePanel.SetActive(true);
                break;

            case "Back":
                LoadSavePanel.SetActive(false);
                break;
        }
    }

    public void NavigateSettings(string direction)
    {
        switch (direction)
        {
            case "Settings":
                SettingsPanel.SetActive(true);
                break;

            case "Back":
                SettingsPanel.SetActive(false);
                break;
        }
    }

    public void NavigateNewGame(string direction)
    {
        switch (direction)
        {
            case "New Game":
                NewGamePanel.SetActive(true);
                break;

            case "Back":
                NewGamePanel.SetActive(false);
                break;
        }
    }
}
