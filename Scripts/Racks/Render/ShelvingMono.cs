using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class ShelvingMono : MonoBehaviour, IRender
{
    //fileds
    public Shelving shelving;

    [SerializeField]
    GameObject rackPrefab;

    //start
    void Start()
    {
        //debug log
        Debug.Log("ShelvingMono start");
        //create the shelving
        this.shelving = new Shelving(100, 100, 100);

        //create two racks
        this.shelving.AddRack(0, 0, 50, 50, 50);
        this.shelving.AddRack(0, 50, 50, 50, 50);

        Render();
    }


    //clear the shelving
    public void Clear()
    {
        //clear all racks
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    //draw the shelving
    public void Render()
    {
        //clear the shelving
        Clear();
        

        //draw the racks
        foreach (Rack rack in this.shelving.racks)
        {
            //create the rack
            //instantiate the rack prefab
            GameObject rackObject = Instantiate(rackPrefab);
            //set the rack parent
            rackObject.transform.SetParent(transform, false);
            //get RackMono component
            RackMono rackMono = rackObject.GetComponent<RackMono>();

            rackMono.rackData = rack;
            rackMono.Render();
        }
    }
}
*/