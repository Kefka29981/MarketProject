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

        product.holder = this;
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


    //recreate holder with new list
    public void RecreateHolder(List<Product> new_products)
    {
        //copy new_products to new list (to avoid clearing original list)
        new_products = new List<Product>(new_products);

        //TODO: refactor this
        ProductEditorMenu menu = MenuHandler.menuController.GetCurrentMenuScript() as ProductEditorMenu;

        //clear holder
        RemoveProduct();

        foreach (Product product in new_products)
        {
            //check if productMono can be added to holder
            bool result = CanAddProduct(product);

            if (result)
            {
                //add productMono to holder
                AddProduct(product);
            }
            else
            {
                //print error message
                //Debug.Log("Product " + product.productData.id + " can't be added to holder");
                //recreate with reserved globalProducts
                RecreateHolder(reservedProducts);
                //set reserve as active
                menu.SetReserveProductAsActive();
                break;
            }
        }


    }

    //recreate holder with same globalProducts
    public override void RecreateHolder()
    {
        //if not empty
        if(!IsEmpty())
        {
            List<Product> products = new List<Product>();
            products.Add(product);
            RecreateHolder(products);
        }
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
        //create new list
        reservedProducts = new List<Product>();

        List<Product> products = new List<Product>();
        products.Add(product);

        //TODO: refactor this
        //get according menu from menu handler
        ProductEditorMenu menu = MenuHandler.menuController.GetCurrentMenuScript() as ProductEditorMenu;

        //clear reserved globalProducts list
        foreach (Product product in products)
        {
            //reserved productMono
            Product reservedProduct = new Product(product);
            reservedProducts.Add(reservedProduct);

            //if productMono set as active by menu
            if (menu.activeProduct == product)
            {
                //set reserved productMono as active
                menu.reserveProduct = reservedProduct;

                //log
                Debug.Log("Reserved productMono set");
            }
        }
    }

    //is empty
    public bool IsEmpty()
    {
        return product == null;
    }
}
