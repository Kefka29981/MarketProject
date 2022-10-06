using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//static class that containse references to all prefabs

public static class PrefabStorage
{

    public static PrefabStorageMono PrefabStorageMono;

    //todo: change all fields references
    //prefabs
    public static GameObject productPrefab;
    public static GameObject rackPrefab;
    public static GameObject ghostPrefab;
    public static GameObject spawnedProductPrefab;
    public static GameObject filterPrefab;
    public static GameObject pinPoint;


    //load prefabs
    public static void LoadPrefabs()
    {
        //load prefabs
        productPrefab = PrefabStorageMono.productPrefab;
        rackPrefab = PrefabStorageMono.rackPrefab;
        ghostPrefab = PrefabStorageMono.ghostPrefab;
        spawnedProductPrefab = PrefabStorageMono.spawnedProductPrefab;
        filterPrefab = PrefabStorageMono.filterPrefab;
        pinPoint = PrefabStorageMono.pinPoint;
        

    }

    static PrefabStorage()
    {
        //find prefab storage
        PrefabStorageMono = GameObject.Find("SceneLoader").GetComponent<PrefabStorageMono>();
        PrefabStorage.LoadPrefabs();
    }
}
