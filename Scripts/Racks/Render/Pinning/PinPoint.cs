using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinPoint : Image
{
    //this class shows little point in the position of pinning
    //methods
    public void Init()
    {
        //set color
        color = new Color(0, 0, 0, 0.5f);
    }

    //show
    public void Show()
    {
        //set color
        color = new Color(0, 0, 0, 0.5f);
    }

    //hide
    public void Hide()
    {
        //set color
        color = new Color(0, 0, 0, 0);
    }
}
