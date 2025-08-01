using System;
using System.Collections.Generic;
using BreakInfinity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; // Required for using UI.Image

public class ScriptManager : MonoBehaviour
{
    public Image sr; // Changed from SpriteRenderer to Image
    public List<Sprite> skins = new List<Sprite>();
    private int selectedSkin = 0;
    public GameObject playerskins;

    public Image sr2; // Changed from SpriteRenderer to Image
    public List<Sprite> logos = new List<Sprite>();
    private int selectedLogo = 0;
    public GameObject companylogos;

    public DepotInfo depotInfo;

    public Color normalButtonColor = new Color(0.196f, 0.196f, 0.196f);
    public Color highlightedButtonColor = new Color(0.784f, 0.784f, 0.784f);
    public Color hoverButtonColor = new Color(0.588f, 0.588f, 0.588f);
    private Button selectedDepotButton = null;

    private void Start()
    {
        InitializeDepots();
    }

    // ===============================
    // Depot Initialization & UI
    // ===============================
    private void InitializeDepots()
    {
        depotInfo.depotLocation = new[] { "London", "Birmingham", "Manchester", "Glasgow", "Liverpool", "Cardiff" };

        for (int i = 0; i < depotInfo.depotLocation.Length; i++)
        {
            // Instantiate depot prefab
            Depots depotInstance = Instantiate(depotInfo.DepotPrefab, depotInfo.DepotPanel.transform);
            depotInstance.DepotID = i;
            depotInfo.Depots.Add(depotInstance);

            // Set depot name
            depotInstance.DepotLocationText.text = depotInfo.depotLocation[i];
            depotInstance.gameObject.SetActive(true);

            // Setup button
            Button depotButton = depotInstance.GetComponentInChildren<Button>();
            if (depotButton != null)
            {
                // Add hover effects via EventTrigger
                AddHoverEffect(depotButton);

                int capturedIndex = i;
                depotButton.onClick.AddListener(() =>
                {
                    HandleDepotButtonClick(depotButton);
                });

                SetButtonColor(depotButton, normalButtonColor);
            }
        }
    }

    private void HandleDepotButtonClick(Button clickedButton)
    {
        if (selectedDepotButton != null)
        {
            SetButtonColor(selectedDepotButton, normalButtonColor);
        }

        SetButtonColor(clickedButton, highlightedButtonColor);
        selectedDepotButton = clickedButton;
    }

    private void SetButtonColor(Button button, Color color)
    {
        ColorBlock cb = button.colors;
        cb.normalColor = color;
        cb.highlightedColor = color;
        cb.selectedColor = color;
        button.colors = cb;
    }

    private void AddHoverEffect(Button button)
    {
        // Add an EventTrigger to handle mouse hover
        EventTrigger eventTrigger = button.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entryEnter = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
        entryEnter.callback.AddListener((eventData) => { OnMouseEnter(button); });

        EventTrigger.Entry entryExit = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit
        };
        entryExit.callback.AddListener((eventData) => { OnMouseExit(button); });

        eventTrigger.triggers.Add(entryEnter);
        eventTrigger.triggers.Add(entryExit);
    }

    private void OnMouseEnter(Button button)
    {
        if (selectedDepotButton != button)
        {
            SetButtonColor(button, hoverButtonColor);
        }
    }

    private void OnMouseExit(Button button)
    {
        if (selectedDepotButton != button)
        {
            SetButtonColor(button, normalButtonColor);
        }
    }

    public void NextOption()
    {
        selectedSkin = (selectedSkin + 1) % skins.Count;
        sr.sprite = skins[selectedSkin];
    }

    public void PrevOption()
    {
        selectedSkin--;
        if (selectedSkin < 0)
        {
            selectedSkin = skins.Count - 1;
        }
        sr.sprite = skins[selectedSkin];
    }

    public void NextOptionLogo()
    {
        selectedLogo = (selectedLogo + 1) % logos.Count;
        sr2.sprite = logos[selectedLogo];
    }

    public void PrevOptionLogo()
    {
        selectedLogo--;
        if (selectedLogo < 0)
        {
            selectedLogo = logos.Count - 1;
        }
        sr2.sprite = logos[selectedLogo];
    }

    /*public void PlayGame()
    {
        PrefabUtility.SaveAsPrefabAsset(playerskins, "Prefabs/SelectedCharacter.prefab");
        SceneManager.LoadScene("SampleScene");
    }*/
}
