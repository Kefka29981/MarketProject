using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

//this component is attached to new product objects
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

    //bool to check if the product is in the rack
    public bool onRack = false;*/

    //update
    void Update()
    {
        Move();

        StatusCheck();

    }

    //start
    void Start()
    {
        //spawn new product
        ProductData productData = new ProductData(10, 20, 10,10);

        //create new product
        Product product = new Product(productData);

        //set as ghost
        //product.isGhost = true;

        //set productMono product
        productMono.product = product;

        
    }

    //void move
    public void Move()
    {
        //get mouse position
        Vector3 mousePosition = Input.mousePosition;

        //set z to 0
        mousePosition.z = 0;

        //set position to mouse position
        transform.position = mousePosition;
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
            //set rackmono as product mono parent
            productMono.transform.SetParent(actualRack.transform);
            //find closest index
            int x_coor = (int)productMono.transform.localPosition.x;
            int index = ClosestIndex(rackMono, x_coor);
            //add product at index
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

