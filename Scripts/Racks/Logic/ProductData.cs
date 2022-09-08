using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductData
{   
    //fields
    public int id;

    //base size
    public float base_width;
    public float base_height;
    public float base_depth;

    public bool canBePinned;

    public bool canBePlacedOnTop;

    //constructor
    public ProductData(int id, float base_width, float base_height, float base_depth, bool canBePinned = false, bool canBePlacedOnTop = false)
    {
        this.id = id;
        this.base_width = base_width;
        this.base_height = base_height;
        this.base_depth = base_depth;
        this.canBePinned = canBePinned;
        this.canBePlacedOnTop = canBePlacedOnTop;
    }
}
