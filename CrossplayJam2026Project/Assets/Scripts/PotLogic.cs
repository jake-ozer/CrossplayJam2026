using System;
using System.Collections.Generic;
using UnityEngine;



public class PotLogic : MonoBehaviour
{
    public List<string> ingredientsInPot = new List<string>();
    private List<string> correctIngredientList = new List<string> {"Eggs", "Goldfish", "Onions", "Pepper"}; 

    // [SerializeField] BaseHoldable tempObjectToAdd;
    // [SerializeField] BaseHoldable tempObjectToAddTwo;

    // [SerializeField] BaseHoldable tempObjectToAddThree;

    void Start()
    {
        // PrintList();
        // AddToPotFromHoldable(tempObjectToAdd);
        // AddToPotFromHoldable(tempObjectToAddTwo);
        // PrintList();
        // Debug.Log(CheckIngredientList());
        // AddToPotFromHoldable(tempObjectToAddThree);
        // PrintList();
        // Debug.Log(CheckIngredientList());   
    }


    /// <summary>
    /// Takes in an object of type BaseHoldable. Appends its name field to ingredientsInPot.
    /// Does not allow duplicate names. Sorts the list immediately after appending.
    /// </summary>
    /// <param name="ingredientToAdd"> object of type BaseHoldable to append to list</param>
    /// <returns>true if successfully added, false otherwise</returns>
    public bool AddToPotFromHoldable(BaseHoldable ingredientToAdd)
    {
        string ingrName = ingredientToAdd.name;

        if(ingredientsInPot.Contains(ingrName))
        {
            //Display "Cannot add Duplicates"
            return false;
        }
        else
        {
            //Display added to pot
            //Make player isHolding false, remove from held hand
            ingredientsInPot.Add(ingrName);
        }

        ingredientsInPot.Sort();
        return true;
    }


    /// <summary>
    /// Helper method for debugging purposes.
    /// Prints a list of ingredients in pot, none if there are none
    /// </summary>
    private void PrintList()
    {
        if(ingredientsInPot.Count == 0)
            Debug.Log("Pot is empty");
        else
        {
            for(int i = 0; i < ingredientsInPot.Count; i++)
            {
                Debug.Log(ingredientsInPot[i]);
            }
        }
    }

    /// <summary>
    /// Compares ingredientInPot list to correctIngredientList list
    /// </summary>
    /// <returns>true if they are identical, false otherwise</returns>
    public bool CheckIngredientList()
    {
        if(ingredientsInPot.Count != correctIngredientList.Count)
            return false;
        
        for(int i = 0; i < correctIngredientList.Count; i++)
        {
            if(ingredientsInPot[i] != correctIngredientList[i])
                return false;
        }

        return true;
    }
}
