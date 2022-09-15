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
        //set selected object in MenuSelectable
        MenuSelectable menuSelectable = menuController.GetCurrentMenuScript() as MenuSelectable;
        
        //unselect old if possible
        try
        {
            menuSelectable.selected.Unselect();
        }
        catch (System.Exception)
        {
            Debug.Log("nothing was selected");
        }
        //MenuHandler.UnselectCurrentObject();


        //set IsSelected to true
        isSelected = true;

        //debug type of this
        Debug.Log("Selected: " + GetType());
        
        menuController.SetCurrentMenu(menuID, this);


        try
        {
            //set selected object
            menuSelectable.selected = this;
        }
        catch (System.Exception)
        {
            Debug.Log("MenuSelectable not found");
        }

        //call OnSelect
        OnSelect();

        //todo: check if monobehaviour
        //try to get as monobehaviour
        try
        {
            MonoBehaviour mono = this as MonoBehaviour;
            mono.StartCoroutine(WaitForUnselect());
        }
        catch
        {
            Debug.LogError("Could not cast to monobehaviour");
        }
    }

    void Unselect()
    {
        //call default menu
        MenuController menuController = MenuHandler.menuController;
        menuController.SetCurrentMenu(MenuHandler.defaultID, null);
        //set IsSelected to false
        isSelected = false;
        //stop coroutine
        try
        {
            MonoBehaviour mono = this as MonoBehaviour;
            mono.StopAllCoroutines();
        }
        catch (System.Exception)
        {
            Debug.Log("Could not stop coroutine");
        }

        //call OnUnselect
        OnUnselect();
    }

    //todo: if another ISelectable object is needed, use this method for basic implementation of changing selected object
    //void BasicUnselectTrigger()

    bool UnselectTrigger();


    //OnSelect and OnUnselect with empty default implementation
    virtual void OnSelect() { }
    virtual void OnUnselect() { }

    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        //select the object
        Select();
    }

    IEnumerator WaitForUnselect()
    {
        //until trigger not called, wait
        while (true)
        {
            if (UnselectTrigger())
            {
                Debug.Log("Unselect");
                //unselect
                Unselect();

                //stop coroutine
                yield break;
            }
            yield return null;
        }
    }
}
