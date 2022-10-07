using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//implement all interfaces required for the drag and drop system
public interface IDragDrop : IDragHandler, IBeginDragHandler, IEndDragHandler
{
    //property rectTransform
    RectTransform rectTransform { get; }

    //property for the drag over object
    bool RestrictionEnabled { get; }

    //property for the object not to drag off
    Collider2D Restriction { get; }


    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        //virtual method
        DragStart();

        //debug
        Debug.Log("OnBeginDrag");
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        //get mouse position
        Vector3 mousePosition = Input.mousePosition;


        //get NotDragOverObject
        if (!RestrictionEnabled)
        {
            Mouse.MoveToMouse(rectTransform);
        }
        else
        {
            //check if mouse position is inside the restriction
            if(Mouse.IsOverObject<Collider2D>(Restriction))
            {
                Mouse.MoveToMouse(rectTransform);
            }
            else
            {
                //todo: check the side of the restriction and move the object inside the restriction
                
            }
        }

        //virtual method
        OnMove();
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        //virtual method
        DragEnd();
    }

    

    //virtual methods
    public virtual void DragStart()
    {
        //do nothing
    }

    public virtual void DragEnd()
    {
        //do nothing
    }

    public virtual void OnMove()
    {
        //do nothing
    }

    public virtual void PointerDown()
    {
        //do nothing
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //virtual method
        PointerDown();
    }
}