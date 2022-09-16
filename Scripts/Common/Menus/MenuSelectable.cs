using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelectable : MenuScript
{
    //fields
    //selected ISelectable
    public ISelectable selected;

    //methods
    public virtual void Unselect()
    {
        //override
    }

    
    public virtual void CouldBeSelected()
    {
        //override
    }
}
