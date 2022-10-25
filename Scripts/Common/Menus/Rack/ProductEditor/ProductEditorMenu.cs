using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RackScene
{
    public class ProductEditorMenu : MenuSelectable
    {
        //fields
        public Button button;

        public TextMeshProUGUI text;

        //todo: get active product form ISelectable
        public Product activeProduct;

        //reserve productMono to set as active
        public Product reserveProduct;

        public AbstractProductHolderMono productHolderMono;

        public int index;

        public List<GameObject> deactivationPinList;



        //override SetCurrentObject
        public override void SetCurrentObject(ISelectable newProduct)
        {
            selectedObject = newProduct;
            //get as productMono
            ProductMono productMono = selectedObject as ProductMono;

            //get product
            activeProduct = productMono.product;

            //get product holder
            productHolderMono = productMono.holder;

            //get text holder and update text
            TextHolder textHolder = this.textHolder;

            //update text
            textHolder.UpdateText();

        }


        public void IncreaseSize(string axis)
        {
            //update reserves
            activeProduct.holder.UpdateReservedProducts();

            //string to axis
            Axis axisEnum = AxisExtensions.ToAxis(axis);


            activeProduct.IncrementAmount(axisEnum, 1);

            productHolderMono.productHolderData.RecreateHolder();


            textHolder.UpdateText();

            //get rackmono as IRender
            IRender rackRender = productHolderMono as IRender;
            rackRender.Render();



        }

        public void DecreaseSize(string axis)
        {
            //update reserves
            activeProduct.holder.UpdateReservedProducts();

            //string to axis
            Axis axisEnum = AxisExtensions.ToAxis(axis);

            activeProduct.IncrementAmount(axisEnum, -1);

            productHolderMono.productHolderData.RecreateHolder();


            textHolder.UpdateText();

            //get rackmono as IRender
            IRender rackRender = productHolderMono as IRender;
            rackRender.Render();
        }

        //rotate active productMono XY by 90 degrees
        public void RotateActiveProductXY()
        {

            //update reserves
            activeProduct.holder.UpdateReservedProducts();


            activeProduct.rotation.RotateXY(activeProduct);

            productHolderMono.productHolderData.RecreateHolder();


            textHolder.UpdateText();

            //get rackmono as IRender
            IRender rackRender = productHolderMono as IRender;
            rackRender.Render();
        }

        //rotate active productMono YZ by 90 degrees
        public void RotateActiveProductYZ()
        {
            //update reserves
            activeProduct.holder.UpdateReservedProducts();


            activeProduct.rotation.RotateYZ(activeProduct);

            productHolderMono.productHolderData.RecreateHolder();


            textHolder.UpdateText();

            //get rackmono as IRender
            IRender rackRender = productHolderMono as IRender;
            rackRender.Render();
        }

        //delete active product
        public void DeleteActiveProduct()
        {
            //remove from holder
            productHolderMono.productHolderData.RemoveProduct(activeProduct);

            //render holder
            productHolderMono.RenderDefault();


            //default menu
            MenuManager.instance.SetDefaultMenu();
        }

        //set reserve productMono as active
        public void SetReserveProductAsActive()
        {
            //set active productMono
            activeProduct = reserveProduct;

            //update text
            textHolder.UpdateText();

            //debug log
            Debug.Log("ProductEditorMenu: SetReserveProductAsActive: Active productMono set");
        }

        //check if deactivate
        public void DeactivationCheck()
        {
            //check if current holder is pin
            if (productHolderMono.holderType == HolderType.Pin)
            {
                //for each gameobject in deactivationPinList
                foreach (GameObject go in deactivationPinList)
                {
                    //deactivate
                    go.SetActive(false);
                }
            }
            else
            {
                //for each gameobject in deactivationPinList
                foreach (GameObject go in deactivationPinList)
                {
                    //activate
                    go.SetActive(true);
                }
            }
        }


    }

}

