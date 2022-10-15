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
            //TODO: replace with the direct reference to the rectTransform
            return rt;
        }
    }

    public bool RestrictionEnabled { get => false; }
    public Collider2D Restriction { get; }


    public GhostProduct ghost;

    public ProductMono productMono;

    [SerializeField]
    private RectTransform rt;

    public AbstractProductHolderMono holder;

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
        //get holder
        holder = productMono.holder;

        IsDragging = true;

        //set productMono as ghost
        productMono.product.isGhost = true;

        //set holder as main
        holder.SetAsMain();

        //render holder//get rackmono as IRender
        IRender rackRender = holder as IRender;
        rackRender.Render();

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

        //render holder//get rackmono as IRender
        IRender rackRender = holder as IRender;
        rackRender.Render();

        //render old holder (reference in productMono)
        //get rackmono as IRender
        IRender oldRackRender = productMono.holder as IRender;
        oldRackRender.Render();
    }

    private void TimerAlarm()
    {
        //TODO: check if productMono belongs to certain holder
        if (holder == null)
        {
            //assign holder
            holder = productMono.holder as RackMono;
        }

        //check holder
        TryToSwitchProductHolder();
        SetActualIndex();


        //TODO: render only if something changed
        //render holder
        //get rackmono as IRender
        IRender rackRender = holder as IRender;
        rackRender.Render();
    }    

    //Closest index on holder (if neigbours are 4 and 5, then closest index is 5)
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

    //check if new holder should be applied
    public void TryToSwitchProductHolder()
    {
        //check if mouse is over holder
        AbstractProductHolderMono newHolder = Mouse.IsOverObject<AbstractProductHolderMono>();
        
        //if mouse is over holder
        if (newHolder != null)
        {
            //say hello
            //Debug.Log("Hello");
        }

        //if not null and not equal to current holder
        if (newHolder != null && newHolder != holder && newHolder.productHolderData.CanAddProduct(productMono.product))
        {
            //TODO: didn't remove if can't add
            //add productMono to new holder
            newHolder.productHolderData.AddProduct(productMono.product);

            //remove productMono from current holder
            holder.productHolderData.RemoveProduct(productMono.product);

            //recreate old holder without ghosts
            holder.productHolderData.RecreateWithoutGhosts();

            //render old holder
            IRender rackRender = holder as IRender;
            rackRender.Render();

            //set new holder
            holder = newHolder;


            //set new parent for ProductMono
            productMono.transform.SetParent(holder.transform, false);

            //set productMono position vector 
            Mouse.MoveToMouse(productMono.transform);
        }
    }

    public void SetActualIndex()
    {
        //if abstractproductholdermono is holder
        if (holder.holderType == HolderType.Rack)
        {
            RackMono rack = holder as RackMono;
            //get holder
            int x_coor = (int)productMono.transform.localPosition.x;
            int index = ClosestIndex(holder as RackMono, x_coor);
            //if closest index not equal to current ghost index on holder
            if (rack.rackData.products.IndexOf(productMono.product) != index)
            {
                //reposition product
                rack.rackData.RepositionProduct(productMono.product, index);


            }
        }
    }


}
