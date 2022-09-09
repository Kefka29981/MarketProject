using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RackMono : MonoBehaviour, IRender
{
    //fields
    public Rack rackData;

    //image
    public Image image;

    //prefabs
    public GameObject productPrefab;

    public GameObject ghostPrefab;

    public void Clear()
    {
        //clear all products
        foreach (Transform child in transform)
        {   DragDropProduct productMono = child.GetComponent<DragDropProduct>();
            //check if child is productMono
            if (productMono != null)
            {
                //destroy child if not dragging
                if (productMono.IsDragging == false)
                {
                    Destroy(child.gameObject);
                }
            }
            else
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void Render()
    {
        //clear all products
        Clear();

        //set image width and height
        image.rectTransform.sizeDelta = new Vector2(rackData.width, rackData.height);

        //draw all products in rack
        foreach (Product product in rackData.products)
        {
            if(!product.isGhost)
            {
            //create product object
            GameObject productObject = Instantiate(productPrefab);
            productObject.transform.SetParent(transform, false);

            //set product object position
            productObject.transform.localPosition = new Vector3(product.x, product.y, 0);

            //set product object size
            productObject.GetComponent<RectTransform>().sizeDelta = new Vector2(product.width, product.height);

            //set product reference
            ProductMono productMono = productObject.GetComponent<ProductMono>();
            productMono.product = product;
            productMono.rack = this;

            //render product
            productMono.Render();
            }
            else //same for ghost
            {
                GameObject ghostObject = Instantiate(ghostPrefab);
                ghostObject.transform.SetParent(transform, false);

                ghostObject.transform.localPosition = new Vector3(product.x, product.y, 0);

                ghostObject.GetComponent<RectTransform>().sizeDelta = new Vector2(product.width, product.height);

                ProductMono ghostMono = ghostObject.GetComponent<ProductMono>();
                ghostMono.product = product;
                ghostMono.rack = this;
            }
        }

    }

    //start
    void Start()
    {
        //create new rack
        rackData = new Rack(500, 150, 100, 100, 100);

        //create products data
        ProductData productData1 = new ProductData(1, 50, 50, 25);
        ProductData productData2 = new ProductData(2, 50, 15, 25, canBePlacedOnTop: true);
        ProductData productData3 = new ProductData(3, 90, 30, 100, canBePlacedOnTop: true);

        //create new products
        Product product1 = new Product(productData1);
        Product product2 = new Product(productData2);
        Product product3 = new Product(productData3);

        
        product2.rotation.RotateXY(product2);
        //apply rotation to product2
        product2.rotation.ApplyRotation(product2);

        //add products to rack
        rackData.AddProduct(product1);
        rackData.AddProduct(product2);
        rackData.AddProduct(product3);

        //increment product 2 width
        product2.IncrementAmount(Axis.X, 2);
        



        //render rack
        Render();
    }

    //get on top in sibling hierarchy
    public void SetAsMain()
    {
        transform.SetAsLastSibling();
    }


}
