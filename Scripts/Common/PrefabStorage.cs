using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//static class that containse references to all prefabs

public static class PrefabStorage
{

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
        productPrefab = Resources.Load<GameObject>("Prefabs/Rack/Product");
        rackPrefab = Resources.Load<GameObject>("Assets/Prefabs/Rack/Rack");
        ghostPrefab = Resources.Load<GameObject>("Prefabs/Rack/Ghost");
        spawnedProductPrefab = Resources.Load<GameObject>("Prefabs/Rack/SpawnedProduct");
        filterPrefab = Resources.Load<GameObject>("Prefabs/Rack/FilterPrefab");
        pinPoint = Resources.Load<GameObject>("Assets/Prefabs/Rack/PinPoint");
    }
}
