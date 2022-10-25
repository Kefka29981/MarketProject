using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//static class that containse references to all prefabs

namespace RackScene
{
    public static class PrefabManager
    {

        public static PrefabStorageMono PrefabStorageMono;

        //todo: change all fields references
        //prefabs
        public static GameObject productPrefab { get; private set; }
        public static GameObject rackPrefab { get; private set; }
        public static GameObject ghostPrefab { get; private set; }
        public static GameObject spawnedProductPrefab { get; private set; }
        public static GameObject filterPrefab { get; private set; }
        public static GameObject pinPoint { get; private set; }
        public static GameObject toCategoryButtonPrefab { get; private set; }


        //load prefabs
        public static void LoadPrefabs()
        {
            //load prefabs
            productPrefab = PrefabStorageMono.productPrefab;
            rackPrefab = PrefabStorageMono.rackPrefab;
            ghostPrefab = PrefabStorageMono.ghostPrefab;
            spawnedProductPrefab = PrefabStorageMono.spawnedProductPrefab;
            filterPrefab = PrefabStorageMono.filterPrefab;
            pinPoint = PrefabStorageMono.pinPoint;
            toCategoryButtonPrefab = PrefabStorageMono.toCategoryButtonPrefab;


        }

        static PrefabManager()
        {
            //find prefab storage
            PrefabStorageMono = GameObject.Find("SceneLoader").GetComponent<PrefabStorageMono>();
            LoadPrefabs();
        }

        //next methods are used for instantiating and setting prefabs
        public static GameObject InstantiateProductPrefab()
        {
            GameObject product = GameObject.Instantiate(productPrefab);

            //return product
            return product;
        }

        public static GameObject InstantiateProductPositioner(ProductPanel productPanel)
        {
            //find all objects with component ProductPositioner and destroy them
            ProductPositioner[] productPositioners = GameObject.FindObjectsOfType<ProductPositioner>();
            foreach (ProductPositioner productPositioner in productPositioners)
            {
                GameObject.Destroy(productPositioner.gameObject);
            }


            //instantiate product
            GameObject spawnedProduct = GameObject.Instantiate(productPanel.spawnedProductPrefab);

            //todo: move reference to static class
            //find shelving object
            GameObject shelving = GameObject.Find("Shelving");

            //set parent to this
            spawnedProduct.transform.SetParent(shelving.transform, false);

            //get product mono
            ProductMono productMono = spawnedProduct.GetComponent<ProductMono>();

            //create new product
            Product product = new Product(productPanel.productData.id);
            //set product data
            productMono.product = product;

            //todo: use SetSize method instead
            //set size
            productMono.GetComponent<RectTransform>().sizeDelta = new Vector2(product.width, product.height);

            //get menu controller and set active product to this in product editor menu
            ProductEditorMenu productEditorMenu =
                MenuManager.instance.GetMenu(MenuID.ProductEditor) as ProductEditorMenu;
            productEditorMenu.SetCurrentObject(productMono);

            return spawnedProduct;

        }



    }
}

//enums for state machines are stored here