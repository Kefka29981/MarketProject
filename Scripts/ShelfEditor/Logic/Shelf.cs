using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf
{
    // The shelf's name
    public string name;

    // The shelf's position
    public float x1, y1, x2, y2; 

    //constructor
    public Shelf(string name, float x1, float y1, float x2, float y2)
    {
        this.name = name;
        this.x1 = x1;
        this.y1 = y1;
        this.x2 = x2;
        this.y2 = y2;
    }   
}

public class ShelfList
{
    // The list of shelves
    public List<Shelf> shelves = new List<Shelf>();

    //check if any shelf in list overlaps with any other shelf
    public bool CheckOverlap()
    {
        for (int i = 0; i < shelves.Count; i++)
        {
            for (int j = i + 1; j < shelves.Count; j++)
            {
                if (shelves[i].x1 < shelves[j].x2 && shelves[i].x2 > shelves[j].x1 && shelves[i].y1 < shelves[j].y2 && shelves[i].y2 > shelves[j].y1)
                {
                    return true;
                }
            }
        }
        return false;
    }

    //constructor
    public ShelfList()
    {
        shelves.Add(new Shelf("Shelf1", 0, 0, 10, 10));
        shelves.Add(new Shelf("Shelf2", 11, 11, 15, 15));

        if (CheckOverlap())
        {
            Debug.Log("Shelves overlap");
        }
        else
        {
            Debug.Log("Shelves do not overlap");
        }
    }
}