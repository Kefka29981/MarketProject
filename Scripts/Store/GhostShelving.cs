using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class GhostShelving : MonoBehaviour, IDragDrop
{
    //rectTransform property realization
    public RectTransform rectTransform { get => rt_parent; }
    public bool RestrictionEnabled { get => true; }
    public Collider2D Restriction { get => col; }

    //public rectTransform
    [SerializeField]
    private RectTransform rt_parent;
    [SerializeField]
    private RectTransform rt;

    [SerializeField]
    private Collider2D col;

    //references to all corner size handlers
    [SerializeField] 
    public CornerResizer TopLeft;
    [SerializeField]
    public CornerResizer TopRight;
    [SerializeField]
    public CornerResizer BottomLeft;
    [SerializeField]
    public CornerResizer BottomRight;

    //resize method
    public void Resize()
    {
        //get the new size of the rectTransform (distances between the according corners) but always positive
        float width = Mathf.Abs(TopRight.rectTransform.anchoredPosition.x - TopLeft.rectTransform.anchoredPosition.x);
        float height = Mathf.Abs(TopLeft.rectTransform.anchoredPosition.y - BottomLeft.rectTransform.anchoredPosition.y);

        //set the new size
        rt.sizeDelta = new Vector2(width, height);

        //get new x and y position of the rectTransform
        float x = (TopLeft.rectTransform.anchoredPosition.x + TopRight.rectTransform.anchoredPosition.x) / 2;
        float y = (TopLeft.rectTransform.anchoredPosition.y + BottomLeft.rectTransform.anchoredPosition.y) / 2;

        //set the new position
        rt.anchoredPosition = new Vector2(x, y);
    }

    //Reconfigure method
    public void Reconfigure()
    {
        //move parent rt to current position
        rt_parent.position = transform.position;

        //set position to 0,0
        rt.anchoredPosition = Vector2.zero;

        //move all corner resizers to the according positions
        TopLeft.rectTransform.anchoredPosition = new Vector3(-rt.sizeDelta.x / 2, rt.sizeDelta.y / 2, 0);
        TopRight.rectTransform.anchoredPosition = new Vector3(rt.sizeDelta.x / 2, rt.sizeDelta.y / 2, 0);
        BottomLeft.rectTransform.anchoredPosition = new Vector3(-rt.sizeDelta.x / 2, -rt.sizeDelta.y / 2, 0);
        BottomRight.rectTransform.anchoredPosition = new Vector3(rt.sizeDelta.x / 2, -rt.sizeDelta.y / 2, 0);
    }
}
