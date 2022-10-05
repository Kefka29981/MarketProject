using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//never create an object of this class, it is only for inheritance!!!
//can't make it abstract because of monobehaviour inheritance
public class CornerResizer : MonoBehaviour, IDragDrop
{
    //rectTransform property realization
    public RectTransform rectTransform { get => rt; }
    public bool RestrictionEnabled { get => true; }

    public Collider2D Restriction
    {
        get => col;
    }

    //private rectTransform field
    [SerializeField]
    private RectTransform rt;

    //restriction collider
    [SerializeField]
    private Collider2D col;

    //two neighbour objects
    [SerializeField]
    private CornerResizer leftrightSide;

    [SerializeField]
    private CornerResizer topdownSide;

    [SerializeField]
    private GhostShelving ghostShelving;

    //override OnMove virtual method from IDragDrop interface
    void IDragDrop.OnMove()
    {
        //get the position of the object
        Vector2 pos = rectTransform.position;

        //get the position of the neighbour objects
        Vector2 posLR = leftrightSide.rectTransform.position;
        Vector2 posTD = topdownSide.rectTransform.position;

        //set the position of the neighbour objects
        leftrightSide.rectTransform.position = new Vector2(posLR.x, pos.y);
        topdownSide.rectTransform.position = new Vector2(pos.x, posTD.y);

        //resize the ghost shelving
        ghostShelving.Resize();
    }

    //override OnDragEnd virtual method from IDragDrop interface
    void IDragDrop.DragEnd()
    {
        ghostShelving.Reconfigure();
    }


}

