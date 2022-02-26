using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsNewCard : MonoBehaviour
{
    public GameObject Card;
    public GameObject CardPosition;
    private HorizontalLayoutGroup kk;

    private void Awake()
    {
        kk=GameObject.Find("CardPosition").GetComponent<HorizontalLayoutGroup>();
    }

    public void GetCard()
    {
        kk.enabled = true;
        GameObject newCard=Instantiate(Card);
        newCard.transform.SetParent(CardPosition.transform);
        kk.enabled = true;
    }
}
