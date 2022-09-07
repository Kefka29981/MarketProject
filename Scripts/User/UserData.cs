using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : IData
{
    //fields
    public List<Store> stores;

    //path to user data folder
    public string path = "Assets/Resources/accounts/";
    
    //methods
    public void Save()
    {
        SaveLoadSystem.Save(this, path + ActiveSession.GetUsername(), "userdata");
    }
    
    public IData Load()
    {
        UserData data = new UserData();
        data = SaveLoadSystem.Load<UserData>(path + ActiveSession.GetUsername(), "userdata");
        stores = data.stores;

        return data;
    }

    //constructor
    public UserData()
    {
        stores = new List<Store>();
    }

    //constructor with stores
    public UserData(List<Store> stores)
    {
        this.stores = stores;
    }
}
