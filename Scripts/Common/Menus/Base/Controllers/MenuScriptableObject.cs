using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Menu", menuName = "Menu", order = 1)]
public class MenuScriptableObject : ScriptableObject
{
    public List<MenuID> MenuList = new List<MenuID>();

    public MenuID MenuDefault;
}
