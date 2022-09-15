using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProductListMenu : MenuScript
{
    //fields
    //list of products
    public List<ProductData> products = new List<ProductData>();

    //product panel prefab
    public GameObject productPanelPrefab;

    //content game object
    public GameObject content;

    //start
    void Start()
    {
        //create two products
        ProductData product1 = new ProductData(100, 100, 100, 100);


        ProductData product2 = new ProductData(103, 50, 50, 100);
        //add products to list
        products.Add(product1);
        products.Add(product2);

        //instantiate product panels
        InstantiateProductPanels();
    }

    //instantiate product panels
    public void InstantiateProductPanels()
    {
        //for each product in products
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



}
