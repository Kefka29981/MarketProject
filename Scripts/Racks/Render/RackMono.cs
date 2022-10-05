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
        //clear all globalProducts
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

    public void RenderDefault()
    {
        //clear all globalProducts
        Clear();

        //set image width and height
        image.rectTransform.sizeDelta = new Vector2(rackData.width, rackData.height);

        //draw all globalProducts in rack
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
                //get as IRender
                IRender render = productMono as IRender;
                render.RenderDefault();
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

            //create globalProducts data
            

            //create new globalProducts
            Product product1 = new Product(1);
            Product product2 = new Product(1);
            Product product3 = new Product(1);


            product2.rotation.RotateXY(product2);
            //apply rotation to product2
            product2.rotation.ApplyRotation(product2);

            //add globalProducts to rack
            rackData.AddProduct(product1);
            rackData.AddProduct(product2);
            rackData.AddProduct(product3);

            //increment productMono 2 width
            product2.IncrementAmount(Axis.X, 2);


            //recreate rack
            rackData.RecreateRack();

            //render rack//render old rack//get rackmono as IRender
            IRender rackRender = this as IRender;
            rackRender.Render();
        }
        else
        {
            //create new rack
            rackData = new Rack(500, 100, 100, 100, 100);

            
            //create new globalProducts
            Product product1 = new Product(1);
            Product product2 = new Product(1);
            Product product3 = new Product(1);


            product2.rotation.RotateXY(product2);
            //apply rotation to product2
            product2.rotation.ApplyRotation(product2);

            //add globalProducts to rack
            rackData.AddProduct(product1);
            rackData.AddProduct(product2);
            rackData.AddProduct(product3);

            //increment productMono 2 width
            product2.IncrementAmount(Axis.X, 2);


            //recreate rack
            rackData.RecreateRack();

            //render rack//render old rack//get rackmono as IRender
            IRender rackRender = this as IRender;
            rackRender.Render();
        }
    }

    //get on top in sibling hierarchy
    public void SetAsMain()
    {
        transform.SetAsLastSibling();
    }

    //find productmono by product
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
