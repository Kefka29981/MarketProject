using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace RackScene
{
    //this class contains logic to operate with filters
    public static class Filters
    {
        public static string[] appliedFilters;

        public static string[] GetTags()
        {
            Category category = CategoryController.Active;

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

            //load in applied filters
            appliedFilters = tags;

            return appliedFilters;
        }
    }

}
