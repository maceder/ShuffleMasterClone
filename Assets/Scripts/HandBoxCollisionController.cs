using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBoxCollisionController : MonoBehaviour
{
    public EnumSwipeDirection enumSwipeDirection;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gate"))
        {
            Message.Send<int>(EventName.GateAmount, other.GetComponent<GateStats>().gateAmount);
            Message.Send<EnumSwipeDirection>(EventName.WhichHand, enumSwipeDirection);
            other.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
