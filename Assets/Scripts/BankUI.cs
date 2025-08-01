using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class BankUI : MonoBehaviour
{
    public GameObject BalanceBG;
    public GameObject LoansBG;
    public GameObject THBG;
    public Button BalanceButton;
    public Button LoansButton;
    public Button THButton;
    public string hexColor = "#3BAFA9";

    public Transform BalanceBGT;
    public Transform LoansBGT;
    public Transform THBGT;
    public GameObject BalanceBGT2;
    public GameObject LoansBGT2;
    public GameObject THBGT2;
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 10f;
    public Transform[] Backgrounds;

    private Coroutine currentMoveCoroutine;

    public Data data;
    public TMP_Text moneyText;

    void Start()
    {
        BalanceBG.SetActive(true);
        LoansBG.SetActive(false);
        THBG.SetActive(false);
        BalanceBGT2.SetActive(true);
        LoansBGT2.SetActive(false);
        THBGT2.SetActive(false);

        Backgrounds = new Transform[] { BalanceBGT, LoansBGT, THBGT };

        // Get the Image component attached to the button
        Image buttonImage = BalanceButton.GetComponent<Image>();

        // Try to parse the hex color string into a Color
        if (buttonImage != null && UnityEngine.ColorUtility.TryParseHtmlString(hexColor, out Color color))
        {
            // Set the button's color
            buttonImage.color = color;
        }

        Backgrounds[0].position = endPoint.position;
        Backgrounds[1].position = startPoint.position;
        Backgrounds[2].position = startPoint.position;
    }

    public void OpenApp()
    {
        BalanceBG.SetActive(true);
        LoansBG.SetActive(false);
        THBG.SetActive(false);
        BalanceBGT2.SetActive(true);
        LoansBGT2.SetActive(false);
        THBGT2.SetActive(false);

        Backgrounds = new Transform[] { BalanceBGT, LoansBGT, THBGT };

        ResetButtonColors();

        // Get the Image component attached to the button
        Image buttonImage = BalanceButton.GetComponent<Image>();

        // Try to parse the hex color string into a Color
        if (buttonImage != null && UnityEngine.ColorUtility.TryParseHtmlString(hexColor, out Color color))
        {
            // Set the button's color
            buttonImage.color = color;
        }

        Backgrounds[0].position = endPoint.position;
        Backgrounds[1].position = startPoint.position;
        Backgrounds[2].position = startPoint.position;
    }

    public void SwitchTab(int tab)
    {
        ResetButtonColors();

        switch (tab)
        {
            case 0:
                SetButtonColor(BalanceButton, hexColor);
                BalanceBGT2.SetActive(true);
                BalanceBG.SetActive(true);
                if (currentMoveCoroutine != null)
                {
                    StopCoroutine(currentMoveCoroutine);
                }
                StartCoroutine(MoveBackground(0, Backgrounds, 0));
                LoansBGT2.SetActive(false);
                THBGT2.SetActive(false);
                LoansBG.SetActive(false);
                THBG.SetActive(false);
                break;
            case 1:
                SetButtonColor(LoansButton, hexColor);
                LoansBGT2.SetActive(true);
                LoansBG.SetActive(true);
                if (currentMoveCoroutine != null)
                {
                    StopCoroutine(currentMoveCoroutine);
                }
                StartCoroutine(MoveBackground(0, Backgrounds, 1));
                BalanceBGT2.SetActive(false);
                THBGT2.SetActive(false);
                BalanceBG.SetActive(false);
                THBG.SetActive(false);

                break;
            case 2:
                SetButtonColor(THButton, hexColor);
                THBGT2.SetActive(true);
                THBG.SetActive(true);
                if (currentMoveCoroutine != null)
                {
                    StopCoroutine(currentMoveCoroutine);
                }
                StartCoroutine(MoveBackground(0, Backgrounds, 2));
                BalanceBGT2.SetActive(false);
                LoansBGT2.SetActive(false);
                BalanceBG.SetActive(false);
                LoansBG.SetActive(false);
                break;
        }
    }

    private void ResetButtonColors()
    {
        SetButtonColor(BalanceButton, "#FFFFFF");
        SetButtonColor(LoansButton, "#FFFFFF");
        SetButtonColor(THButton, "#FFFFFF");
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
                Backgrounds[2].position = startPoint.position;
                break;
            case 1:
                Backgrounds[0].position = startPoint.position;
                Backgrounds[2].position = startPoint.position;
                break;
            case 2:
                Backgrounds[0].position = startPoint.position;
                Backgrounds[1].position = startPoint.position;
                break;
        }
    }
    Vector2 currentMovementTarget(int direction)
    {
        return direction == 1 ? startPoint.position : endPoint.position;
    }
    void Update()
    {
        moneyText.text = $" $ {data.money.Notate()}";
    }
}
