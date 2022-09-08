using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Mouse
{
    //methods
    public static T IsOverObject<T>() where T : MonoBehaviour
    {
        //get mouse position
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //send raycast
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        //check if hit
        if (hit.collider != null)
        {
            //check if the object is of type T
            if (hit.collider.gameObject.GetComponent<T>() != null)
            {
                return hit.collider.gameObject.GetComponent<T>();
            }
        }
        return null;
    }
}
