using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public enum GateType
{
    Good,
    Bad,
}
public class GateStats : MonoBehaviour
{
    public GateType gateType;
    public int gateAmount;
    public TextMeshPro textMeshProUGUI;


    private void Start()
    {
        textMeshProUGUI.text = gateAmount.ToString();
    }
}
