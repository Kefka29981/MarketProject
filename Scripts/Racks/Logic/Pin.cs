using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin
{
    //products could be pinned here

    //depth
    public int depth;

    //position
    public Vector2 position;

    //constructor
    public Pin(int depth, Vector2 position)
    {
        this.depth = depth;
        this.position = position;
    }

    //pin product
    public void PinProduct(Product product)
    {
        //pin product
    }
}
