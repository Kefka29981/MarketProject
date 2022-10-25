using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RackMenuController : MenuManager
{
    public override MenuID defaultMenu { get => MenuID.ProductList;}

    public override void Init()
    {
        //set instance reference to this
        instance = this;
    }
}
