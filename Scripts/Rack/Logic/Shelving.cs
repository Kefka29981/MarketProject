using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public static class Shelving
{
    public static List<RackMono> racks = new List<RackMono>();

    //static constructor
    static Shelving()
    {
        //add all racks mono to list
        foreach (RackMono rack in GameObject.FindObjectsOfType<RackMono>())
        {
            racks.Add(rack);
        }
    }
}

