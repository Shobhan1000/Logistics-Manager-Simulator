using TMPro;
using UnityEngine;

public class HomeUI : MonoBehaviour
{
    public Data data;
    public TMP_Text moneyText;

    void Update()
    {
        moneyText.text = $" $ {data.money.Notate()}";
    }
}
