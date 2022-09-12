using UnityEngine;

/// <summary>
/// SwipeDirector den fýrlatýlan isteðe göre iþlemerin yapýldýðý manager class
/// </summary>
public class SwipeManager : MonoBehaviour
{
    private void Awake()
    {
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
    }

    private void SwipeDetector_OnSwipe(SwipeData data)
    {
        Debug.Log(data.Direction);
    }
}
