using BreakInfinity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoanInfo : MonoBehaviour
{
    public List<Loans> Loans;
    public Loans LoanPrefab;
    public ScrollRect LoanScroll;
    public GameObject LoanPanel; 
    public string[] loanName;
    public BigDouble[] loanAmount;
    public float[] loanInterest;
    public int[] loanDuration;
    public string[] loanStatus;
    public BigDouble[] loanTotalRepay;
    public BigDouble[] loanMonthlyRepay;
}
