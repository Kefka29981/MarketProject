using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class FilterPanel : MonoBehaviour
{
    //fields
    //button
    public Button button;

    //text
    public TextMeshProUGUI text;

    //category
    public Category category;

    
    //methods
    //Activate filter
    public void ActivateFilter()
    {
        //set active category
        CategoryController.Active = category;

        //instantiate filters
        MenuHandler.filterMenu.InstantiateFilters();
    }

    //init
    public void Init(Category category)
    {
        //set category
        this.category = category;

        //set text
        text.text = category.name;
    }
}
