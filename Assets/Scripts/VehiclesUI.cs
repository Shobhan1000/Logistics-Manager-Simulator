using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class VehiclesUI : MonoBehaviour
{
    public GameObject GarageBG;
    public GameObject PurchasesBG;
    public Button GarageButton;
    public Button PurchasesButton;
    public string hexColor = "#3778C2";

    public Transform GarageBGT;
    public Transform PurchasesBGT;
    public GameObject GarageBGT2;
    public GameObject PurchasesBGT2;
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 10f;
    public Transform[] Backgrounds;

    public GameObject DealershipBG;
    public GameObject UsedBG;
    public GameObject VehicleTypeText1;
    public GameObject VehicleTypeText2;
    public GameObject CarsPanelForDealer;
    public GameObject CarsPanelForUsed;
    public Button DealershipButton;
    public Button UsedButton;

    public GameObject FleetBG;
    public GameObject MaintenanceBG;
    public GameObject VehicleTypeText3;
    public GameObject VehicleTypeText4;
    public GameObject CarsPanelForFleet;
    public GameObject CarMaintenancePanel;
    public Button FleetButton;
    public Button MaintenanceButton;

    private Coroutine currentMoveCoroutine;

    void Start()
    {
        GarageBG.SetActive(true);
        PurchasesBG.SetActive(false);
        GarageBGT2.SetActive(true);
        PurchasesBGT2.SetActive(false);
        VehicleTypeText3.SetActive(false);
        CarsPanelForFleet.SetActive(true);
        MaintenanceBG.SetActive(false);
        SetButtonColor(FleetButton, "#C8C8C8");
        SetButtonColor(MaintenanceButton, "#FFFFFF");

        Backgrounds = new Transform[] { GarageBGT, PurchasesBGT };

        // Get the Image component attached to the button
        Image buttonImage = GarageButton.GetComponent<Image>();

        // Try to parse the hex color string into a Color
        if (buttonImage != null && UnityEngine.ColorUtility.TryParseHtmlString(hexColor, out Color color))
        {
            // Set the button's color
            buttonImage.color = color;
        }

        Backgrounds[0].position = endPoint.position;
        Backgrounds[1].position = startPoint.position;
    }

    public void OpenApp()
    {
        GarageBG.SetActive(true);
        PurchasesBG.SetActive(false);
        GarageBGT2.SetActive(true);
        PurchasesBGT2.SetActive(false);
        VehicleTypeText3.SetActive(false);
        CarsPanelForFleet.SetActive(true);
        MaintenanceBG.SetActive(false);
        SetButtonColor(FleetButton, "#C8C8C8");
        SetButtonColor(MaintenanceButton, "#FFFFFF");

        Backgrounds = new Transform[] { GarageBGT, PurchasesBGT };

        ResetButtonColors();

        // Get the Image component attached to the button
        Image buttonImage = GarageButton.GetComponent<Image>();

        // Try to parse the hex color string into a Color
        if (buttonImage != null && UnityEngine.ColorUtility.TryParseHtmlString(hexColor, out Color color))
        {
            // Set the button's color
            buttonImage.color = color;
        }

        Backgrounds[0].position = endPoint.position;
        Backgrounds[1].position = startPoint.position;
    }

    public void SwitchTab(int tab)
    {
        ResetButtonColors();

        switch (tab)
        {
            case 0:
                SetButtonColor(GarageButton, hexColor);
                SetButtonColor(FleetButton, "#C8C8C8");
                SetButtonColor(MaintenanceButton, "#FFFFFF");
                GarageBGT2.SetActive(true);
                GarageBG.SetActive(true);
                VehicleTypeText3.SetActive(true);
                if (currentMoveCoroutine != null)
                {
                    StopCoroutine(currentMoveCoroutine);
                }
                StartCoroutine(MoveBackground(0, Backgrounds, 0));
                PurchasesBGT2.SetActive(false);
                PurchasesBG.SetActive(false);
                break;
            case 1:
                SetButtonColor(PurchasesButton, hexColor);
                SetButtonColor(DealershipButton, "#C8C8C8");
                SetButtonColor(UsedButton, "#FFFFFF");
                PurchasesBGT2.SetActive(true);
                PurchasesBG.SetActive(true);
                VehicleTypeText1.SetActive(true);
                if (currentMoveCoroutine != null)
                {
                    StopCoroutine(currentMoveCoroutine);
                }
                StartCoroutine(MoveBackground(0, Backgrounds, 1));
                GarageBGT2.SetActive(false);
                GarageBG.SetActive(false);
                break;
        }
    }

    private void ResetButtonColors()
    {
        SetButtonColor(GarageButton, "#FFFFFF");
        SetButtonColor(PurchasesButton, "#FFFFFF");
    }

    private void SetButtonColor(Button button, string hex)
    {
        if (button != null && ColorUtility.TryParseHtmlString(hex, out Color color))
        {
            button.GetComponent<Image>().color = color;
        }
    }

    IEnumerator MoveBackground(int direction, Transform[] backgrounds, int number)
    {
        Vector2 target = currentMovementTarget(direction);

        while (Vector2.Distance(backgrounds[number].position, target) > 3f)
        {
            backgrounds[number].position = Vector2.Lerp(backgrounds[number].position, target, speed * Time.deltaTime);
            yield return null;
        }

        backgrounds[number].position = target;

        switch (number)
        {
            case 0:
                Backgrounds[1].position = startPoint.position;
                break;
            case 1:
                Backgrounds[0].position = startPoint.position;
                break;
        }
    }
    Vector2 currentMovementTarget(int direction)
    {
        return direction == 1 ? startPoint.position : endPoint.position;
    }

    public void OpenPurchaseTabs(int tabType)
    {
        switch (tabType)
        {
            case 0:
                DealershipBG.SetActive(true);
                UsedBG.SetActive(false);
                VehicleTypeText1.SetActive(true);
                CarsPanelForDealer.SetActive(false);
                CarsPanelForUsed.SetActive(false);
                SetButtonColor(DealershipButton, "#C8C8C8");
                SetButtonColor(UsedButton, "#FFFFFF");
                break;
            case 1:
                DealershipBG.SetActive(false);
                UsedBG.SetActive(true);
                VehicleTypeText2.SetActive(true);
                CarsPanelForDealer.SetActive(false);
                CarsPanelForUsed.SetActive(false);
                SetButtonColor(DealershipButton, "#FFFFFF");
                SetButtonColor(UsedButton, "#C8C8C8");
                break;
            case 2:

                if (DealershipBG.activeSelf)
                {
                    VehicleTypeText1.SetActive(false);
                    CarsPanelForDealer.SetActive(true);
                    UsedBG.SetActive(false);
                    SetButtonColor(DealershipButton, "#C8C8C8");
                    SetButtonColor(UsedButton, "#FFFFFF");
                }
                else
                {
                    VehicleTypeText2.SetActive(false);
                    CarsPanelForUsed.SetActive(true);
                    UsedBG.SetActive(true);
                    SetButtonColor(DealershipButton, "#FFFFFF");
                    SetButtonColor(UsedButton, "#C8C8C8");
                }
                break;
        }
    }

    public void OpenGarageTabs(int tabType)
    {
        switch (tabType)
        {
            case 0:
                FleetBG.SetActive(true);
                MaintenanceBG.SetActive(false);
                VehicleTypeText3.SetActive(true);
                CarsPanelForFleet.SetActive(false);
                CarMaintenancePanel.SetActive(false);
                SetButtonColor(FleetButton, "#C8C8C8");
                SetButtonColor(MaintenanceButton, "#FFFFFF");
                break;
            case 1:
                FleetBG.SetActive(false);
                MaintenanceBG.SetActive(true);
                VehicleTypeText4.SetActive(true);
                CarsPanelForFleet.SetActive(false);
                CarMaintenancePanel.SetActive(false);
                SetButtonColor(FleetButton, "#FFFFFF");
                SetButtonColor(MaintenanceButton, "#C8C8C8");
                break;
            case 2:

                if (FleetBG.activeSelf)
                {
                    VehicleTypeText3.SetActive(false);
                    CarsPanelForFleet.SetActive(true);
                    MaintenanceBG.SetActive(false);
                    SetButtonColor(FleetButton, "#C8C8C8");
                    SetButtonColor(MaintenanceButton, "#FFFFFF");
                }
                else
                {
                    VehicleTypeText4.SetActive(false);
                    CarMaintenancePanel.SetActive(true);
                    MaintenanceBG.SetActive(true);
                    SetButtonColor(FleetButton, "#FFFFFF");
                    SetButtonColor(MaintenanceButton, "#C8C8C8");
                }
                break;
        }
    }
}
