using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/// <summary>
/// Her gate 'de olan script Transactionsu ve deðeri yazdýrýyorum;
/// </summary>

public enum FourTransactions
{
    plus,
    minus,
    multi,
    compartment,
}

public class GateStats : MonoBehaviour
{
    public FourTransactions fourTransactions;
    public int gateAmount;
    public TextMeshPro textMeshProUGUI;



    private void Start()
    {
        switch (fourTransactions)
        {
            case FourTransactions.plus:
                textMeshProUGUI.text = "+ " + gateAmount;
                break;
            case FourTransactions.minus:
                textMeshProUGUI.text = "- " + gateAmount;
                break;
            case FourTransactions.multi:
                textMeshProUGUI.text = "*" + gateAmount;
                break;
            case FourTransactions.compartment:
                textMeshProUGUI.text = "/ " + gateAmount;
                break;
            default:
                break;
        }

    }
}
