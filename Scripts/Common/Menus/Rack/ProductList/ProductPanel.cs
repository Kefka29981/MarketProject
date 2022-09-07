using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class ProductPanel : MonoBehaviour
{
    //fields
    //id of the stored product data
    public int id;

    //awake
    private void Awake()
    {
        //set id
        id = int.Parse(transform.name);
    }

    
}
