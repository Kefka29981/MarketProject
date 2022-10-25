using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuScript : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public MenuID id;

    public TextHolder textHolder;

    public bool IsSelectable;
}

