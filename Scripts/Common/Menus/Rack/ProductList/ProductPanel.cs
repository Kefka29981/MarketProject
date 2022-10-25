using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace RackScene
{
    public class ProductPanel : MonoBehaviour
    {
        //product data
        public ProductData productData;

        //button
        public Button button;

        public TextMeshProUGUI text;

        //todo: create static class to store prefabs
        //prefab for spawned product
        public GameObject spawnedProductPrefab;

        //methods
        //initiate product panel
        public void Init()
        {

            text.text = productData.name;

        }

        //spawn product
        public void SpawnProduct()
        {
            //spawn product
            PrefabManager.InstantiateProductPositioner(this);
        }
    }
}

