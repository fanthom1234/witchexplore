// This script defines a recipe for crafting cakes, where each recipe specifies required ingredients 
// and generates a unique crafting string based on the combination of a fruit and the ingredients needed.

using System;
using Unity.Collections;
using UnityEngine;

[System.Serializable]
public class Ingredient
{
    // Enum defining different types of ingredients.
    public enum EType
    {
        Flour, Milk, Egg, Butter, Sugar, 
    }

    public FruitItemSO Fruit; // The fruit required for the recipe.
    public int Flour; // Does the recipe require flour?
    public int Milk; // Does the recipe require milk?
    public int Egg; // Does the recipe require egg?
    public int Butter; // Does the recipe require ghee?
    public int Sugar; // Does the recipe require sugar?

    public int Count
    {
        get { return Flour + Milk + Egg + Butter + Sugar; }
    }

    public void Add(EType type)
    {
        Flour += type == EType.Flour ? 1 : 0;
        Milk += type == EType.Milk ? 1 : 0;
        Egg += type == EType.Egg ? 1 : 0;
        Butter += type == EType.Butter ? 1 : 0;
        Sugar += type == EType.Sugar ? 1 : 0;
    }
    public void Clear()
    {
        Fruit = null;
        Flour = 0;
        Milk = 0;
        Egg = 0;
        Butter = 0;
        Sugar = 0; 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public int GetIngredientIdAt(int i)
    {
        int count = 0;

        count += Flour;
        if (count == i) { return 1; }
        count += Milk;
        if (count == i) { return 2; }
        count += Egg;
        if (count == i) { return 3; }
        count += Butter;
        if (count == i) { return 4; }
        count += Sugar;
        if (count == i) { return 5; }

        return 0;
    }
}

[CreateAssetMenu(fileName = "NewRecipe", menuName = "WB/Recipe")]
public class CakeRecipeData : ScriptableObject
{
    public string recipeName;
    public BaseCakeSO ResultBaseCake;
    [Header("Ingredients")]
    public Ingredient RequiredIngredients = new Ingredient();

    [ReadOnly]
    public string craftingString; // Unique string representing the combination of fruit and ingredients.
    public string instructions = "lorem ipsum";

    // Automatically updates the crafting string when the recipe is modified in the editor.
    private void OnValidate()
    {
        craftingString = GenerateCraftingString(RequiredIngredients);
        recipeName = RequiredIngredients.Fruit.DisplayName + " Cake";
    }

    /// <summary>
    /// Static method to generate the crafting string based on the fruit and the ingredients.
    /// </summary>
    public static string GenerateCraftingString(Ingredient ingres)
    {
        string s = ingres.Fruit.DisplayName;
        for (int i = 0; i < ingres.Flour; i++)
        {
            s += "flour";
        }
        for (int i = 0; i < ingres.Milk; i++)
        {
            s += "milk";
        }
        for (int i = 0; i < ingres.Egg; i++)
        {
            s += "egg";
        }
        for (int i = 0; i < ingres.Butter; i++)
        {
            s += "butter";
        }
        for (int i = 0; i < ingres.Sugar; i++)
        {
            s += "sugar";
        }
        return s;
    }
}
