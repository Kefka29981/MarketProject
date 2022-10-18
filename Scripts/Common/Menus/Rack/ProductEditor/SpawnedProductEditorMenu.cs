using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpawnedProductEditorMenu : MenuSelectable
{
    //fields
    public Button button;

    public TextMeshProUGUI text;

    public Product activeProduct;

    public int index;

    public GameObject productObject;



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

            //update text
            textHolder.UpdateText();
        }
    }

    public void IncreaseSize(string axis)
    {

        //string to axis
        Axis axisEnum = AxisExtensions.ToAxis(axis);


        activeProduct.IncrementAmount(axisEnum, 1);



        textHolder.UpdateText();

    }

    public void DecreaseSize(string axis)
    {

        //string to axis
        Axis axisEnum = AxisExtensions.ToAxis(axis);

        activeProduct.IncrementAmount(axisEnum, -1);

        textHolder.UpdateText();
    }

    //rotate active productMono XY by 90 degrees
    public void RotateActiveProductXY()
    {


        activeProduct.rotation.RotateXY(activeProduct);

        textHolder.UpdateText();

    }

    //rotate active productMono YZ by 90 degrees
    public void RotateActiveProductYZ()
    {


        activeProduct.rotation.RotateYZ(activeProduct);


        textHolder.UpdateText();
    }

    //delete active product
    public void DeleteActiveProduct()
    {
        //destroy productmono gameobject
        Destroy(productObject);
        //default menu
        MenuHandler.menuController.SetDefaultMenu();

    }

}



