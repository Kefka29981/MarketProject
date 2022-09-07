using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using TMPro;

public class FilterMenu: MenuScript
{
    public CategoryController categoryController;

    public GameObject filterPrefab;

    public GameObject filtersListObject;

    //public TextMeshProUGUI Text;

    public string[] appliedFilters;

    //to root category
    public void ToRoot()
    {
        categoryController.Active = categoryController.Root;
        InstantiateFilters();

        //clear text
        //ClearText();
    }

    //Instantiate a filter buttons for each subcategory of active category
    public void InstantiateFilters()
    {
        //destroy all content of filters list object
        foreach (Transform child in filtersListObject.transform)
        {
            Destroy(child.gameObject);
        }

        //get active category
        Category active = categoryController.Active;

        //get subcategories
        List<Category> subcategories = active.subcategories;

        //instantiate filter buttons for each subcategory
        foreach (Category subcategory in subcategories)
        {
            //instantiate filter button
            GameObject filter = Instantiate(filterPrefab);

            filter.transform.SetParent(filtersListObject.transform, false);

            //set filter action
            //find filter button child
            Button filterButton = filter.transform.Find("Button").gameObject.GetComponent<Button>();
            
            filterButton.onClick.AddListener(() =>
            {
                categoryController.Active = subcategory;
                InstantiateFilters();
            });

            //set button text
            filter.transform.Find("Text").gameObject.GetComponent<Text>().text = subcategory.name;
        }

        //Update text
        //UpdateText();
    }

    //return all tags of active category and all subcategories (for example, root returns "root, alcohol, wine, beer, liquor, food, drinks, snacks, desserts")
    public void GetTags()
    {
        Category category = categoryController.Active;

        //category to json
        string json = Newtonsoft.Json.JsonConvert.SerializeObject(category);

        //using regex to get all tags from json
        //create regex
        Regex regex = new Regex(@"\""tag\"":\s*\""([^\""]*)\""");
        //get all tags
        MatchCollection matches = regex.Matches(json);

        Debug.Log(matches.Count);

        //create array of tags
        string[] tags = new string[matches.Count];
        //add tags to array
        for (int i = 0; i < matches.Count; i++)
        {
            tags[i] = matches[i].Groups[1].Value;
            Debug.Log(tags[i]);
        }

    }

    /*/update text field every time user changes active category
    public void UpdateText()
    {
        //if active category is not root
        if (categoryController.Active != categoryController.Root)
        {
        Text.text = (Text.text + " --> " + categoryController.Active.name);
        }
    }

    //clear text field
    public void ClearText()
    {
        Text.text = "";
    }*/
}
