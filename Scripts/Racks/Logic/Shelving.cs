using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
//shelving contains a racks
public class Shelving
{
    //fields
    public List<Rack> racks;

    //size
    public int width;
    public int height;
    public int depth;

    //constructor
    public Shelving(int width, int height, int depth)
    {
        this.width = width;
        this.height = height;
        this.depth = depth;
        this.racks = new List<Rack>();
    }

    //check if the rack doesn't overlap with other racks
    public bool CheckOverlap(Rack rack)
    {
        //check if the rack is inside the shelving
        if (rack.x + rack.width > this.racks[0].width)
        {
            return false;
        }

        //check if the rack is inside the shelving
        if (rack.y + rack.height > this.racks[0].height)
        {
            return false;
        }

        //check if the rack doesn't overlap with other racks
        foreach (Rack otherRack in this.racks)
        {
            if (rack.x + rack.width > otherRack.x && rack.x < otherRack.x + otherRack.width)
            {
                if (rack.y + rack.height > otherRack.y && rack.y < otherRack.y + otherRack.height)
                {
                    return false;
                }
            }
        }

        return true;
    }

    //add new rack to the shelving at certain position and with certain size
    public void AddRack(int x, int y, int width, int height, int depth)
    {
        //create new rack
        Rack rack = new Rack(width, height, depth, x , y);

        //check if the rack doesn't overlap with other racks
        if (this.CheckOverlap(rack))
        {
            //add rack to the list
            this.racks.Add(rack);
        }
    }

    
}*/

//temporary static class for shelving
public static class Shelving
{
    //contain list of all rackMono
    public static List<RackMono> rackMonos = new List<RackMono>();

    //constructor
    static Shelving()
    {
        //find all rackMono on the scene
        foreach (RackMono rackMono in GameObject.FindObjectsOfType<RackMono>())
        {
            //add rackMono to the list
            rackMonos.Add(rackMono);
        }
    } 


}
