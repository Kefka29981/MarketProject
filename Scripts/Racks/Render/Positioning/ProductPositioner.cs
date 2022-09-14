using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

//this component is attached to new productMono objects
//it always moves with the mouse

public class ProductPositioner : MonoBehaviour
{

    //field
    //productMono
    public ProductMono productMono;

    //rackmono
    public RackMono rackMono;

    /*/rackmono (last on mouse)
    public RackMono rackMonoLastMouseOver;

    //bool to check if the productMono is in the rack
    public bool onRack = false;*/

    //update
    void Update()
    {
        Move();

        StatusCheck();

        //on click
        if (Input.GetMouseButtonDown(0))
        {
            //if mouse is over rack
            if (rackMono != null)
            {
                //make product not ghost
                productMono.product.isGhost = false;
                //RENDER
                rackMono.Render();
                //DESTROY EVERYTHING
                Destroy(productMono.gameObject);
            }
        }
    }

    //start
    void Start()
    {
        //spawn new productMono
        ProductData productData = new ProductData(10, 20, 10,10);

        //create new productMono
        Product product = new Product(productData);

        

        //set productMono productMono
        productMono.product = product;

        //set as ghost
        productMono.product.isGhost = true;

        //isDragging = true
        productMono.isDragging = true;
    }

    //void move
    public void Move()
    {
        //get mouse position
        Vector3 mousePosition = Input.mousePosition;

        //set z position
        mousePosition.z = 10;

        //set productMono position
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    public void StatusCheck()
    {
        //check if mouse is over rack
        RackMono actualRack = Mouse.IsOverObject<RackMono>();
        

        if (actualRack != rackMono && rackMono != null)
        {
            OnOutOfRack();
        }

        if (actualRack != null)
        {
            OnOverRack(actualRack);
        }


    }

    private void OnOverRack(RackMono actualRack)
    {
        if (actualRack.rackData.CanAddProduct(productMono.product))
        {
            rackMono = actualRack;
            //set rackmono as productMono mono parent
            productMono.transform.SetParent(actualRack.transform);
            //remove product from rack
            actualRack.rackData.RemoveProduct(productMono.product);
            //find closest index
            int x_coor = (int)productMono.transform.localPosition.x;
            int index = ClosestIndex(rackMono, x_coor);
            //add productMono at index
            actualRack.rackData.AddProductOnIndex(productMono.product, index);

            //render
            rackMono.Render();
        }
    }

    private void OnOutOfRack()
    {
        //clear rack mono from ghosts
        rackMono.rackData.RecreateWithoutGhosts();

        //render rack
        rackMono.Render();

        //set as null
        rackMono = null;
    }

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
}

