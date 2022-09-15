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

    public bool smol;

    public void Clear()
    {
        //clear all products
        foreach (Transform child in transform)
        {   
            ProductMono mono = child.GetComponent<ProductMono>();
            //check if child is mono
            if (mono != null)
            {
                //destroy child if not dragging
                if (mono.isDragging == false)
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
                //create productMono object
                GameObject productObject = Instantiate(productPrefab);
                productObject.transform.SetParent(transform, false);

                //set productMono object position
                productObject.transform.localPosition = new Vector3(product.x, product.y, 0);

                //todo: get component without using GetComponent
                //todo: use SetSize method
                //set productMono object size
                productObject.GetComponent<RectTransform>().sizeDelta = new Vector2(product.width, product.height);

                //set productMono reference
                ProductMono productMono = productObject.GetComponent<ProductMono>();
                productMono.product = product;
                productMono.rack = this;

                //todo: refactor later
                ProductDragDrop pdd = productObject.GetComponent<ProductDragDrop>();

                pdd.rack = this;
                pdd.productMono = productMono;

                //render productMono
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
        if(smol){
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

            //increment productMono 2 width
            product2.IncrementAmount(Axis.X, 2);


            //recreate rack
            rackData.RecreateRack();

            //render rack
            Render();
        }
        else
        {
            //create new rack
            rackData = new Rack(500, 100, 100, 100, 100);

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

            //increment productMono 2 width
            product2.IncrementAmount(Axis.X, 2);


            //recreate rack
            rackData.RecreateRack();

            //render rack
            Render();
        }
    }

    //get on top in sibling hierarchy
    public void SetAsMain()
    {
        transform.SetAsLastSibling();
    }

    //find productmono by productMono
    public ProductMono FindProductMono(Product product)
    {
        foreach (Transform child in transform)
        {
            ProductMono productMono = child.GetComponent<ProductMono>();
            if (productMono != null)
            {
                if (productMono.product == product)
                {
                    return productMono;
                }
            }
        }
        return null;
    }

}
