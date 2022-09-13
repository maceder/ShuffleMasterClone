using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private PlayerAnimationController playerAnimationController;
    EnumSwipeDirection swipeDirection;
    private void Awake()
    {
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
    }

    private void SwipeDetector_OnSwipe(SwipeData data)
    {
        switch (data.Direction)
        {
            case EnumSwipeDirection.Left:
                playerAnimationController.LeftHandUp();
                break;
            case EnumSwipeDirection.Right:
                playerAnimationController.RightHandUp();
                break;
            case EnumSwipeDirection.None:
                playerAnimationController.SetPlayerInGameAnim();
                break;
        }
    }
}
