using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;


public class CardsController : MonoBehaviour
{
    public List<GameObject> leftHand, rightHand;
    public Transform leftHandPosition, rightHandPosition;
    public EnumSwipeDirection enumSwipeDirection;

    public float cardMoveDelay;
    private float currentCardDelay;

    private void Start()
    {
        currentCardDelay = cardMoveDelay;
        for (int i = 0; i < 25; i++)
        {
            var poolCard = ObjectPool.SharedInstance.GetPooledObject();
            if (poolCard != null)
            {
                poolCard.SetActive(true);
                poolCard.transform.position = leftHandPosition.position + new Vector3(0, 0.15f * i, 0);
                leftHand.Add(poolCard);
            }
        }
    }
    private void Update()
    {
        currentCardDelay -= Time.deltaTime;

        if (currentCardDelay < 0)
        {
            currentCardDelay = cardMoveDelay;
            switch (enumSwipeDirection)
            {
                case EnumSwipeDirection.Left:
                    if (leftHand.Count > 1)
                    {
                        leftHand.Last().transform.DOJump(rightHand[0].transform.position + new Vector3(0, 0.15f * rightHand.Count), 3, 1, .3f);
                        rightHand.Add(leftHand.Last());
                        leftHand.Remove(leftHand.Last());
                    }

                    break;
                case EnumSwipeDirection.Right:
                    if (rightHand.Count > 1)
                    {
                        rightHand.Last().transform.DOJump(leftHand[0].transform.position + new Vector3(0, 0.15f * leftHand.Count), 3, 1, .3f);
                        leftHand.Add(rightHand.Last());
                        rightHand.Remove(rightHand.Last());
                    }
                    break;
                case EnumSwipeDirection.None:
                    break;

            }

        }
    }


    //Basýlý tutuðumda gelen veriyi yukarýda sürekli bir þekilde kontrol edicem bu sayede gelen veri hýzýna göre haraket etmicek delay koyabilicem
    public void MoveCardToOtherHand(EnumSwipeDirection _swipeDirection)
    {
        switch (_swipeDirection)
        {
            case EnumSwipeDirection.Left:
                enumSwipeDirection = EnumSwipeDirection.Left;
                break;
            case EnumSwipeDirection.Right:
                enumSwipeDirection = EnumSwipeDirection.Right;
                break;
            case EnumSwipeDirection.None:
                enumSwipeDirection = EnumSwipeDirection.None;
                break;

        }
    }
}
