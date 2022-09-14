using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;


public class CardsController : MonoBehaviour
{

    private EnumSwipeDirection enumSwipeDirection;
    [SerializeField]
    private List<GameObject> leftHand, rightHand;
    [SerializeField]
    private Transform leftHandPosition, rightHandPosition;

    public float cardMoveDelay;
    public int startCardAmount;
    private float currentCardDelay;

    private void Start()
    {
        enumSwipeDirection = EnumSwipeDirection.None;

        leftHand = new List<GameObject>();
        rightHand = new List<GameObject>();

        leftHand.Add(leftHandPosition.gameObject);
        rightHand.Add(rightHandPosition.gameObject);

        currentCardDelay = cardMoveDelay;


        for (int i = 0; i < startCardAmount; i++)
        {
            var poolCard = ObjectPool.SharedInstance.GetPooledObject();
            if (poolCard != null)
            {
                poolCard.SetActive(true);
                poolCard.transform.position = leftHandPosition.position + new Vector3(0, 0.15f * i, 0);
                poolCard.transform.parent = leftHandPosition.parent;
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
                        ShuffleCardToHands(leftHand, rightHand);
                    }
                    break;
                case EnumSwipeDirection.Right:
                    if (rightHand.Count > 1)
                    {
                        ShuffleCardToHands(rightHand, leftHand);
                    }

                    break;
                case EnumSwipeDirection.None:
                    break;

            }
        }

        if (Input.GetKeyDown("v"))
        {
            Debug.Log("12");
            SetAddedCardAmount(8);
        }

        if (Input.GetKeyDown("h"))
        {
            RemoveCardFromHand(leftHand, 5);
        }
    }

    private void ShuffleCardToHands(List<GameObject> _addListHand, List<GameObject> _removeListHand)
    {
        var rightCardObject = _addListHand.Last();
        rightCardObject.transform.DOJump(_removeListHand[0].transform.position + new Vector3(0, 0.15f * (_removeListHand.Count - 1)), 3, 1, .3f);
        _removeListHand.Add(rightCardObject);
        rightCardObject.transform.parent = _removeListHand[0].transform.parent;
        _addListHand.Remove(rightCardObject);
    }



    private void AddCardsToHand(GameObject obj, ref List<GameObject> handList)
    {
        List<GameObject> listObj = new List<GameObject>();
        int value = 1;
        var positionObj = Vector3.zero;
        foreach (var item in handList)
        {
            if (value == 1)
            {
                positionObj = item.transform.position;
                listObj.Add(item);
                obj.transform.position = positionObj;
                listObj.Add(obj);
                obj.transform.parent = item.transform.parent;
                value++;
            }
            else
            {
                positionObj += new Vector3(0, 0.15f, 0);
                item.transform.position = positionObj;
                listObj.Add(item);
            }
        }
        handList = listObj;
    }

    private void RemoveCardFromHand(List<GameObject> _handList, int removeCardAmount)
    {
        for (int i = 0; i < removeCardAmount; i++)
        {
            if (_handList.Count > 1)
            {
                var obj = _handList.Last();
                obj.SetActive(false);
                _handList.Remove(obj);
            }
        }
    }

    private void SetAddedCardAmount(int CardAmount)
    {
        for (int i = 1; i < CardAmount; i++)
        {
            var poolCard = ObjectPool.SharedInstance.GetPooledObject();
            if (poolCard != null)
            {
                poolCard.SetActive(true);
                AddCardsToHand(poolCard, ref rightHand);
            }
        }
    }

    //Basýlý tutuðumda gelen veriyi yukarýda sürekli bir þekilde kontrol edicem bu sayede gelen veri hýzýna göre haraket etmicek delay koyabilicem
    public void SetSwipeDetectorMessage(EnumSwipeDirection _swipeDirection)
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
