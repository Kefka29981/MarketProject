using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class PinsMono : AbstractProductHolderMono
{
    public Pin pinData;

    //holder type
    public override HolderType holderType => HolderType.Pin;

    public override ProductHolder productHolderData { get => pinData; set => pinData = value as Pin; }
    

    public override void RenderDefault()
    {
        //clear
        Clear();
        if(!pinData.IsEmpty()) 
        {
            if (!pinData.product.isGhost)
                {
                    //create productMono object
                    GameObject productObject = Instantiate(productPrefab);
                    productObject.transform.SetParent(transform, false);

                    //set productMono object position
                    productObject.transform.localPosition = new Vector3(-pinData.product.productData.pinpointX, -pinData.product.productData.pinpointY, 0);

                    //todo: get component without using GetComponent
                    //todo: use SetSize method
                    //set productMono object size
                    productObject.GetComponent<RectTransform>().sizeDelta = new Vector2(pinData.product.width, pinData.product.height);

                    //set productMono reference
                    ProductMono productMono = productObject.GetComponent<ProductMono>();
                    productMono.product = pinData.product;
                    productMono.holder = this;

                    //todo: refactor later
                    ProductDragDrop pdd = productObject.GetComponent<ProductDragDrop>();

                    pdd.holder = this;
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

                ghostObject.transform.localPosition = new Vector3(-pinData.product.productData.pinpointX, -pinData.product.productData.pinpointY, 0);

                ghostObject.GetComponent<RectTransform>().sizeDelta = new Vector2(pinData.product.width, pinData.product.height);

                ProductMono ghostMono = ghostObject.GetComponent<ProductMono>();
                ghostMono.product = pinData.product;
                ghostMono.holder = this;
            }

        }
    }

    public override void Clear()
    {
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

    public override void SetAsMain()
    {
        transform.SetAsLastSibling();
    }

    //image
    public Image image;

    //prefabs
    public GameObject productPrefab;

    public GameObject ghostPrefab;

    //start
    void Start()
    {
        //pin data creation
        pinData = new Pin(3000);
    }

}
