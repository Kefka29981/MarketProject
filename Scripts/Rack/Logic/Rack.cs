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

    //methods
    public Rack(float width, float height, float depth, float x, float y)
    {
        this.width = width;
        this.height = height;
        this.depth = depth;
        this.x = x;
        this.y = y;
        this.products = new List<Product>();
    }


    [JsonConstructor]
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
            this.AddProduct(product);
        }
    }



    //add product to rack
    public void AddProduct(Product product)
    {
        //set parameters of product
        product.SetActualParameters();

        bool result = CanAddProduct(product);

        if(result)
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

    //create backup of rack as json string
    public string CreateBackup()
    {
        //create json string
        string json = JsonUtility.ToJson(this);

        return json;
    }

    //load backup from json string
    public void LoadBackup(string json)
    {
        //load json string
        Rack rack = JsonUtility.FromJson<Rack>(json);

        //copy rack to this
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
            this.AddProduct(product);
        }
    }

    //save as JSON
    public void SaveAsJSON()
    {
        //create json string
        string json = JsonConvert.SerializeObject(this);

        //create file and write json string to it
        System.IO.File.WriteAllText(@"Assets\Scripts\Rack\JSON\rack.json", json);
    }
    

}