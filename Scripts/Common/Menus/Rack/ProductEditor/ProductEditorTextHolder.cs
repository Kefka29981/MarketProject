using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProductEditorTextHolder : TextHolder
{
    //menu reference
    public ProductEditorMenu menu;

    //TODO: make logic index-free
    //override
    public override void UpdateText()
    {
        //get active product
        Product product = menu.activeProduct;

        int amount_width = product.amount[AxisExtensions.RotationWise(product, Axis.X)];

        int amount_height = product.amount[AxisExtensions.RotationWise(product, Axis.Y)];

        int amount_depth = product.amount[AxisExtensions.RotationWise(product, Axis.Z)];

        //if product is not null
        texts[0].text = amount_width.ToString();

        texts[1].text = amount_height.ToString();

        texts[2].text = amount_depth.ToString();
    }
}
