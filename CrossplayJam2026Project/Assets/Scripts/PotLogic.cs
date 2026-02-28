using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;



public class PotLogic : MonoBehaviour
{
    public List<string> ingredientsInPot = new List<string>();

    private StringBuilder currentIngredientsSB = new StringBuilder("Current Ingredients:\n");
    public string currentIngredients = "Current Ingredients:\n"; 
    public bool canAddToPot = true;
    private List<string> correctIngredientList = new List<string> {"Egg", "Goldfish", "Mushroom", "Onion", "Pepper"}; 

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
    /// Appends to currentIngredientsSB StringBuilder, sets currentIngredient.
    /// </summary>
    /// <param name="ingredientToAdd"> object of type BaseHoldable to append to list</param>
    /// <returns>true if successfully added, false otherwise</returns>
    public bool AddToPotFromHoldable(BaseHoldable ingredientToAdd)
    {

        if(!canAddToPot)
            return false;

        string ingrName = ingredientToAdd.name;

        if(ingredientsInPot.Contains(ingrName))
        {
            return false;
        }
        else
        {
            ingredientsInPot.Add(ingrName);
            currentIngredientsSB.Append(ingrName + "\n");
            currentIngredients = currentIngredientsSB.ToString();
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
