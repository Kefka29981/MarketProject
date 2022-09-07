using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropProduct : MonoBehaviour, IDragDrop
{

    //rectTransform property realization
    public RectTransform rectTransform
    {
        get
        {
            return product.GetComponent<RectTransform>();
        }
    }

    public GhostProduct ghost;

    public ProductMono product;

    public RackMono rack;

    public ShelvingMono shelving;

    public bool IsDragging = false;

    //coroutine to wait one second
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.5f);
        //trigger alarm
        TimerAlarm();
        //restart timer
        StartCoroutine(Timer());
    }


    public void DragStart()
    {
        //get rack
        rack = product.rack;

        IsDragging = true;

        //set product as ghost
        product.product.isGhost = true;

        //render rack
        rack.Render();

        //start timer
        StartCoroutine(Timer());
    }

    //on drop
    public void DragEnd()
    {
        //set product as not ghost
        product.product.isGhost = false;
        
        IsDragging = false;

        //stop timer
        StopCoroutine(Timer());
        
        //render rack
        rack.Render();
    }

    private void TimerAlarm()
    {
        //check if rack not null
        if (rack == null)
        {
            //get rack
            rack = product.rack;
        }


        int x_coor = (int)product.transform.localPosition.x;
        int y_coor = (int)product.transform.localPosition.y;
        
        
        
        int index = ClosestIndex(rack, x_coor);
        //if closest index not equal to current ghost index on rack
        if (rack.rackData.products.IndexOf(product.product) != index)
        {
            //remove product from rack
            rack.rackData.products.Remove(product.product);

            //add product to rack at closest index
            rack.rackData.AddProductOnIndex(product.product, index);

            //render rack
            rack.Render();
        }
    }    

    //Closest index on rack (if neigbours are 4 and 5, then closest index is 5)
    public int ClosestIndex(RackMono rack, int x_coordinate)
    {
        int closestIndex = 0;
        int closestDistance = int.MaxValue;

        
        for (int i = 0; i < rack.rackData.products.Count; i++)
        {
            int distance = (int)Mathf.Abs(rack.rackData.products[i].x - x_coordinate);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        return closestIndex;
    }

    //on pointer down
    public void PointerDown()
    {
        //set product as ghost
        product.product.isGhost = true;
    }

    //find closest rack using y coordinate
    public RackMono ClosestRack(List<RackMono> racks, int y_coordinate)
    {
        RackMono closestRack = null;
        int closestDistance = int.MaxValue;

        foreach (RackMono rack in racks)
        {
            int distance = (int)Mathf.Abs(rack.rackData.y - y_coordinate);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestRack = rack;
            }
        }

        return closestRack;
    }

    //if closest rack is not current rack
    public void RackChanged(Rack newRack)
    {
        //remove product from current rack
        rack.rackData.products.Remove(product.product);

        //add product to new rack
        newRack.AddProduct(product.product);

        //render old rack
        rack.Render();
    }

    /*public void RuckCheck()
    {
        //get closest rack
        RackMono closestRack = ClosestRack(shell.racks, (int)product.transform.localPosition.y);

        //if closest rack is not current rack
        if (closestRack != rack)
        {
            //change rack
            RackChanged(closestRack.rackData);
        }
    }*/
}

//123
