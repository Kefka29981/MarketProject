using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;



public static class CategoryController
{

    //fields
    //root category
    public static Category Root;

    //active category
    public static Category Active;

    //filter menu
    public static FilterMenu filterMenu;

    // Start is called before the first frame update
    public static void Start()
    {
        //get filter menu from menu handler
        filterMenu = MenuHandler.filterMenu;
        //create category tree
        CreateCategories();
        //load from json
        Root = LoadFromJSON();

        //set active category to root
        Active = Root;

        filterMenu.InstantiateFilters();
    }

    static void CreateCategories()
    {
        //create category alcohol
        Category alcohol = new Category("Alcohol", "alcohol");

        //create few subcategories
        Category wine = new Category("Wine", "wine");
        Category beer = new Category("Beer", "beer");
        Category liquor = new Category("Liquor", "liquor");

        //add subcategories to alcohol
        alcohol.AddSubcategory(wine);
        alcohol.AddSubcategory(beer);
        alcohol.AddSubcategory(liquor);

        //create category food
        Category food = new Category("Food", "food");

        //create few subcategories
        Category drinks = new Category("Drinks", "drinks");
        Category snacks = new Category("Snacks", "snacks");
        Category desserts = new Category("Desserts", "desserts");

        //add subcategories to food
        food.AddSubcategory(drinks);
        food.AddSubcategory(snacks);
        food.AddSubcategory(desserts);

        //create root category
        Category root = new Category("Root", "root");

        //add categories to root
        root.AddSubcategory(alcohol);
        root.AddSubcategory(food);

        //convert to json using newtonsoft.json
        string json = Newtonsoft.Json.JsonConvert.SerializeObject(root);

        json = JToken.Parse(json).ToString(Newtonsoft.Json.Formatting.Indented);

        //save to file
        System.IO.File.WriteAllText("Assets/Resources/categories.json", json);
    }

    //find category by tag
    public static Category FindCategory(string tag)
    {
        //create queue
        Queue<Category> queue = new Queue<Category>();

        //add root to queue
        queue.Enqueue(Root);

        //while queue is not empty
        while (queue.Count > 0)
        {
            //get category from queue
            Category category = queue.Dequeue();

            //if category tag is equal to tag
            if (category.tag == tag)
            {
                //return category
                return category;
            }

            //add subcategories to queue
            foreach (Category subcategory in category.subcategories)
            {
                queue.Enqueue(subcategory);
            }
        }

        //if category not found, exception
        throw new System.Exception("Category not found");
    }

    //find all parent categories of category
    public static List<Category> FindParents(Category category)
    {
        //create list of parents
        List<Category> parents = new List<Category>();

        //find parent by tag for categories until find root
        while (category.tag != "root")
        {
            //find parent by tag
            category = FindCategory(category.parent);

            //add parent to list
            parents.Add(category);
        }

        //reverse list
        parents.Reverse();

        //return list of parents
        return parents;
    }

    //set active category
    public static void SetActive(Category category)
    {
        Active = category;
        MenuHandler.filterMenu.InstantiateFilters();
        MenuHandler.filterMenu.filterHierarchy.InstantiateAllButtons();
    }



    //load from json file
    static public Category LoadFromJSON()
    {
        //load from json file
        string json = System.IO.File.ReadAllText("Assets/Resources/categories.json");

        //convert to object
        Category root = Newtonsoft.Json.JsonConvert.DeserializeObject<Category>(json);

        return root;
    }

    //static constructor
    static CategoryController()
    {
        Start();
    }


}
