using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductEditorMenu : MenuScript
{
    //fields
    public Button button;

    public TextMeshProUGUI text;

    public Product activeProduct;

    //reserve product to set as active
    public Product reserveProduct;

    public RackMono rackMono;

    public int index;


    public override void GetData(object data)
    {
        //try to cast the data to a Product
        ProductMono productBody = data as ProductMono;

        Product product = productBody.product;

        //if the cast was successful
        if (product != null)
        {
            //log
            Debug.Log("ProductEditorMenu: GetData: Product found");
            
            MenuHandler.SetNewSelectedObject(productBody as ISelectable);

            activeProduct = product;

            rackMono = productBody.rack;

            //update text
            textHolder.UpdateText();

        }
    }

    public void IncreaseSize(string axis)
    {
        //update reserves
        activeProduct.rack.UpdateReservedProducts();
        
        //string to axis
        Axis axisEnum = AxisExtensions.ToAxis(axis);


        activeProduct.IncrementAmount(axisEnum, 1);

        rackMono.rackData.RecreateRack();


        textHolder.UpdateText();

        rackMono.Render();

        

    }

    public void DecreaseSize(string axis)
    {
        //update reserves
        activeProduct.rack.UpdateReservedProducts();
        
        //string to axis
        Axis axisEnum = AxisExtensions.ToAxis(axis);

        activeProduct.IncrementAmount(axisEnum, -1);

        rackMono.rackData.RecreateRack();


        textHolder.UpdateText();

        rackMono.Render();
    }

    //rotate active product XY by 90 degrees
    public void RotateActiveProductXY()
    {

        //update reserves
        activeProduct.rack.UpdateReservedProducts();


        activeProduct.rotation.RotateXY(activeProduct);

        rackMono.rackData.RecreateRack();


        textHolder.UpdateText();

        rackMono.Render();
    }

    //rotate active product YZ by 90 degrees
    public void RotateActiveProductYZ()
    {
        //update reserves
        activeProduct.rack.UpdateReservedProducts();


        activeProduct.rotation.RotateYZ(activeProduct);

        rackMono.rackData.RecreateRack();


        textHolder.UpdateText();

        rackMono.Render();
    }

    //set reserve product as active
    public void SetReserveProductAsActive()
    {
        //set active product
        activeProduct = reserveProduct;

        //update text
        textHolder.UpdateText();

        //debug log
        Debug.Log("ProductEditorMenu: SetReserveProductAsActive: Active product set");
    }

}
