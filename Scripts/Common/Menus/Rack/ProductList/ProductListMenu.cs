using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ProductListMenu : MenuScript
{
    //fields
    //list of globalProducts
    public List<ProductData> globalProducts = new List<ProductData>();

    //list of products
    public List<ProductData> products = new List<ProductData>();

    //product panel prefab
    public GameObject productPanelPrefab;

    //content game object
    public GameObject content;

    //start
    void Start()
    {
        //load all products from cvs
        globalProducts = ProductDataCVS.product_data;

        //apply filters
        ApplyFilters();
    }

    //instantiate product panels
    public void InstantiateProductPanels()
    {
        //for each product in globalProducts
        foreach (ProductData product in products)
        {
            //instantiate product panel
            GameObject productPanel = Instantiate(productPanelPrefab);

            //set parent content
            productPanel.transform.SetParent(content.transform, false);

            //get product panel script
            ProductPanel productPanelScript = productPanel.GetComponent<ProductPanel>();

            //set product data
            productPanelScript.productData = product;

            //initiate product panel
            productPanelScript.Init();
        }

    }

    //open filter button
    public void OpenFilterButton()
    {
        //set active menu
        MenuHandler.menuController.CurrentMenu = MenuID.Filters;
        //open filter menu
        MenuHandler.menuController.ShowMenu();
    }

    //apply filters
    public void ApplyFilters()
    {
        //clear products
        products.Clear();

        //get tags
        List<string> tags = MenuHandler.filterMenu.GetTags().ToList();
        //for each product in globalProducts
        foreach (ProductData product in globalProducts)
        {
            //check if tags contains product tag
            if (tags.Contains(product.tag))
            {
                //add product to products
                products.Add(product);
            }
        }

        //clear content
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }

        //instantiate product panels
        InstantiateProductPanels();
    }



}
