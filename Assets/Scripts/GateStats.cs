using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GateStats : MonoBehaviour
{
    public int gateAmount;
    public TextMeshPro textMeshProUGUI;


    private void Start()
    {
        textMeshProUGUI.text = gateAmount.ToString();
    }
}
