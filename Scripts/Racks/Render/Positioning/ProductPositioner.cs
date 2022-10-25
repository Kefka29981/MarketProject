using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

//this component is attached to new productMono objects
//it always moves with the mouse


//todo: make it so that the productMono is always in front of the holder

namespace RackScene
{
    public class ProductPositioner : MonoBehaviour
    {

        //field
        //productMono
        public ProductMono productMono;

        //rackmono
        public AbstractProductHolderMono holderMono;

        /*/rackmono (last on mouse)
    public RackMono rackMonoLastMouseOver;

    //bool to check if the productMono is in the holder
    public bool onRack = false;*/

        //timer coroutine
        IEnumerator Timer()
        {
            yield return new WaitForSeconds(0.3f);
            //trigger alarm
            TimerAlarm();
            //restart timer
            StartCoroutine(Timer());
        }


        //update
        void Update()
        {
            Move();

            //on click
            if (Input.GetMouseButtonDown(0))
            {
                //if mouse is over holder
                if (holderMono != null)
                {
                    //make product not ghost
                    productMono.product.isGhost = false;
                    //RENDER//render old holder//get rackmono as IRender
                    IRender rackRender = holderMono as IRender;
                    rackRender.Render();
                    //DESTROY EVERYTHING
                    Destroy(productMono.gameObject);
                }
            }

            //on w or s key
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            {
                //rotate product by X axis
                productMono.product.rotation.RotateXY(productMono.product);
                //set actual parameters
                productMono.product.SetActualParameters();

                //if mouse is over holder
                if (holderMono != null)
                {
                    //recreate without ghost
                    holderMono.productHolderData.RecreateWithoutGhosts();

                    //render
                    IRender holderRender = holderMono as IRender;
                    holderRender.Render();
                }

                productMono.GetComponent<RectTransform>().sizeDelta =
                    new Vector2(productMono.product.width, productMono.product.height);
                IRender productRender = productMono as IRender;
                productRender.Render();

            }

            //on a or d key
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                //rotate product by X axis
                productMono.product.rotation.RotateYZ(productMono.product);
                //set actual parameters
                productMono.product.SetActualParameters();

                //if mouse is over holder
                if (holderMono != null)
                {
                    //recreate without ghost
                    holderMono.productHolderData.RecreateWithoutGhosts();

                    //render
                    IRender holderRender = holderMono as IRender;
                    holderRender.Render();
                }

                productMono.GetComponent<RectTransform>().sizeDelta =
                    new Vector2(productMono.product.width, productMono.product.height);
                IRender productRender = productMono as IRender;
                productRender.Render();
            }
        }

        //start
        void Start()
        {
            /*/spawn new productMono
        ProductData productData = new ProductData(10, 209, 10,10);

        //create new productMono
        Product product = new Product(productData);

        

        //set productMono productMono
        productMono.product = product;*/

            //set as ghost
            productMono.product.isGhost = true;

            //isDragging = true
            productMono.isDragging = true;

            //spawn pinpoint
            productMono.SpawnPinPoint();

            //start coroutine
            StartCoroutine(Timer());
        }

        //void move
        public void Move()
        {
            //get mouse position
            Vector3 mousePosition = Input.mousePosition;

            //set z position
            mousePosition.z = 10;

            //set productMono position
            transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        }

        public void StatusCheck()
        {
            //check if mouse is over holder
            AbstractProductHolderMono actualHolder = Mouse.IsOverObject<AbstractProductHolderMono>();


            if (actualHolder != holderMono && holderMono != null)
            {
                OnOutOfRack();
            }

            if (actualHolder != null)
            {
                OnOverRack(actualHolder);
            }


        }

        private void OnOverRack(AbstractProductHolderMono actualHolder)
        {
            if (actualHolder.holderType == HolderType.Rack)
            {
                RackMono actualRack = actualHolder as RackMono;
                if (actualRack.rackData.CanAddProduct(productMono.product, containmentCheck: true))
                {
                    holderMono = actualRack;
                    //set rackmono as productMono mono parent
                    productMono.transform.SetParent(actualRack.transform);
                    //remove product from holder
                    actualRack.rackData.RemoveProduct(productMono.product);
                    //find closest index
                    int x_coor = (int)productMono.transform.localPosition.x;
                    int index = ClosestIndex(actualRack, x_coor);
                    //add productMono at index
                    actualRack.rackData.AddProductOnIndex(productMono.product, index);

                    //render//render old holder//get rackmono as IRender
                    IRender rackRender = holderMono as IRender;
                    rackRender.Render();
                }
            }

            //same for pin
            if (actualHolder.holderType == HolderType.Pin)
            {
                PinsMono actualPin = actualHolder as PinsMono;
                if (actualPin.pinData.CanAddProduct(productMono.product, containmentCheck: true))
                {
                    holderMono = actualPin;
                    //set rackmono as productMono mono parent
                    productMono.transform.SetParent(actualPin.transform);
                    //remove product from holder
                    actualPin.pinData.RemoveProduct(productMono.product);

                    //add productMono at index
                    actualPin.pinData.AddProduct(productMono.product);

                    //render//render old holder//get rackmono as IRender
                    IRender rackRender = holderMono as IRender;
                    rackRender.Render();
                }
            }

        }

        private void OnOutOfRack()
        {
            //clear holder mono from ghosts
            holderMono.productHolderData.RecreateWithoutGhosts();

            //get rackmono as IRender
            IRender rackRender = holderMono as IRender;
            rackRender.Render();

            //set as null
            holderMono = null;
        }

        public int ClosestIndex(RackMono rack, int x_coordinate)
        {
            int closestIndex = 0;
            int closestDistance = int.MaxValue;


            for (int i = 0; i < rack.rackData.products.Count; i++)
            {
                int distance = (int)Mathf.Abs(rack.rackData.products[i].x - x_coordinate);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }

            //if the closest index is the last one
            if (closestIndex == rack.rackData.products.Count - 1)
            {
                //if the x coordinate is bigger than the last product
                if (x_coordinate > rack.rackData.products[closestIndex].x)
                {
                    //add one to the index
                    closestIndex++;
                }
            }

            //log closest index
            Debug.Log("closest index: " + closestIndex);

            return closestIndex;
        }

        //timer alarm
        public void TimerAlarm()
        {
            StatusCheck();
        }
    }


}