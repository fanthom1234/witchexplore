using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "WB/Recipe")]
public class RecipeData : ScriptableObject
{
    public string recipeName;
    public Sprite image;
    public string ingredients;
    public string instructions;
}
