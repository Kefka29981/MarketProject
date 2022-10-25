using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using TMPro;

namespace RackScene
{
    public class FilterMenu : MenuScript
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
            //TODO: move logic of spawn into prefab manager
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
                ApplyFilters();
            }
        }

        //return all tags of active category and all subcategories


        //call apply filters from product list
        public void ApplyFilters()
        {
            ProductListMenu menu = MenuManager.instance.GetMenu(MenuID.ProductList) as ProductListMenu;
            menu.RecreateWithAppliedFilters();
            //return to product list menu
            MenuManager.instance.SetCurrentMenu(MenuID.ProductList);
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
}

