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

    public bool IsDragging = false;

    //on pointer down
    public void PointerDown()
    {
        //set product as ghost
        product.product.isGhost = true;
    }

    //coroutine to wait one second
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.4f);
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

        //set rack as main
        rack.SetAsMain();

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

        //render old rack (reference in product)
        product.rack.Render();
    }

    private void TimerAlarm()
    {
        //TODO: check if product belongs to certain rack
        if (rack == null)
        {
            //assign rack
            rack = product.rack;
        }

        //check rack
        CheckRack();

        int x_coor = (int)product.transform.localPosition.x;
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

    //check if new rack should be applied
    public void CheckRack()
    {
        //check if mouse is over rack
        RackMono newRack = Mouse.IsOverObject<RackMono>();
        
        //if mouse is over rack
        if (newRack != null)
        {
            //say hello
            Debug.Log("Hello");
        }

        //if not null and not equal to current rack
        if (newRack != null && newRack != rack && newRack.rackData.CanAddProduct(product.product))
        {
            //TODO: ddn't remove if can't add
            //add product to new rack
            newRack.rackData.AddProduct(product.product);

            //remove product from current rack
            rack.rackData.products.Remove(product.product);

            //recreate old rack without ghosts
            rack.rackData.RecreateWithoutGhosts();
            
            //render old rack
            rack.Render();

            //set new rack
            rack = newRack;
        }
    }

    
}
