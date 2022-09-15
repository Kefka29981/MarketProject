using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Category
{
    //name
    public string name;

    //tag
    public string tag;

    //subcategories
    public List<Category> subcategories;

    //parent category tag
    public string parent;
    
    //methods
    //constructor
    public Category(string name, string tag)
    {
        this.name = name;
        this.tag = tag;
        subcategories = new List<Category>();
    }

    //add subcategory
    public void AddSubcategory(Category subcategory)
    {
        subcategories.Add(subcategory);
        subcategory.parent = this.tag;
    }
    
}
