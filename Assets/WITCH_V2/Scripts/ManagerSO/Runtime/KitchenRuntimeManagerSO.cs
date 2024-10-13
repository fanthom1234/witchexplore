// This scriptable object manages the runtime kitchen system, allowing for the crafting of cakes 
// based on the ingredients and fruit added. It uses predefined recipes to determine the crafting outcome.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Kitchen_RuntimeManager", menuName = "WB/Kitchen RT")]
public class KitchenRuntimeManagerSO : ScriptableObject
{
    [Header("References")]
    public List<CakeRecipeData> AllCakeRecipes; // List of all valid cake recipes.

    /// <summary>
    /// Crafts a cake based on the fruit and ingredients added to the cauldron.
    /// <param name="fruit"></param>
    /// <param name="ingredients"></param>
    /// <returns></returns>
    public CakeRecipeData CraftCake(Ingredient ingredients)
    {
        // Generate a crafting string based on the fruit and the ingredients present in the cauldron.
        string crafting = CakeRecipeData.GenerateCraftingString(ingredients);

        // Check if the crafting string matches any known recipe.
        foreach (CakeRecipeData recipeData in AllCakeRecipes)
        {
            if (recipeData.craftingString == crafting)
            {
                return recipeData; // Return the matching recipe.
            }
        }
        return null; // Return the fail recipe if no match is found.
    }
}
