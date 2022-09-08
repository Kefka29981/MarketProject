using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ISelectable: IPointerDownHandler
{
    //methods
    void Select();
    void Unselect();

    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        //select the object
        Select();
    }
}
