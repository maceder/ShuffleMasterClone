using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private PlayerAnimationController playerAnimationController;
    [SerializeField]
    private CardsController cardsController;
    private int currentAmount;

    private void OnEnable()
    {
        //CollisionTrig
        Message.AddListener<int>(EventName.WhichHand, CurrentSetCurrentAmount);
        Message.AddListener<EnumSwipeDirection>(EventName.WhichHand, WhichHandCollisionDetect);
        //Swipe
        Message.AddListener<EnumSwipeDirection>(EventName.SwipeDirection, SwipeDetector_OnSwipe);
    }
    private void OnDisable()
    {
        Message.RemoveListener<int>(EventName.WhichHand, CurrentSetCurrentAmount);
        Message.RemoveListener<EnumSwipeDirection>(EventName.WhichHand, SwipeDetector_OnSwipe);
        Message.RemoveListener<EnumSwipeDirection>(EventName.SwipeDirection, SwipeDetector_OnSwipe);
    }

    private void CurrentSetCurrentAmount(int value)
    {
        currentAmount = value;
    }
    private void WhichHandCollisionDetect(EnumSwipeDirection enumSwipeDirection)
    {
        switch (enumSwipeDirection)
        {
            case EnumSwipeDirection.Left:
                cardsController.AddCardDelay(cardsController.leftHand, currentAmount);
                break;
            case EnumSwipeDirection.Right:
                cardsController.AddCardDelay(cardsController.rightHand, currentAmount);
                break;
        }
    }

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
