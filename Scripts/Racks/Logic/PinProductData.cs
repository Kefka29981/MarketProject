using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinProductData : ProductData
{
    //fields
    public int id;

    //base size
    public float base_width;
    public float base_height;
    public float base_depth;

    public bool canBePinned
    {
        get => true;
    }

    public int pinpoint_x;
    public int pinpoint_y;

    public bool canBePlacedOnTop;

    public string tag;
    public string name;

    //constructor base class
    public PinProductData(int id, float base_width, float base_height, float base_depth, int pinX, int pinY, string name = "Default", string tag = "default", bool canBePlacedOnTop = false) : base(id, base_width, base_height, base_depth, name, tag, canBePlacedOnTop)
    {
        this.id = id;
        this.base_width = base_width;
        this.base_height = base_height;
        this.base_depth = base_depth;
        this.name = name;
        this.tag = tag;
        this.canBePlacedOnTop = canBePlacedOnTop;
    }
}
