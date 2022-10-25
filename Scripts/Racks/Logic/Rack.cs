using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace RackScene
{
    [Serializable]
    public class Rack : ProductHolder
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



        //add productMono to holder
        public override void AddProduct(Product product)
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

                product.holder = this;

            }


        }

        public override void RemoveProduct(Product product)
        {

            products.Remove(product);

            //recreate holder
            RecreateHolder(products);
        }

        //check if productMono can be added to holder
        public override bool CanAddProduct(Product product, bool containmentCheck = false)
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

            //compare height with holder height
            if (product.height > this.height)
            {
                result = false;
            }

            //compare depth with holder depth
            if (product.depth > this.depth)
            {
                result = false;
            }

            /*
             *We use this bool variable to check if the productMono is already in the holder
             *Otherwise, ghost could possibly block it's own real reflection
            */
            //if containment check is enabled
            if (containmentCheck)
            {
                //debug log result
                Debug.Log("Containment check result: " + result);
                //check if product already exists in holder
                if (products.Contains(product))
                {
                    //if result was false before, send status update message log
                    if (!result)
                    {
                        //debug log
                        Debug.Log("Product already exists in holder");
                    }
                    result = true;
                }
            }


            return result;
        }


        //recreate holder with new list
        public void RecreateHolder(List<Product> new_products)
        {
            //copy new_products to new list (to avoid clearing original list)
            new_products = new List<Product>(new_products);

            //TODO: refactor this
            ProductEditorMenu menu = MenuManager.instance.GetMenu(MenuID.ProductEditor) as ProductEditorMenu;

            //clear holder
            this.products = new List<Product>();

            //update leftest point
            this.leftestPoint = 0;

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
            RecreateHolder(products);
        }

        //add productMono on certain index
        public void AddProductOnIndex(Product product, int index, bool containmentCheck = false)
        {
            //check if productMono can be added to holder
            bool result = CanAddProduct(product, containmentCheck: containmentCheck);

            if (result)
            {
                //add productMono to holder
                products.Insert(index, product);

                //recreate holder
                RecreateHolder(products);
            }
            else
            {
                //print error message
                Debug.Log("Product " + product.productData.id + " can't be added to holder");
            }
        }

        //reposition product to index
        public void RepositionProduct(Product product, int index)
        {

            if (CanAddProduct(product, containmentCheck: true))
            {
                //remove product from holder
                RemoveProduct(product);

                //add product to new index
                AddProductOnIndex(product, index);
            }
        }

        //recreate without ghosts
        public override void RecreateWithoutGhosts()
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

            //recreate holder
            RecreateHolder(new_products);
        }

        //update reserved globalProducts
        public override void UpdateReservedProducts()
        {
            //create new list
            reservedProducts = new List<Product>();

            //TODO: refactor this
            //get according menu from menu handler
            ProductEditorMenu menu = MenuManager.instance.GetMenu(MenuID.ProductEditor) as ProductEditorMenu;

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
}

