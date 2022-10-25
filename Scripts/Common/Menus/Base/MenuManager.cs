using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//refactored menu controller (singleton)
public abstract partial class MenuManager : MonoBehaviour
{
    //reference to the singleton
    public static MenuManager instance {get; protected set;}

    //reference to the menu scriptable object
    public MenuScriptableObject menuScriptableObject;

    //list of menu scripts
    public List<MenuScript> menus;

    //current menu
    private MenuID? currentMenu;

    //default menu
    public abstract MenuID defaultMenu {get;}


    public abstract void Init();

    //start
    void Awake()
    {
        Init();
    }

    //set current menu by ID
    public void SetCurrentMenu(MenuID id)
    {
        currentMenu = id;
        ShowMenu();
    }

    //

    //get current menu
    public MenuScript GetCurrentMenu()
    {
        if (currentMenu == null)
        {
            currentMenu = defaultMenu;
        }

        return menus.Find(x => x.id == currentMenu);
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

    //get menu by ID
    public MenuScript GetMenu(MenuID id)
    {
        return menus.Find(x => x.id == id);
    }


    public MenuSelectable GetMenuSelectable(MenuID id)
    {
        return GetMenu(id) as MenuSelectable;
    }


    public void CallMenu(MenuID id)
    {
        MenuScript menu = GetMenu(id);
        if(menu.IsSelectable)
        {
            //check if ISelectable isn't null
            MenuSelectable menu_s = menu as MenuSelectable;

            if(menu_s.selectedObject != null)
            {
                SetCurrentMenu(id);
            }
        }
        else
        {
            SetCurrentMenu(id);
        }
    }

    public void SetDefaultMenu()
    {
        SetCurrentMenu(defaultMenu);
    }


    /*public bool ReleaseMenu()
    {
        SetCurrentMenu(defaultMenu);
    }*/


    
}

public enum MenuID
{
    //productMono editor
    ProductEditor,

    //filters
    Filters,

    //holder editor
    RackEditor,

    //productMono list
    ProductList
}
