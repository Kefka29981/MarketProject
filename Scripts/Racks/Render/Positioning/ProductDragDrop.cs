using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductDragDrop : MonoBehaviour, IDragDrop
{

    //rectTransform property realization
    public RectTransform rectTransform
    {
        get
        {
            return productMono.GetComponent<RectTransform>();
        }
    }

    public GhostProduct ghost;

    public ProductMono productMono;

    public RackMono rack;

    public bool IsDragging { get => productMono.isDragging; set => productMono.isDragging = value; }

    //on pointer down
    public void PointerDown()
    {
        //set productMono as ghost
        productMono.product.isGhost = true;
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
        rack = productMono.rack;

        IsDragging = true;

        //set productMono as ghost
        productMono.product.isGhost = true;

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
        //set productMono as not ghost
        productMono.product.isGhost = false;
        
        IsDragging = false;

        //stop timer
        StopCoroutine(Timer());
        
        //render rack
        rack.Render();

        //render old rack (reference in productMono)
        productMono.rack.Render();
    }

    private void TimerAlarm()
    {
        //TODO: check if productMono belongs to certain rack
        if (rack == null)
        {
            //assign rack
            rack = productMono.rack;
        }

        //check rack
        CheckRack();

        int x_coor = (int)productMono.transform.localPosition.x;
        int index = ClosestIndex(rack, x_coor);
        //if closest index not equal to current ghost index on rack
        if (rack.rackData.products.IndexOf(productMono.product) != index)
        {
            //reposition product
            rack.rackData.RepositionProduct(productMono.product, index);

            
        }

        //TODO: render only if something changed
        //render rack
        rack.Render();
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
            //Debug.Log("Hello");
        }

        //if not null and not equal to current rack
        if (newRack != null && newRack != rack && newRack.rackData.CanAddProduct(productMono.product))
        {
            //TODO: ddn't remove if can't add
            //add productMono to new rack
            newRack.rackData.AddProduct(productMono.product);

            //remove productMono from current rack
            rack.rackData.products.Remove(productMono.product);

            //recreate old rack without ghosts
            rack.rackData.RecreateWithoutGhosts();

            //render old rack
            rack.Render();
            
            //set new rack
            rack = newRack;


        }
    }

    
}
