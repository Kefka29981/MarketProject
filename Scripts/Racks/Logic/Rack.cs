using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class Rack
{
    //fields
    public int id;

    //size
    public float width;
    public float height;
    public float depth;

    //leftest point
    public float leftestPoint;

    //position
    public float x;
    public float y;

    //globalProducts
    public List<Product> products;

    //reserved globalProducts list (used to store old globalProducts when changing rack)
    public List<Product> reservedProducts;

    //methods
    public Rack(float width, float height, float depth, float x, float y)
    {
        this.id = 0;
        this.width = width;
        this.height = height;
        this.depth = depth;
        this.x = x;
        this.y = y;
        this.leftestPoint = 0;
        this.products = new List<Product>();
    }


    public Rack(Rack rack)
    {
        this.id = rack.id;
        this.width = rack.width;
        this.height = rack.height;
        this.depth = rack.depth;
        this.x = rack.x;
        this.y = rack.y;
        this.leftestPoint = rack.leftestPoint;
        this.products = new List<Product>();
        foreach (Product product in rack.products)
        {
            //create duplicate of productMono
            Product newProduct = new Product(product);
            this.AddProduct(newProduct);
        }
    }



    //add productMono to rack
    public void AddProduct(Product product)
    {
        //set parameters of productMono
        product.SetActualParameters();

        bool result = CanAddProduct(product);

        if (result)
        {
            products.Add(product);

            //set new x and y of productMono
            //x equals to leftest point plus half productMono width
            //y equals to height minus half productMono height
            product.x = leftestPoint + product.width / 2;
            product.y = product.height / 2;

            //update leftest point
            leftestPoint += product.width;

            //update z amount of productMono (check how many times it can be stacked in depth)
            //productMono.amount[Axis.Z] = (int)(depth / productMono.depth);

            product.rack = this;

        }


    }

    public void RemoveProduct(Product product)
    {
        
        products.Remove(product);

        //recreate rack
        RecreateRack(products);
    }

    //check if productMono can be added to rack
    public bool CanAddProduct(Product product, bool containmentCheck = false)
    {
        bool result = true;

        //apply rotation
        product.SetActualParameters();

        //get horizontal free space
        float freeSpace = this.width - this.leftestPoint;

        //compare productMono width with free space
        if (product.width > freeSpace)
        {
            result = false;
        }

        //compare height with rack height
        if (product.height > this.height)
        {
            result = false;
        }

        //compare depth with rack depth
        if (product.depth > this.depth)
        {
            result = false;
        }

        //if containment check is enabled
        if (containmentCheck)
        {
            //debug log result
            Debug.Log("Containment check result: " + result);
            //check if product already exists in rack
            if (products.Contains(product))
            {
                //if result was false before, send status update message log
                if (!result)
                {
                    //debug log
                    Debug.Log("Product already exists in rack");
                }
                result = true;
            }
        }


        return result;
    }


    //recreate rack with new list
    public void RecreateRack(List<Product> new_products)
    {
        //copy new_products to new list (to avoid clearing original list)
        new_products = new List<Product>(new_products);

        //TODO: refactor this
        ProductEditorMenu menu = MenuHandler.menuController.GetCurrentMenuScript() as ProductEditorMenu;

        //clear rack
        this.products = new List<Product>();

        //update leftest point
        this.leftestPoint = 0;

        foreach (Product product in new_products)
        {
            //check if productMono can be added to rack
            bool result = CanAddProduct(product);

            if (result)
            {
                //add productMono to rack
                AddProduct(product);
            }
            else
            {
                //print error message
                //Debug.Log("Product " + product.productData.id + " can't be added to rack");
                //recreate with reserved globalProducts
                RecreateRack(reservedProducts);
                //set reserve as active
                menu.SetReserveProductAsActive();
                break;
            }
        }
        

    }

    //recreate rack with same globalProducts
    public void RecreateRack()
    {
        RecreateRack(products);
    }

    //add productMono on certain index
    public void AddProductOnIndex(Product product, int index, bool containmentCheck = false)
    {
        //check if productMono can be added to rack
        bool result = CanAddProduct(product, containmentCheck: containmentCheck);

        if (result)
        {
            //add productMono to rack
            products.Insert(index, product);

            //recreate rack
            RecreateRack(products);
        }
        else
        {
            //print error message
            Debug.Log("Product " + product.productData.id + " can't be added to rack");
        }
    }

    //reposition product to index
    public void RepositionProduct(Product product, int index)
    {

        if (CanAddProduct(product, containmentCheck: true))
        {
            //remove product from rack
            RemoveProduct(product);

            //add product to new index
            AddProductOnIndex(product, index);
        }
    }

    //recreate without ghosts
    public void RecreateWithoutGhosts()
    {
        //create new list
        List<Product> new_products = new List<Product>();

        //add only globalProducts without ghosts
        foreach (Product product in products)
        {
            if (!product.isGhost)
            {
                new_products.Add(product);
            }
        }

        //recreate rack
        RecreateRack(new_products);
    }

    //update reserved globalProducts
    public void UpdateReservedProducts()
    {
        //create new list
        reservedProducts = new List<Product>();

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
}