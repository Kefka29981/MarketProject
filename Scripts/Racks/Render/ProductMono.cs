using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProductMono : MonoBehaviour, IRender, ISelectable
{
    //fields
    public Product product;

    //image
    public Image image;

    //rack it placed on
    public RackMono rack;

    public TextMeshProUGUI text;

    public void Clear()
    {
        throw new System.NotImplementedException();
    }

    public void Render()
    {
        //update text
        string size = "Size: " + product.width + " X, " + product.height + " Y, " + product.depth + " Z";
        string amount = "Amount: " + product.amount[Axis.X] + " X, " + product.amount[Axis.Y] + " Y, " + product.amount[Axis.Z] + " Z";
        string rotation = "Rotation: " + product.rotation.rotations[Axis.X] + " X, " + product.rotation.rotations[Axis.Y] + " Y, " + product.rotation.rotations[Axis.Z] + " Z";


        text.text = size + "\n" + amount + "\n" + rotation;
    }

    //ISelectable methods
    public void Select()
    {
        //find menu controller game object
        GameObject menuControllerObject = GameObject.Find("MenuController");
        //get menu controller script
        MenuController menuController = menuControllerObject.GetComponent<MenuController>();
        //set product as selected product
        menuController.SetCurrentMenu(MenuID.ProductEditor, this);

        //make text visible
        text.gameObject.SetActive(true);
    }


    public void Unselect()
    {
        //if text not null
        if (text != null)
        {
            //make text invisible
            text.gameObject.SetActive(false);
        }
    }
}
