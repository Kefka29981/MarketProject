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

    //products
    public List<Product> products;

    //reserved products list (used to store old products when changing rack)
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
            //create duplicate of product
            Product newProduct = new Product(product);
            this.AddProduct(newProduct);
        }
    }



    //add product to rack
    public void AddProduct(Product product)
    {
        //set parameters of product
        product.SetActualParameters();

        bool result = CanAddProduct(product);

        if (result)
        {
            products.Add(product);

            //set new x and y of product
            //x equals to leftest point plus half product width
            //y equals to height minus half product height
            product.x = leftestPoint + product.width / 2;
            product.y = product.height / 2;

            //update leftest point
            leftestPoint += product.width;

            //update z amount of product (check how many times it can be stacked in depth)
            //product.amount[Axis.Z] = (int)(depth / product.depth);

            product.rack = this;

        }


    }

    public void RemoveProduct(Product product)
    {
        products.Remove(product);

        //recreate rack
        RecreateRack(products);
    }

    //check if product can be added to rack
    public bool CanAddProduct(Product product)
    {
        bool result = true;

        //apply rotation
        product.SetActualParameters();

        //get horizontal free space
        float freeSpace = this.width - this.leftestPoint;

        //compare product width with free space
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
            //check if product can be added to rack
            bool result = CanAddProduct(product);

            if (result)
            {
                //add product to rack
                AddProduct(product);
            }
            else
            {
                //print error message
                Debug.Log("Product " + product.productData.id + " can't be added to rack");
                //recreate with reserved products
                RecreateRack(reservedProducts);
                //set reserve as active
                menu.SetReserveProductAsActive();
                break;
            }
        }
        

    }

    //recreate rack with same products
    public void RecreateRack()
    {
        RecreateRack(products);
    }

    //add product on certain index
    public void AddProductOnIndex(Product product, int index)
    {
        //check if product can be added to rack
        bool result = CanAddProduct(product);

        if (result)
        {
            //add product to rack
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

    //recreate without ghosts
    public void RecreateWithoutGhosts()
    {
        //create new list
        List<Product> new_products = new List<Product>();

        //add only products without ghosts
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

    //update reserved products
    public void UpdateReservedProducts()
    {
        //create new list
        reservedProducts = new List<Product>();

        //TODO: refactor this
        //get according menu from menu handler
        ProductEditorMenu menu = MenuHandler.menuController.GetCurrentMenuScript() as ProductEditorMenu;

        //clear reserved products list
        foreach (Product product in products)
        {
            //reserved product
            Product reservedProduct = new Product(product);
            reservedProducts.Add(reservedProduct);

            //if product set as active by menu
            if (menu.activeProduct == product)
            {
                //set reserved product as active
                menu.reserveProduct = reservedProduct;

                //log
                Debug.Log("Reserved product set");
            }

        }

    }
}