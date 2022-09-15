using UnityEngine;
using TMPro;

public class ToCategoryButton : MonoBehaviour

{

    private Category Category;

    public TextMeshProUGUI ButtonText;

    //set category and text
    public void SetCategory(Category category)
    {
        Category = category;
        ButtonText.text = category.name;
    }

    //go to category
    public void ToCategory()
    {
        //set active category
        CategoryController.SetActive(Category);

        //instantiate filters
        MenuHandler.filterMenu.InstantiateFilters();
    }


}