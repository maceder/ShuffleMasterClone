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
        ////CollisionTrig
        Message.AddListener<int>(EventName.GateAmount, CurrentSetCurrentAmount);
        Message.AddListener<EnumSwipeDirection>(EventName.WhichHand, WhichHandCollisionDetect);


        //Swipe
        Message.AddListener<EnumSwipeDirection>(EventName.SwipeDirection, SwipeDetector_OnSwipe);
    }
    private void OnDisable()
    {
        Message.RemoveListener<int>(EventName.GateAmount, CurrentSetCurrentAmount);
        Message.RemoveListener<EnumSwipeDirection>(EventName.WhichHand, WhichHandCollisionDetect);

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
                if (currentAmount > 0)
                    cardsController.StartCoroutine(cardsController.AddCardDelay(true, currentAmount));
                else
                    cardsController.StartCoroutine(cardsController.RemoveCardFromHand(true, currentAmount *= -1));
                break;
            case EnumSwipeDirection.Right:
                if (currentAmount > 0)
                    cardsController.StartCoroutine(cardsController.AddCardDelay(false, currentAmount));
                else
                    cardsController.StartCoroutine(cardsController.RemoveCardFromHand(false, currentAmount *= -1));
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
