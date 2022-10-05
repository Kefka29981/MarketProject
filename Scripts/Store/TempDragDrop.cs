using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TempDragDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    //rect transform
    public RectTransform rectTransform;
    
    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
}
