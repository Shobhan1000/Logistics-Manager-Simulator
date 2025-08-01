using System;
using BreakInfinity;
using TMPro;
using UnityEngine;

public class AddTH : MonoBehaviour
{
    public THInfo THInfo; // Assign this in the Inspector

    public void CreateNewTransactionHistory(string name, BigDouble amount, Boolean negative)
    {
        // Instantiate the prefab
        TransactionHistories newTH = Instantiate(THInfo.THPrefab, THInfo.THPanel.transform);

        // Assign TMP_Text fields (make sure they are tagged/named appropriately in the prefab)
        TMP_Text[] texts = newTH.GetComponentsInChildren<TMP_Text>();
        foreach (var text in texts)
        {
            if (text.name.Contains("Name")) newTH.THNameText = text;
            else if (text.name.Contains("Amount")) newTH.THAmountText = text;
            else if (text.name.Contains("Date")) newTH.THDateText = text;
        }

        // Assign the name text
        newTH.THNameText.text = name;

        // Assign the amount text and change color based on amount
        newTH.THAmountText.text = amount.ToString("F0");

        // Change the color to green if it's a positive amount
        if (negative == false)
        {
            newTH.THAmountText.color = Color.green;
            newTH.THAmountText.text = "+ $" + newTH.THAmountText.text;
        }
        else
        {
            newTH.THAmountText.color = Color.red;
            newTH.THAmountText.text = "- $" + newTH.THAmountText.text;
        }

        // Optionally, assign the date text if needed
        newTH.THDateText.text = $"{TimeManager.Day:00}/{TimeManager.Month:00}/{TimeManager.Year:0000}";
    }
}