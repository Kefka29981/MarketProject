using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuSelectable : MenuScript
{
    //fields
    //selected ISelectable
    public ISelectable selectedObject;

    //methods
    public virtual void Unselect()
    {
        //when unselecting, show default menu
    }

    
    public virtual void CouldBeSelected()
    {
        //override
    }

    public virtual void SetCurrentObject(ISelectable newObject)
    {
        //override
    }
}
