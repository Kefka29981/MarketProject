using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//temporary static class for shelving
public static class Shelving
{
    //methods
    //todo: check if only single productMono corresponds to the product
    //find productMono by product
    public static ProductMono FindProductMonoByProduct(Product product)
    {
        //get all productMonos
        ProductMono[] productMonos = GameObject.FindObjectsOfType<ProductMono>();

        //loop through productMonos
        foreach (ProductMono productMono in productMonos)
        {
            //if the productMono's product is the same as the product
            if (productMono.product == product)
            {
                //return the productMono
                return productMono;
            }
        }

        //if no productMono was found
        return null;
    }


}
