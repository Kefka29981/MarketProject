using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RackScene
{
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
            RecreateWithAppliedFilters();
        }

        //instantiate product panels
        //TODO: move logic of spawn into prefab manager
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
            MenuManager.instance.CallMenu(MenuID.Filters);
        }

        //apply filters
        public void RecreateWithAppliedFilters()
        {
            //clear products
            products.Clear();

            //get filters menu
            FilterMenu filtersMenu = MenuManager.instance.GetMenu(MenuID.Filters) as FilterMenu;

            //get tags
            List<string> tags = Filters.GetTags().ToList();
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
}

