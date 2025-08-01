using UnityEngine;

public class LoanButtonHandler : MonoBehaviour
{
    public BankInterestRateSystem loanSystem;
    public string loanType;
    public int loanIndex;

    public void OnClick()
    {
        loanSystem.TakeOutLoan(loanType, loanIndex);
    }
}