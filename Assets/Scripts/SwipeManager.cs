using UnityEngine;

/// <summary>
/// SwipeDirector den f�rlat�lan iste�e g�re i�lemerin yap�ld��� manager class
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
