using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoanUI : MonoBehaviour
{
    public string hexColor = "#6BAEFC";
    public GameObject STLoans;
    public GameObject LTLoans;
    public GameObject ELoans;
    public Button STLButton;
    public Button LTLButton;
    public Button ELButton;

    void Start()
    {
        STLoans.SetActive(true);
        LTLoans.SetActive(false);
        ELoans.SetActive(false);

        // Get the Image component attached to the button
        Image buttonImage = STLButton.GetComponent<Image>();

        // Try to parse the hex color string into a Color
        if (buttonImage != null && UnityEngine.ColorUtility.TryParseHtmlString(hexColor, out Color color))
        {
            // Set the button's color
            buttonImage.color = color;
        }
    }

    public void SwitchTab(int tab)
    {
        ResetButtonColors();

        switch (tab)
        {
            case 0:
                SetButtonColor(STLButton, hexColor);
                STLoans.SetActive(true);
                LTLoans.SetActive(false);
                ELoans.SetActive(false);
                break;
            case 1:
                SetButtonColor(LTLButton, hexColor);
                LTLoans.SetActive(true);
                STLoans.SetActive(false);
                ELoans.SetActive(false);
                break;
            case 2:
                SetButtonColor(ELButton, hexColor);
                ELoans.SetActive(true);
                LTLoans.SetActive(false);
                STLoans.SetActive(false);
                break;
        }
    }
    private void ResetButtonColors()
    {
        SetButtonColor(STLButton, "#ADB4DD");
        SetButtonColor(LTLButton, "#ADB4DD");
        SetButtonColor(ELButton, "#ADB4DD");
    }
    private void SetButtonColor(Button button, string hex)
    {
        if (button != null && ColorUtility.TryParseHtmlString(hex, out Color color))
        {
            button.GetComponent<Image>().color = color;
        }
    }
}
