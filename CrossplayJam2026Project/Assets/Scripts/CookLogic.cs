using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class CookLogic : MonoBehaviour
{
    [SerializeField] private PotLogic thePot;
    [SerializeField] private BaseHoldable theOmelette;

    [SerializeField] private PlayableDirector timelineDirector;

    [SerializeField] private Dictionary<string,string> adjectiveMap = new Dictionary<string, string>();

    


    void Start()
    {
        PopulateAdjectiveMap();
    }


    /// <summary>
    /// Checks if an omelette can be created.
    /// If so, creates an omelette object of type BaseHoldable
    /// if correct omelette, will have appropriate flag
    /// </summary>
    public void CookTheOmelette()
    {
        if(thePot.ingredientsInPot.Count == 0)
        {
            Debug.Log("Cannot cook without ingredients");
            return;
        }

        if(!thePot.ingredientsInPot.Contains("Egg"))
        {
            Debug.Log("Cannot make omelette without egg!");
            return;
        }

        bool isCorrectOmelette = thePot.CheckIngredientList();

        string omeletteName = BuildAdjectiveString();
        theOmelette.holdableName = omeletteName;
        theOmelette.isOmelette = true;
        theOmelette.correctOmelette = isCorrectOmelette;
        thePot.canAddToPot = false;

        timelineDirector.Play();

        Debug.Log(omeletteName);

        
    }

    /// <summary>
    /// Creates a string to describe the created omelette 
    /// based on used ingredients 
    /// </summary>
    /// <returns>A string to describe the omelette created</returns>
    private string BuildAdjectiveString()
    {
        StringBuilder adjectiveStringSB = new StringBuilder();

        for(int i = 0; i < thePot.ingredientsInPot.Count; i++)
        {
            if(adjectiveMap.ContainsKey(thePot.ingredientsInPot[i]))
                adjectiveStringSB.Append(adjectiveMap[thePot.ingredientsInPot[i]] + " ");

        }

        adjectiveStringSB.Append("Omelette");

        return adjectiveStringSB.ToString();
    }


    /// <summary>
    /// Helper method that adds all adjectives necessary to adjective hashmap
    /// </summary>
    private void PopulateAdjectiveMap()
    {
        adjectiveMap.Add("Tomato", "Tomatoey");
        adjectiveMap.Add("Onion", "Onioney");
        adjectiveMap.Add("Bacon", "Meaty");
        adjectiveMap.Add("Ham", "Thick");
        adjectiveMap.Add("Pepper","Spicy");
        adjectiveMap.Add("Cheese", "Cheezy");
        adjectiveMap.Add("Spinach","Leafey");
        adjectiveMap.Add("Mushroom","Shroomy");
        adjectiveMap.Add("Potato","Starchy");
        adjectiveMap.Add("Brocolli","Green");
        adjectiveMap.Add("Goldfish","Fishy");
    }
}
