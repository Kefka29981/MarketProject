using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneLoader_Main : SceneLoader
{
    //fields
    //path to user data file
    public string path = "Assets/Resources/accounts/";

    //acc data text
    public TextMeshProUGUI acc_data;

    public StoreListController storeListController;


    //override methods
    public override void LoadScene()
    {
        //update acc data text
        acc_data.text = "Logged in as " + ActiveSession.GetUsername();

        //load user data from according json
        UserData data = (UserData)SaveLoadSystem.Load<UserData>(path + ActiveSession.GetUsername(), "userdata.json");

        //debug log
        Debug.Log("Loaded user data. Number of stores: " + data.stores.Count);

        //set stores
        storeListController.user_data = data;
    }

    //save user data
    public void Save()
    {
        storeListController.user_data.Save();
    }

    //log out button
    public void LogOut()
    {
        //log out
        ActiveSession.LogOut();
    }
}
