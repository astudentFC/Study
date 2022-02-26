using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.UIElements;


//IDragHandler,IEndDragHandler,

public class DragPointer : MonoBehaviour,IDragHandler,IEndDragHandler,IPointerExitHandler,IPointerEnterHandler
{
    private Tweener cardReviewMove;
    private Tweener cardReviewScale;
    //! rename
    private HorizontalLayoutGroup kk;
    private bool isDrag;
    private void Awake()
    {

        cardReviewMove=transform.DOLocalMoveY(30f,0.1f);
        cardReviewScale = transform.DOScale(1.2f,0.1f);
        cardReviewMove.Pause();
        cardReviewScale.Pause();
        cardReviewScale.SetAutoKill(false);
        cardReviewMove.SetAutoKill(false);
        kk=GameObject.Find("CardPosition").GetComponent<HorizontalLayoutGroup>();
    }

    
    public void OnDrag(PointerEventData eventData)
    {
        kk.enabled = false;
        transform.DOScale(0.8f, 0.1f);
        transform.position = Input.mousePosition;
        isDrag = true;
        //transform.position = Vector3.Lerp(transform.position, Input.mousePosition, Time.fixedDeltaTime * 1);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
        kk.enabled = true;
        isDrag = false;
        Debug.Log(000);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        kk.enabled = false;
            transform.DOPlayForward();
            transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOPlayBackwards();
        
        kk.enabled = true;
    }


}
