using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private PlayerAnimationController playerAnimationController;
    [SerializeField]
    private CardsController cardsController;
    private void Awake()
    {
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
    }

    private void SwipeDetector_OnSwipe(SwipeData data)
    {
        switch (data.Direction)
        {
            case EnumSwipeDirection.Left:
                cardsController.SetSwipeDetectorMessage(data.Direction);
                playerAnimationController.LeftHandUp();
                break;
            case EnumSwipeDirection.Right:
                cardsController.SetSwipeDetectorMessage(data.Direction);
                playerAnimationController.RightHandUp();
                break;
            case EnumSwipeDirection.None:
                cardsController.SetSwipeDetectorMessage(data.Direction);
                playerAnimationController.SetPlayerInGameAnim();
                break;
        }
    }

}
