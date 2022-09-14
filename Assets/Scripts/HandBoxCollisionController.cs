using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBoxCollisionController : MonoBehaviour
{
    [SerializeField]
    private EnumSwipeDirection enumSwipeDirection;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gate"))
        {
            Message.Send<EnumSwipeDirection>(EventName.WhichHand, enumSwipeDirection);
            Message.Send<int>(EventName.GateCollision, other.GetComponent<GateStats>().gateAmount);
            other.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
