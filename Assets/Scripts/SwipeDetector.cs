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
    private bool detectSwipeOnlyAfterRelease = false;

    [SerializeField]
    private float minDistanceForSwipe = 10f;

    public static event Action<SwipeData> OnSwipe = delegate { };




    private void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUpPosition = touch.position;
                fingerDownPosition = touch.position;
            }

            if (!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
            {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerDownPosition = touch.position;
                DetectSwipe();
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


