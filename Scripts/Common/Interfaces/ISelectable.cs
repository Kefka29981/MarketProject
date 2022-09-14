using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ISelectable: IPointerDownHandler
{
    //properties
    MenuID menuID { get; }

    bool isSelected { get; set; }

    void Select()
    {
        //get menu controller
        MenuController menuController = MenuHandler.menuController;

        //set IsSelected to true
        isSelected = true;

        //debug type of this
        Debug.Log("Selected: " + GetType());
        //set productMono as selected productMono
        menuController.SetCurrentMenu(menuID, this);

        //call OnSelect
        OnSelect();
    }

    void Unselect()
    {
        //call default menu
        MenuController menuController = MenuHandler.menuController;
        menuController.SetCurrentMenu(MenuHandler.defaultID, null);
        //set IsSelected to false
        isSelected = false;
        
        //call OnUnselect
        OnUnselect();
    }

    //OnSelect and OnUnselect with empty default implementation
    virtual void OnSelect() { }
    virtual void OnUnselect() { }

    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        //select the object
        Select();
    }
}
