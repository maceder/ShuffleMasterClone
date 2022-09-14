using System;
using UnityEngine;



/// <summary>
/// Ekrandaki kaydýrma haraketinin yönünü belirliyor
/// </summary>
public class SwipeDetector : MonoBehaviour
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;


    [SerializeField]
    private float minDistanceForSwipe = 10f;



    private void LateUpdate()
    {
        foreach (Touch touch in Input.touches)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    fingerUpPosition = touch.position;
                    fingerDownPosition = touch.position;
                    break;
                case TouchPhase.Moved:
                    fingerDownPosition = touch.position;
                    DetectSwipe();
                    break;
                case TouchPhase.Ended:
                    Message.Send<EnumSwipeDirection>(EventName.SwipeDirection, EnumSwipeDirection.None);
                    break;
            }
        }
    }
    private void DetectSwipe()
    {
        if (Math.Abs(HorizontalMovementDistance()) > minDistanceForSwipe)
        {
            var direction = HorizontalMovementDistance() > 0 ? EnumSwipeDirection.Left : EnumSwipeDirection.Right;
            Message.Send<EnumSwipeDirection>(EventName.SwipeDirection, direction);
        }
        fingerUpPosition = fingerDownPosition;
    }
    private float HorizontalMovementDistance()
    {
        return fingerDownPosition.x - fingerUpPosition.x;
    }
}


