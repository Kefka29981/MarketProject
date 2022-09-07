using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreListController : MonoBehaviour
{
    //fields
    GameObject store_list_panel_prefab;

    //userdata
    public UserData user_data;

    //list of store panels
    public List<StorePanel> store_panels;

    //button to add new store
    public Button add_store_button;

    //methods
    //start
    void Start()
    {
        //add listener to add store button
        add_store_button.onClick.AddListener(AddStore);
    }

    //add new store
    public void AddStore()
    {
        //create new store
        Store store = new Store("novus");
        //add store to list
        user_data.stores.Add(store);

        //debug log
        Debug.Log("Store added: " + store.name);
    }
}
