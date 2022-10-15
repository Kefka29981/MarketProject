using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public partial class ProductMono : MonoBehaviour, IRender, ISelectable
{
    //fields
    public Product product;

    //image
    public Image image;

    //holder it placed on
    public AbstractProductHolderMono holder;

    public TextMeshProUGUI text;

    public bool isDragging = false;

    public void Clear()
    {
        //nothing for now
    }

    public void RenderDefault()
    {
        //update text
        string size = "Size: " + product.width + " X, " + product.height + " Y, " + product.depth + " Z";
        string amount = "Amount: " + product.amount[Axis.X] + " X, " + product.amount[Axis.Y] + " Y, " + product.amount[Axis.Z] + " Z";
        string rotation = "Rotation: " + product.rotation.rotations[Axis.X] + " X, " + product.rotation.rotations[Axis.Y] + " Y, " + product.rotation.rotations[Axis.Z] + " Z";


        text.text = size + "\n" + amount + "\n" + rotation;

        //if product is equal to active product from menu, select it
        if (product == MenuHandler.productEditorMenu.activeProduct && !product.isGhost)
        {
            //cast this as ISelectable
            ISelectable selectable = this as ISelectable;

            //select this
            selectable.Select();
        }
    }

    public void SetSize(float width, float height)
    {
        //set size
        image.rectTransform.sizeDelta = new Vector2(width, height);
    }

    //ISelectable methods and properties
    public MenuID menuID => MenuID.ProductEditor;
    
    public bool isSelected { get; set; }
    

    void ISelectable.OnSelect()
    {
        //if not ghost
        if (!product.isGhost)
        {
            //make text visible
            text.gameObject.SetActive(true);
        }

        //check pinpoint status
        CheckPinPointStatus();

        //get product editor menu
        ProductEditorMenu menu = MenuHandler.productEditorMenu;

        menu.DeactivationCheck();
    }


    void ISelectable.OnUnselect()
    {
        //if text not null
        if (text != null)
        {
            //make text invisible
            text.gameObject.SetActive(false);
        }
        
        CheckPinPointStatus();
    }

    //unselect trigger
    public bool UnselectTrigger()
    {
        //result
        bool result = false;

        //when right click
        if (Input.GetMouseButtonDown(1))
        {
            result = true;
        }

        //return result
        return result;
    }

    //OnRender empty virtual method
    public virtual void OnRender()
    {
        //nothing for now
    }

    
}
