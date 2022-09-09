using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ISelectable: IPointerDownHandler
{
    //properties
    MenuID menuID { get; }

    void Select()
    {
        //get menu controller
        MenuController menuController = MenuHandler.menuController;
        //set product as selected product
        menuController.SetCurrentMenu(menuID, this);

        //call OnSelect
        OnSelect();
    }

    void Unselect()
    {
        //call default menu
        MenuController menuController = MenuHandler.menuController;
        menuController.SetCurrentMenu(MenuHandler.defaultID, null);

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
