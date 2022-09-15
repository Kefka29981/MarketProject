using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    //current menu
    private MenuID currentMenu;

    public MenuID CurrentMenu
    {
        get => currentMenu;
        set => currentMenu = value;
    }

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

    //get current menu
    public MenuScript GetCurrentMenuScript()
    {
        return menus.Find(x => x.id == currentMenu);
    }


}



public enum MenuID
{
    //productMono editor
    ProductEditor,

    //filters
    Filters,

    //rack editor
    RackEditor,

    //productMono list
    ProductList

}

public static class MenuHandler
{
    //menu controller
    public static MenuController menuController;

    //all menu classes reference
    public static FilterMenu filterMenu;
    public static ProductEditorMenu productEditorMenu;
    public static ProductListMenu productListMenu;

    //default ID
    public static MenuID defaultID = MenuID.ProductList;

    //selected productMono
    private static ISelectable selectedObj;

    //set selected productMono

    /*
    public static void SetNewSelectedObject(ISelectable obj)
    {
        //log
        Debug.Log("MenuHandler: SetNewSelectedObject: " + obj.ToString());

        //if the selected object is not null
        if (selectedObj != null)
        {
            selectedObj.Unselect();
        }
        //select new object

        selectedObj = obj;
    }*/

    //unselect selected object and activate global menu
    public static void UnselectCurrentObject()
    {
        ///<summary>
        ///
        ///
        ///NEVER USE UNSELECT HERE!!!
        ///
        ///  
        ///<summary>
        


        //if the selected object is not null
        if (selectedObj != null)
        {


            //todo: refactor this
            //get as mono and stop coroutine if possible
            try
            {
                MonoBehaviour mono = selectedObj as MonoBehaviour;
                mono.StopAllCoroutines();
                selectedObj = null;
            }
            catch (System.Exception)
            {
                
            }
        }

    }

    static MenuHandler()
    {
        //get menu controller
        menuController = GameObject.Find("MenuController").GetComponent<MenuController>();
        //get all menu classes reference
        filterMenu = GameObject.Find("FilterMenu").GetComponent<FilterMenu>();
        
        productEditorMenu = GameObject.Find("ProductEditor").GetComponent<ProductEditorMenu>();
    }
}