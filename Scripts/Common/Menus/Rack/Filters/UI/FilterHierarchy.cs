using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RackScene
{
    public class FilterHierarchy : MonoBehaviour

    {

        public List<ToCategoryButton> CategoriesButtons;
        

        public GameObject startingPoint;


        public ToCategoryButton InstantiateButton(Category category)
        {
            //instantiate button prefab
            GameObject button = PrefabManager.toCategoryButtonPrefab;

            //set parent
            button.transform.SetParent(startingPoint.transform, false);

            //get button script
            ToCategoryButton buttonScript = button.GetComponent<ToCategoryButton>();

            //add button to list
            CategoriesButtons.Add(buttonScript);

            //set category
            buttonScript.SetCategory(category);


            return buttonScript;
        }

        public void InstantiateAllButtons()
        {
            //clear buttons
            ClearButtons();

            //get list of all categories from root to active
            List<Category> parentCategories = CategoryController.FindParents(CategoryController.Active);

            //instantiate buttons for each category
            foreach (Category category in parentCategories)
            {
                InstantiateButton(category);
            }
            //log count
            Debug.Log("Buttons count: " + parentCategories.Count);
            SetButtonsYPosition();
        }

        //set all buttons y position
        public void SetButtonsYPosition()
        {
            foreach (ToCategoryButton button in CategoriesButtons)
            {
                //get button index
                int index = CategoriesButtons.IndexOf(button);

                //find child named Category Button
                Transform categoryButton = PrefabManager.toCategoryButtonPrefab.transform.Find("CategoryButton");

                //get button height
                float buttonHeight = categoryButton.GetComponent<RectTransform>().rect.height;

                //get button spacing
                float buttonSpacing = 5;

                //get total height
                float totalHeight = (index + 1) * buttonHeight;

                //set new y
                float newY = totalHeight + (index * buttonSpacing);

                //set new position
                button.transform.localPosition = new Vector3(0, -newY, 0);

            }
        }

        //clear all buttons
        public void ClearButtons()
        {
            foreach (ToCategoryButton button in CategoriesButtons)
            {
                Destroy(button.gameObject);
            }

            CategoriesButtons.Clear();
        }
    }
}
