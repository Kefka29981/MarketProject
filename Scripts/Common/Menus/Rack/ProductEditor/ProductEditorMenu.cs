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

    public RackMono rackMono;


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

            //update text
            textHolder.UpdateText();

        }
    }

    public void IncreaseSize(string axis)
    {
        //string to axis
        Axis axisEnum = AxisExtensions.ToAxis(axis);

        string backup = rackMono.rackData.CreateBackup();

        int NumberOfProducts = rackMono.rackData.products.Count;

        activeProduct.IncrementAmount(axisEnum, 1);

        rackMono.rackData.RecreateRack();

        //if the number of products not the same as before, load the backup
        if (NumberOfProducts != rackMono.rackData.products.Count)
        {
            rackMono.rackData.LoadBackup(backup);
        }

        textHolder.UpdateText();

        rackMono.Render();

    }

    public void DecreaseSize(string axis)
    {
        //string to axis
        Axis axisEnum = AxisExtensions.ToAxis(axis);

        string backup = rackMono.rackData.CreateBackup();

        int NumberOfProducts = rackMono.rackData.products.Count;

        activeProduct.IncrementAmount(axisEnum, -1);

        rackMono.rackData.RecreateRack();

        //if the number of products not the same as before, load the backup
        if (NumberOfProducts != rackMono.rackData.products.Count)
        {
            rackMono.rackData.LoadBackup(backup);
        }

        textHolder.UpdateText();

        rackMono.Render();
    }

    //rotate active product XY by 90 degrees
    public void RotateActiveProductXY()
    {
        
        string backup = rackMono.rackData.CreateBackup();

        int NumberOfProducts = rackMono.rackData.products.Count;

        activeProduct.rotation.RotateXY(activeProduct);

        rackMono.rackData.RecreateRack();

        //if the number of products not the same as before, load the backup
        if (NumberOfProducts != rackMono.rackData.products.Count)
        {
            rackMono.rackData.LoadBackup(backup);
        }

        textHolder.UpdateText();

        rackMono.Render();
    }

    //rotate active product YZ by 90 degrees
    public void RotateActiveProductYZ()
    {
        string backup = rackMono.rackData.CreateBackup();

        int NumberOfProducts = rackMono.rackData.products.Count;

        activeProduct.rotation.RotateYZ(activeProduct);

        rackMono.rackData.RecreateRack();

        //if the number of products not the same as before, load the backup
        if (NumberOfProducts != rackMono.rackData.products.Count)
        {
            rackMono.rackData.LoadBackup(backup);
        }

        textHolder.UpdateText();

        rackMono.Render();
    }
}
