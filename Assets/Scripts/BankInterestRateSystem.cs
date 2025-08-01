using BreakInfinity;
using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class BankInterestRateSystem : MonoBehaviour
{
    public LoanInfo[] Loans;
    public Data data;

    public void Start()
    {
        StartLoanManager();
    }

    public void StartLoanManager()
    {
        Loans[0].loanName = new[] { "Micro", "STTest2", "STTest3", "STTest4", "STTest5" };
        Loans[1].loanName = new[] { "Micro", "LTTest2", "LTTest3", "LTTest4", "LTTest5" };
        Loans[2].loanName = new[] { "Micro", "ETest2", "ETest3", "ETest4", "ETest5" };

        //Short Term
        Loans[0].loanAmount = new BigDouble[] { 2000, 1000, 50000, 3000, 25000 };
        Loans[0].loanInterest = new[] { 18.0f, 8.0f, 4.5f, 15.0f, 6.5f };
        Loans[0].loanDuration = new[] { 6, 12, 240, 6, 60 };
        Loans[0].loanStatus = new[] { "Pending", "Closed", "Active", "Paid Off", "Overdue" };
        Loans[0].loanTotalRepay = new BigDouble[] { (2000 + (2000 * 0.18 * 0.5)), (1000 + (1000*0.08*1)) , (50000 + (50000*0.045*20)) , (3000 + (3000*0.15*0.5)) , (25000 + (25000*0.065*5)) };
        Loans[0].loanMonthlyRepay = new BigDouble[] { ((2000 + (2000 * 0.18 * 0.5)) / 6), ((1000 + (1000 * 0.08 * 1)) / 12) , ((50000 + (50000 * 0.045 * 20)) / 240) , ((3000 + (3000 * 0.15 * 0.5)) / 6) , ((25000 + (25000 * 0.065 * 5)) / 60) };

        //Long Term
        Loans[1].loanAmount = new BigDouble[] { 2000, 1000, 50000, 3000, 25000 };
        Loans[1].loanInterest = new[] { 18.0f, 8.0f, 4.5f, 15.0f, 6.5f };
        Loans[1].loanDuration = new[] { 6, 12, 240, 6, 60 };
        Loans[1].loanStatus = new[] { "Pending", "Closed", "Active", "Paid Off", "Overdue" };
        Loans[1].loanTotalRepay = new BigDouble[] { (2000 + (2000 * 0.18 * 0.5)), (1000 + (1000 * 0.08 * 1)), (50000 + (50000 * 0.045 * 20)), (3000 + (3000 * 0.15 * 0.5)), (25000 + (25000 * 0.065 * 5)) };
        Loans[1].loanMonthlyRepay = new BigDouble[] { ((2000 + (2000 * 0.18 * 0.5)) / 6), ((1000 + (1000 * 0.08 * 1)) / 12), ((50000 + (50000 * 0.045 * 20)) / 240), ((3000 + (3000 * 0.15 * 0.5)) / 6), ((25000 + (25000 * 0.065 * 5)) / 60) };

        //Emergency
        Loans[2].loanAmount = new BigDouble[] { 2000, 1000, 50000, 3000, 25000 };
        Loans[2].loanInterest = new[] { 18.0f, 8.0f, 4.5f, 15.0f, 6.5f };
        Loans[2].loanDuration = new[] { 6, 12, 240, 6, 60 };
        Loans[2].loanStatus = new[] { "Pending", "Closed", "Active", "Paid Off", "Overdue" };
        Loans[2].loanTotalRepay = new BigDouble[] { (2000 + (2000 * 0.18 * 0.5)), (1000 + (1000 * 0.08 * 1)), (50000 + (50000 * 0.045 * 20)), (3000 + (3000 * 0.15 * 0.5)), (25000 + (25000 * 0.065 * 5)) };
        Loans[2].loanMonthlyRepay = new BigDouble[] { ((2000 + (2000 * 0.18 * 0.5)) / 6), ((1000 + (1000 * 0.08 * 1)) / 12), ((50000 + (50000 * 0.045 * 20)) / 240), ((3000 + (3000 * 0.15 * 0.5)) / 6), ((25000 + (25000 * 0.065 * 5)) / 60) };

        /*loanName = new List<string> { "Micro", "Short", "Long", "Emergency", "Business", "Personal", "Medical", "Education", "Equipment" };
        loanAmount = new List<BigDouble> { 2000, 1000, 50000, 3000, 25000, 15000, 2000, 30000, 50000 };
        loanInterest = new List<float> { 18.0f, 8.0f, 4.5f, 15.0f, 6.5f, 10.0f, 7.0f, 5.5f, 6.0f };
        loanDuration = new List<int> { 6, 12, 240, 6, 60, 36, 24, 96, 36 };
        loanStatus = new List<string> { "Pending", "Approved", "Active", "Paid Off", "Overdue", "Closed" };*/

        CreateLoans(0);
        CreateLoans(1);
        CreateLoans(2);

        void CreateLoans(int index)
        {
            for (int i = 0; i < Loans[index].loanName.Length; i++) // Ensure this is < (not <=)
            {
                // Instantiate the loan prefab
                Loans loan = Instantiate(Loans[index].LoanPrefab, Loans[index].LoanPanel.transform);
                loan.LoanID = i;

                // Set up the LoanButtonHandler for the button
                LoanButtonHandler handler = loan.GetComponent<LoanButtonHandler>();
                if (handler != null)
                {
                    handler.loanSystem = this;
                    handler.loanType = index == 0 ? "Short" : index == 1 ? "Long" : "Emergency";
                    handler.loanIndex = i;

                    // Set up the OnClick listener for the button
                    Button button = loan.GetComponent<Button>(); // Get the Button component
                    if (button != null)
                    {
                        button.onClick.AddListener(handler.OnClick); // Add OnClick listener
                    }
                }

                loan.gameObject.SetActive(false); // Hide the loan prefab initially
                Loans[index].Loans.Add(loan); // Add to the list of loans
            }

            Loans[index].LoanScroll.normalizedPosition = new Vector2(0, 0); // Reset scroll position
        }

        UpdateLoanUI("short");
        UpdateLoanUI("long");
        UpdateLoanUI("emergency");
    }
    public void UpdateLoanUI(string type, int UpgradeID = -1)
    {

        switch (type)
        {
            case "short":
                UpdateAllUI(Loans[0].Loans, Loans[0].loanName, Loans[0].loanDuration, Loans[0].loanAmount, Loans[0].loanInterest, Loans[0].loanStatus, Loans[0].loanTotalRepay, 0);
                break;
            case "long":
                UpdateAllUI(Loans[1].Loans, Loans[1].loanName, Loans[1].loanDuration, Loans[1].loanAmount, Loans[1].loanInterest, Loans[1].loanStatus, Loans[0].loanTotalRepay, 1);
                break;
            case "emergency":
                UpdateAllUI(Loans[2].Loans, Loans[2].loanName, Loans[2].loanDuration, Loans[2].loanAmount, Loans[2].loanInterest, Loans[2].loanStatus, Loans[0].loanTotalRepay, 2);
                break;
        }

        void UpdateAllUI(List<Loans> loans, string[] loanNames, int[] loanDurations, BigDouble[] loanAmounts, float[] loanInterests, string[] loanStatus, BigDouble[] loanTotalRepay, int index)
        {
            if (UpgradeID == -1)
            {
                for (int i = 0; i < Loans[index].loanName.Length; i++)
                {
                    UpdateUI(i);
                }
            }
            else
            {
                UpdateUI(UpgradeID);
            }

            void UpdateUI(int id)
            {
                loans[id].LoanNameText.text = loanNames[id];
                loans[id].LoanAmountText.text = "$" + loanAmounts[id];
                loans[id].LoanInterestText.text = "Interest Rate: " + loanInterests[id] + "%";
                loans[id].LoanDurationText.text = "Duration: " + loanDurations[id] + " Months";
                loans[id].LoanStatusText.text = loanStatus[id];
                loans[id].LoanTotalRepayText.text = " ";
                loans[id].gameObject.SetActive(true);
            }
        }
    }

    public void TakeOutLoan(string type, int index)
    {
        int loanTypeIndex = type == "Short" ? 0 : type == "Long" ? 1 : 2;

        StartCoroutine(HandleLoanProcess(loanTypeIndex, index));
    }

    // Coroutine to simulate loan approval delay
    private IEnumerator HandleLoanProcess(int loanTypeIndex, int index)
    {
        Loans loanUI = Loans[loanTypeIndex].Loans[index];

        // Set status to Pending...
        Loans[loanTypeIndex].loanStatus[index] = "Pending...";
        loanUI.LoanStatusText.text = "Pending...";

        // Disable button visually
        Button button = loanUI.GetComponent<Button>();
        if (button != null)
        {
            button.interactable = false;
            ColorBlock cb = button.colors;
            cb.normalColor = Color.gray;
            button.colors = cb;
        }

        yield return new WaitForSeconds(3f); // Simulate approval delay

        // Set status to Approved
        Loans[loanTypeIndex].loanStatus[index] = "Approved";
        loanUI.LoanStatusText.text = "Approved";
        loanUI.LoanTotalRepayText.text = "Amount Remaining: " + Loans[loanTypeIndex].loanTotalRepay[index].ToString("F0");

        // Give money to player
        data.money += Loans[loanTypeIndex].loanAmount[index];

        // Now call AddTH function to add transaction history
        AddTH addTHComponent = UnityEngine.Object.FindFirstObjectByType<AddTH>();
        if (addTHComponent != null)
        {
            string loanName = Loans[loanTypeIndex].loanName[index]; // Assuming LoanName is available in loanUI
            BigDouble loanAmount = Loans[loanTypeIndex].loanAmount[index];

            // Call CreateNewTransactionHistory with loan name and amount
            addTHComponent.CreateNewTransactionHistory(loanName, loanAmount, false);
        }
    }

    public void LoanRepay()
    {
        // Short-term loans
        for (int i = 0; i < Loans[0].loanName.Length; i++)
        {
            if (Loans[0].loanStatus[i] == "Approved")
            {
                Loans[0].loanTotalRepay[i] -= Loans[0].loanMonthlyRepay[i];
                data.money -= Loans[0].loanMonthlyRepay[i];
                HandleRepayment(0, i);
            }
        }

        // Long-term loans
        for (int i = 0; i < Loans[1].loanName.Length; i++)
        {
            if (Loans[1].loanStatus[i] == "Approved")
            {
                Loans[1].loanTotalRepay[i] -= Loans[1].loanMonthlyRepay[i];
                data.money -= Loans[1].loanMonthlyRepay[i];
                HandleRepayment(1, i);
            }
        }

        // Emergency loans
        for (int i = 0; i < Loans[2].loanName.Length; i++)
        {
            if (Loans[2].loanStatus[i] == "Approved")
            {
                Loans[2].loanTotalRepay[i] -= Loans[2].loanMonthlyRepay[i];
                data.money -= Loans[2].loanMonthlyRepay[i];
                HandleRepayment(2, i);
            }
        }
    }

    private void HandleRepayment(int loanTypeIndex, int index)
    {
        Loans loanUI = Loans[loanTypeIndex].Loans[index];

        // Now call AddTH function to add transaction history
        AddTH addTHComponent = UnityEngine.Object.FindFirstObjectByType<AddTH>();
        if (addTHComponent != null)
        {
            string loanName = Loans[loanTypeIndex].loanName[index] + " Repayment"; // Assuming LoanName is available in loanUI
            BigDouble loanAmount = Loans[loanTypeIndex].loanMonthlyRepay[index];

            // Call CreateNewTransactionHistory with loan name and amount
            addTHComponent.CreateNewTransactionHistory(loanName, loanAmount, true);
        }

        if (Loans[loanTypeIndex].loanTotalRepay[index] == 0)
        {
            Button button = loanUI.GetComponent<Button>();
            if (button != null)
            {
                button.interactable = true;
                ColorBlock cb = button.colors;
                cb.normalColor = Color.white;
                button.colors = cb;
            }
            loanUI.LoanStatusText.text = "Available";
            loanUI.LoanTotalRepayText.text = " ";
            Loans[loanTypeIndex].loanTotalRepay[index] = Loans[loanTypeIndex].loanAmount[index] + (Loans[loanTypeIndex].loanAmount[index] * (Loans[loanTypeIndex].loanInterest[index]/100) * (Loans[loanTypeIndex].loanDuration[index]/12));
        }
        else
        {
            loanUI.LoanTotalRepayText.text = "Amount Remaining: " + Loans[loanTypeIndex].loanTotalRepay[index].ToString("F0");
        }  
    }
}