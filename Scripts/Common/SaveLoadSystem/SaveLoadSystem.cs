using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class SaveLoadSystem
{
    //methods
    //Save IData as json to folder
    public static void Save<T>(T data, string folder, string fileName) where T : IData
    {
        //save data
        string json = JsonUtility.ToJson(data);

        //save json to file in certain folder
        string path = folder + "/" + fileName;
        File.WriteAllText(path, json);
    }

    //Load IData from json from folder
    public static T Load<T>(string folder, string fileName) where T : IData
    {
        //load data
        string path = folder + "/" + fileName;
        string json = File.ReadAllText(path);
        //load from json
        T data = JsonUtility.FromJson<T>(json);
        //return data
        return data;
    }
}
