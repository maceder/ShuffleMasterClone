using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �ki elde de olan collisionlar� kontrol etti�im yer gate ile �arp��t���nda gateeventte olan 2 veriyi ve hangi elin �arpt��� bilgisini f�rlatt���m yer
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



//Direk event olarak yoll�ca��m  class i�indeki farkl� verilere eri�mek i�in

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
