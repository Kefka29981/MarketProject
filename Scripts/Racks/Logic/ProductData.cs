using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RackScene
{
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
        public int pinpointX;
        public int pinpointY;

        public string tag;
        public string name;

        //constructor
        public ProductData(int id, float base_width, float base_height, float base_depth, bool canBePinned, string name = "Default", string tag = "default", bool canBePlacedOnTop = false, int pinpointX = 0, int pinpointY = 0)
        {
            this.id = id;
            this.base_width = base_width;
            this.base_height = base_height;
            this.base_depth = base_depth;
            this.name = name;
            this.tag = tag;
            this.canBePlacedOnTop = canBePlacedOnTop;
            this.canBePinned = canBePinned;
            this.pinpointX = pinpointX;
            this.pinpointY = pinpointY;
        }
    }
}

