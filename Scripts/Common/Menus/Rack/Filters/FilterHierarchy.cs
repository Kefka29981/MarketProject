using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FilterHierarchy : MonoBehaviour

{
    public CategoryController categoryController;

    public List<ToCategoryButton> CategoriesButtons;

    public GameObject ToCategoryButtonPrefab;

    public GameObject startingPoint;

    //instantiate prefab and add it to list
    void Start()
    {
        //find category controller
        categoryController = GetComponentInParent<FilterMenu>().categoryController;
        
        //instantiate prefab
        InstantiateButton();
        InstantiateButton();
        InstantiateButton();
        InstantiateButton();
        InstantiateButton();
        InstantiateButton();

    }

    public ToCategoryButton InstantiateButton()
    {
        //instantiate button prefab
        GameObject button = Instantiate(ToCategoryButtonPrefab);

        //set parent
        button.transform.SetParent(startingPoint.transform, false);

        //get button script
        ToCategoryButton buttonScript = button.GetComponent<ToCategoryButton>();

        //add button to list
        CategoriesButtons.Add(buttonScript);

        //set category to active
        buttonScript.SetCategory(categoryController.Active);

        //set Y position for all buttons
        SetButtonsYPosition();

        return buttonScript;
    }

    //set all buttons y position
    public void SetButtonsYPosition()
    {
        foreach (ToCategoryButton button in CategoriesButtons)
        {
            //get button index
            int index = CategoriesButtons.IndexOf(button);

            //find child named Category Button
            Transform categoryButton = ToCategoryButtonPrefab.transform.Find("CategoryButton");

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
}