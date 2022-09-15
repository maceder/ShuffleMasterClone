using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using DG.Tweening;
using System;

public class CardsController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI leftHandCardAmountText, rightHandCardAmountText;
    [HideInInspector]
    public List<GameObject> leftHand, rightHand;
    [SerializeField]
    private Transform leftHandPosition, rightHandPosition;


    public float cardMoveDelay;
    public int startCardAmount;

    private float currentCardDelay;
    private EnumSwipeDirection enumSwipeDirection;


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

        leftHandCardAmountText.text = (leftHand.Count - 1).ToString();
        rightHandCardAmountText.text = (rightHand.Count - 1).ToString();

        currentCardDelay -= Time.deltaTime;

        if (currentCardDelay < 0)
        {
            currentCardDelay = cardMoveDelay;

            switch (enumSwipeDirection)
            {
                case EnumSwipeDirection.Left:
                    if (leftHand.Count > 1)
                        ShuffleCardToHands(leftHand, rightHand);
                    break;
                case EnumSwipeDirection.Right:
                    if (rightHand.Count > 1)
                        ShuffleCardToHands(rightHand, leftHand);
                    break;
                case EnumSwipeDirection.None:
                    break;

            }
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
                item.transform.DOScale(new Vector3(0.74f, 0.1f, 0.57f), 0.1f).OnComplete(() =>
                {
                    item.transform.DOScale(new Vector3(0.67140317f, 0.1f, 0.433851123f), 0.1f);
                });
                item.transform.position = positionObj;
                listObj.Add(item);
            }
        }
        handList = listObj;
    }





    #region IEnumerator

    public IEnumerator AddCardDelay(bool isleft, int _value)
    {
        for (int i = 0; i < _value; i++)
        {
            var poolCard = ObjectPool.SharedInstance.GetPooledObject();
            if (poolCard != null)
            {
                poolCard.SetActive(true);
                if (isleft)
                    AddCardsToHand(poolCard, ref leftHand);
                else
                    AddCardsToHand(poolCard, ref rightHand);
            }
            yield return new WaitForSeconds(0.07f);
        }
    }
    public IEnumerator RemoveCardFromHand(bool isleft, int removeCardAmount)
    {
        for (int i = 0; i < removeCardAmount; i++)
        {
            var _hand = new List<GameObject>();
            if (isleft)
                _hand = leftHand;
            else
                _hand = rightHand;

            if (_hand.Count > 1)
            {
                var obj = _hand.Last();
                obj.SetActive(false);
                _hand.Remove(obj);
            }
            yield return new WaitForSeconds(0.07f);
        }
    }

    #endregion


    //Bas�l� tutu�umda gelen veriyi yukar�da s�rekli bir �ekilde kontrol edicem bu sayede gelen veri h�z�na g�re haraket etmicek delay koyabilicem
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
