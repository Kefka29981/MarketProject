using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfMono : MonoBehaviour, IRender, ISelectable
{
    public void RenderDefault()
    {
        throw new System.NotImplementedException();
    }

    public void Clear()
    {
        throw new System.NotImplementedException();
    }

    public MenuID menuID { get; }
    public bool isSelected { get; set; }
    public bool UnselectTrigger()
    {
        throw new System.NotImplementedException();
    }

    //start
    public void Start()
    {
        ShelfList sl = new ShelfList();
    }
}
