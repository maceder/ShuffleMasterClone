using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Ellerin Animasyonlarý
/// </summary>

public class PlayerAnimationController : MonoBehaviour
{
    private Animator playerAnimator;
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    public void SetPlayerInGameAnim()
    {
        playerAnimator.SetInteger("Hand", 0);
    }
    public void LeftHandUp()
    {
        playerAnimator.SetInteger("Hand", 1);
    }
    public void RightHandUp()
    {
        playerAnimator.SetInteger("Hand", 2);
    }
}
