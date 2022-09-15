using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class ProductPanel : MonoBehaviour
{
    //product data
    public ProductData productData;

    //todo: create static class to store prefabs
    //prefab for spawned product
    public GameObject spawnedProductPrefab;

    //methods
    //initiate product panel
    public void Init()
    {

    }

    //spawn product
    public void SpawnProduct()
    {
        FindProductPositioners();
        
        //instantiate product
        GameObject spawnedProduct = Instantiate(spawnedProductPrefab);

        //todo: move reference to static class
        //find shelving object
        GameObject shelving = GameObject.Find("Shelving");

        //set parent to this
        spawnedProduct.transform.SetParent(shelving.transform, false);

        //get product mono
        ProductMono productMono = spawnedProduct.GetComponent<ProductMono>();

        //create new product
        Product product = new Product(productData);
        //set product data
        productMono.product = product;

        //todo: use SetSize method instead
        //set size
        productMono.GetComponent<RectTransform>().sizeDelta = new Vector2(product.width, product.height);
    }

    //find all objects with component ProductPositioner
    public void FindProductPositioners()
    {
        //find all objects with component ProductPositioner
        ProductPositioner[] productPositioners = FindObjectsOfType<ProductPositioner>();

        //DESTROY THEM ALL
        foreach (ProductPositioner productPositioner in productPositioners)
        {
            Destroy(productPositioner.gameObject);
        }
    }
}
