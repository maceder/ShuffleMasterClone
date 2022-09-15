using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Ýki elde de olan collisionlarý kontrol ettiðim yer gate ile çarpýþtýðýnda gateeventte olan 2 veriyi ve hangi elin çarptýðý bilgisini fýrlattýðým yer
/// </summary>
public class HandBoxCollisionController : MonoBehaviour
{
    public EnumSwipeDirection enumSwipeDirection;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gate"))
        {
            Message.Send<GateEventStatu>(EventName.GateAllStats, new GateEventStatu(other.GetComponent<GateStats>().gateAmount, enumSwipeDirection
                , other.GetComponent<GateStats>().fourTransactions));

            other.GetComponent<BoxCollider>().enabled = false;
        }
    }
}



//Direk event olarak yollýcaðým  class içindeki farklý verilere eriþmek için

public class GateEventStatu
{
    public int value;
    public EnumSwipeDirection enumSwipeDirection;
    public FourTransactions fourTransactions;


    public GateEventStatu(int value, EnumSwipeDirection enumSwipeDirection, FourTransactions fourTransactions)
    {
        this.value = value;
        this.enumSwipeDirection = enumSwipeDirection;
        this.fourTransactions = fourTransactions;
    }
}
