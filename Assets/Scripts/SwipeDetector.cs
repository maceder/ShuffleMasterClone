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

    public static event Action<SwipeData> OnSwipe = delegate { };


    private void Update()
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
                    SendSwipe(EnumSwipeDirection.None);
                    break;
            }
        }
    }
    private void DetectSwipe()
    {
        if (Math.Abs(HorizontalMovementDistance()) > minDistanceForSwipe)
        {
            var direction = HorizontalMovementDistance() > 0 ? EnumSwipeDirection.Right : EnumSwipeDirection.Left;
            SendSwipe(direction);
        }
        fingerUpPosition = fingerDownPosition;
    }
    private float HorizontalMovementDistance()
    {
        return fingerDownPosition.x - fingerUpPosition.x;
    }

    private void SendSwipe(EnumSwipeDirection direction)
    {
        SwipeData swipeData = new SwipeData()
        {
            Direction = direction,
        };
        OnSwipe(swipeData);
    }
}

public struct SwipeData
{
    public EnumSwipeDirection Direction;
}


