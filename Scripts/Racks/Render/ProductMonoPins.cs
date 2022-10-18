using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class ProductMono : MonoBehaviour, IRender, ISelectable
{
    //if product product data pin point is true spawn pinpoint
    public void SpawnPinPoint()
    {
        //if product product data pin point is true spawn pinpoint
        if (product.productData.canBePinned)
        {
            //spawn pinpoint
            GameObject pinPoint = Instantiate(PrefabStorage.pinPoint);
            //set parent
            pinPoint.transform.SetParent(transform);
            //set position
            pinPoint.transform.localPosition = new Vector2(product.productData.pinpointX, product.productData.pinpointY);

            //rename to PinPoint
            pinPoint.name = "PinPoint";
        }
    }

    //destroy pinpoint
    public void DestroyPinPoint()
    {
        //if there is child with tag PinPoint
        if (transform.Find("PinPoint"))
        {
            //destroy pinpoint
            Destroy(transform.Find("PinPoint").gameObject);
        }
    }

    //todo: refactor bug when multiple left clicks do shit
    public void CheckPinPointStatus()
    {
        bool result = false;
        //if product product data pin point is true spawn pinpoint
        if (product.productData.canBePinned)
        {
            //if default rotation and 1,1,x size
            if (product.rotation.IsDefault() && product.amount[Axis.X] == 1 && product.amount[Axis.Y] == 1)
            {
                //if selected spawn pin
                //todo: getcomponent without using getcomponent
                if (product == MenuHandler.ProductEditorMenu.activeProduct && !product.isGhost && MenuHandler.ProductEditorMenu.GetComponent<CanvasGroup>().alpha == 1)
                {
                    result = true;
                }
            }
        }

        //if true spawn pin
        if (result)
        {
            //pin not presented yet
            if (!transform.Find("PinPoint"))
            {
                SpawnPinPoint();
            }
        }
        //if false destroy pin
        else
        {
            DestroyPinPoint();
        }

        //debug log
        Debug.Log("PinPoint status: " + result);
    }


}
