using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProductHolder
{
    //contains logic reflection of the product holder mono
    //methdos
    public abstract void AddProduct(Product product);

    public abstract void RemoveProduct(Product product);


    //check if the product can be placed in the holder
    public abstract bool CanAddProduct(Product product, bool containmentCheck = false);

    //recreate holder
    public abstract void RecreateHolder();

    //recreate without ghosts
    public abstract void RecreateWithoutGhosts();

    public abstract void UpdateReservedProducts();
}
