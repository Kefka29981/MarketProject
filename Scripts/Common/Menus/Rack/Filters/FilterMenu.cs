using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using TMPro;

public class FilterMenu: MenuScript
{

    public GameObject filterPrefab;

    public GameObject filtersListObject;

    public FilterHierarchy filterHierarchy;

    //text to show when no subcategories are available
    public TextMeshProUGUI noSubcategoriesText;

    //public TextMeshProUGUI Text;

    public string[] appliedFilters;

    //start
    void Start()
    {
        //start category controller
        //CategoryController.Start();
        ToRoot();

        //disable no subcategories text
        noSubcategoriesText.enabled = false;
    }

    //to root category
    public void ToRoot()
    {
        CategoryController.Active = CategoryController.Root;
        InstantiateFilters();

        //clear text
        //ClearText();
    }

    //Instantiate a filter buttons for each subcategory of active category
    public void InstantiateFilters()
    {
        //disable no subcategories text by default
        noSubcategoriesText.enabled = false;

        //destroy all content of filters list object
        foreach (Transform child in filtersListObject.transform)
        {
            Destroy(child.gameObject);
        }

        //get active category
        Category active = CategoryController.Active;

        //get subcategories
        List<Category> subcategories = active.subcategories;

        //instantiate filter buttons for each subcategory
        foreach (Category subcategory in subcategories)
        {
            //instantiate filter button
            GameObject filter = Instantiate(filterPrefab);

            filter.transform.SetParent(filtersListObject.transform, false);

            filter.GetComponent<FilterPanel>().Init(subcategory);

        }
        
        //instantiate all buttons in filter hierarchy
        filterHierarchy.InstantiateAllButtons();

        //if no subcategories, show product list
        if (subcategories.Count == 0)
        {
            //set menu as active
            MenuHandler.menuController.CurrentMenu = MenuID.ProductList;
            
            //get product list and apply filters
            MenuHandler.productListMenu.ApplyFilters();

            //show no subcategories text
            noSubcategoriesText.enabled = true;

            //show product list
            MenuHandler.menuController.ShowMenu();
        }
    }

    //return all tags of active category and all subcategories (for example, root returns "root, alcohol, wine, beer, liquor, food, drinks, snacks, desserts")
    public string[] GetTags()
    {
        Category category = CategoryController.Active;

        //category to json
        string json = Newtonsoft.Json.JsonConvert.SerializeObject(category);

        //using regex to get all tags from json
        //create regex
        Regex regex = new Regex(@"\""tag\"":\s*\""([^\""]*)\""");
        //get all tags
        MatchCollection matches = regex.Matches(json);

        Debug.Log(matches.Count);

        //create array of tags
        string[] tags = new string[matches.Count];
        //add tags to array
        for (int i = 0; i < matches.Count; i++)
        {
            tags[i] = matches[i].Groups[1].Value;
            Debug.Log(tags[i]);
        }

        //load in applied filters
        appliedFilters = tags;

        return appliedFilters;
    }

    //call apply filters from product list
    public void ApplyFilters()
    {
        //get product list
        ProductListMenu productList = MenuHandler.productListMenu;

        //apply filters
        productList.ApplyFilters();

        //return to product list menu
        MenuHandler.menuController.CurrentMenu = MenuID.ProductList;
        MenuHandler.menuController.ShowMenu();
    }

    /*/update text field every time user changes active category
    public void UpdateText()
    {
        //if active category is not root
        if (categoryController.Active != categoryController.Root)
        {
        Text.text = (Text.text + " --> " + categoryController.Active.name);
        }
    }

    //clear text field
    public void ClearText()
    {
        Text.text = "";
    }*/
}
