using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    //current menu
    private MenuID currentMenu;

    //list of menus
    public List<MenuScript> menus;

    //hide all menus
    public void HideAllMenus()
    {
        foreach (MenuScript menu in menus)
        {
            menu.canvasGroup.alpha = 0;
            menu.canvasGroup.blocksRaycasts = false;
            menu.canvasGroup.interactable = false;
        }
    }

    //show menu
    public void ShowMenu()
    {
        HideAllMenus();

        MenuScript menu = menus.Find(x => x.id == currentMenu);

        if (menu != null)
        {
            menu.canvasGroup.alpha = 1;
            menu.canvasGroup.blocksRaycasts = true;
            menu.canvasGroup.interactable = true;
        }
    }

    //set current menu
    public void SetCurrentMenu(MenuID id)
    {
        currentMenu = id;
        ShowMenu();
    }

    //set current menu with data
    public void SetCurrentMenu(MenuID id, ISelectable data)
    {
        currentMenu = id;
        ShowMenu();

        MenuScript menu = menus.Find(x => x.id == currentMenu);

        if (menu != null)
        {
            menu.GetData(data);
        }
    }


}



public enum MenuID
{
    //product editor
    ProductEditor,

    //filters
    Filters,

    //rack editor
    RackEditor,

    //product list
    ProductList

}

public static class MenuHandler
{
    //menu controller
    public static MenuController menuController;

    //default ID
    public static MenuID defaultID = MenuID.ProductList;

    //selected product
    private static ISelectable selectedObj;

    //set selected product

    public static void SetNewSelectedObject(ISelectable obj)
    {
        //log
        Debug.Log("MenuHandler: SetNewSelectedObject: " + obj.ToString());

        //if the selected object is not null
        if (selectedObj != null)
        {
            selectedObj.Unselect();
        }
        //set selected object
        selectedObj = obj;
    }

    //unselect selected object and activate global menu
    public static void UnselectCurrentObject()
    {
        //if the selected object is not null
        if (selectedObj != null)
        {
            selectedObj.Unselect();
        }
    }

    static MenuHandler()
    {
        //get menu controller
        menuController = GameObject.Find("MenuController").GetComponent<MenuController>();
    }
}