using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public MenuID id;

    public TextHolder textHolder;

    public virtual void GetData(object data)
    {
        //override
    }
}

