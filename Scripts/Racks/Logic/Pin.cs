using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : ProductHolder
{
    //products could be pinned here
    //for now, contains only one product
    public Product product;

    //depth
    public int depth;

    //position
    public Vector2 position;

    //constructor
    public Pin(int depth)
    {
        this.depth = depth;
    }


    public override void AddProduct(Product product)
    {
        //add product to the pin if can add product
        if (CanAddProduct(product))
        {
            this.product = product;
        }
    }

    public override void RemoveProduct(Product product)
    {
        //remove product from the pin
        this.product = null;
    }

    public void RemoveProduct()
    {
        //if not null
        if (product != null)
        {
            RemoveProduct(product);
        }
        else
        {
            Debug.Log("Can't remove from empty pin");
        }
    }

    public override bool CanAddProduct(Product product, bool containmentCheck = false)
    {
        //check if product can be pinned
        if (product.productData.canBePinned)
        {
            //check if pin is empty
            if (this.product == null)
            {
                //check if product rotation and depth is correct
                if (product.rotation.IsDefault() && product.depth <= this.depth && product.amount[Axis.X] == 1 && product.amount[Axis.Y] == 1)
                {
                    //log
                    Debug.Log("Product can be pinned");
                    return true;
                }
            }
        }
        //log
        Debug.Log("Can't pin");
        return false;
    }

    public override void RecreateHolder()
    {
        
    }

    public override void RecreateWithoutGhosts()
    {
        //remove product if product is ghost
        if (product != null)
        {
            if (product.isGhost)
            {
                RemoveProduct();
            }
        }
    }

    public override void UpdateReservedProducts()
    {
        
    }

    //is empty
    public bool IsEmpty()
    {
        return product == null;
    }
}
