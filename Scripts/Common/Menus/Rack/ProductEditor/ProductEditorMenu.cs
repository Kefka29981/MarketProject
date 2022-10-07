using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductEditorMenu : MenuSelectable
{
    //fields
    public Button button;

    public TextMeshProUGUI text;

    public Product activeProduct;

    //reserve productMono to set as active
    public Product reserveProduct;

    public AbstractProductHolderMono productHolderMono;

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
            
            //MenuHandler.SetNewSelectedObject(productBody as ISelectable);

            activeProduct = product;

            productHolderMono = productBody.rack;

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

        productHolderMono.productHolderData.RecreateHolder();


        textHolder.UpdateText();

        //get rackmono as IRender
        IRender rackRender = productHolderMono as IRender;
        rackRender.Render();

        

    }

    public void DecreaseSize(string axis)
    {
        //update reserves
        activeProduct.rack.UpdateReservedProducts();
        
        //string to axis
        Axis axisEnum = AxisExtensions.ToAxis(axis);

        activeProduct.IncrementAmount(axisEnum, -1);

        productHolderMono.productHolderData.RecreateHolder();


        textHolder.UpdateText();

        //get rackmono as IRender
        IRender rackRender = productHolderMono as IRender;
        rackRender.Render();
    }

    //rotate active productMono XY by 90 degrees
    public void RotateActiveProductXY()
    {

        //update reserves
        activeProduct.rack.UpdateReservedProducts();


        activeProduct.rotation.RotateXY(activeProduct);

        productHolderMono.productHolderData.RecreateHolder();


        textHolder.UpdateText();

        //get rackmono as IRender
        IRender rackRender = productHolderMono as IRender;
        rackRender.Render();
    }

    //rotate active productMono YZ by 90 degrees
    public void RotateActiveProductYZ()
    {
        //update reserves
        activeProduct.rack.UpdateReservedProducts();


        activeProduct.rotation.RotateYZ(activeProduct);

        productHolderMono.productHolderData.RecreateHolder();


        textHolder.UpdateText();

        //get rackmono as IRender
        IRender rackRender = productHolderMono as IRender;
        rackRender.Render();
    }

    //delete active product
    public void DeleteActiveProduct()
    {
        //remove from holder
        productHolderMono.productHolderData.RemoveProduct(activeProduct);

        //render holder
        productHolderMono.RenderDefault();

        //default menu
        MenuHandler.menuController.SetDefaultMenu();
    }

    //set reserve productMono as active
    public void SetReserveProductAsActive()
    {
        //set active productMono
        activeProduct = reserveProduct;

        //update text
        textHolder.UpdateText();

        //debug log
        Debug.Log("ProductEditorMenu: SetReserveProductAsActive: Active productMono set");

        
        
    }

    
}
