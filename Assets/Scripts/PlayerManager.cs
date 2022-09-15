using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Swipe detectorden gelen inputu yakaladýðým ve gelen veriye göre elleri kontroll ettiðim yer
/// </summary>


public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private PlayerAnimationController playerAnimationController;
    [SerializeField]
    private CardsController cardsController;
    private void OnEnable()
    {
        //GateStats
        Message.AddListener<GateEventStatu>(EventName.GateAllStats, WhichHandCollisionDetect);

        //Swipe
        Message.AddListener<EnumSwipeDirection>(EventName.SwipeDirection, SwipeDetector_OnSwipe);
    }
    private void OnDisable()
    {
        //GateStats
        Message.RemoveListener<GateEventStatu>(EventName.GateAllStats, WhichHandCollisionDetect);

        //Swipe
        Message.RemoveListener<EnumSwipeDirection>(EventName.SwipeDirection, SwipeDetector_OnSwipe);
    }


    private void WhichHandCollisionDetect(GateEventStatu gateEventStatu)
    {
        switch (gateEventStatu.enumSwipeDirection)
        {
            case EnumSwipeDirection.Left:
                cardsController.CalculateTransactionValue(true, gateEventStatu.value, gateEventStatu.fourTransactions);
                break;
            case EnumSwipeDirection.Right:
                cardsController.CalculateTransactionValue(false, gateEventStatu.value, gateEventStatu.fourTransactions);
                break;
        }
    }


    //Swipe 

    private void SwipeDetector_OnSwipe(EnumSwipeDirection _swipeDirection)
    {
        switch (_swipeDirection)
        {
            case EnumSwipeDirection.Left:
                cardsController.SetSwipeDetectorMessage(_swipeDirection);
                playerAnimationController.LeftHandUp();
                break;
            case EnumSwipeDirection.Right:
                cardsController.SetSwipeDetectorMessage(_swipeDirection);
                playerAnimationController.RightHandUp();
                break;
            case EnumSwipeDirection.None:
                cardsController.SetSwipeDetectorMessage(_swipeDirection);
                playerAnimationController.SetPlayerInGameAnim();
                break;
        }
    }

}
