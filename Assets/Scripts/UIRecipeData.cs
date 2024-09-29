using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRecipeData : MonoBehaviour
{
    public RecipeData recipeData;
    public UnityEngine.UI.Image image;
    public TMP_Text ingredients;
    public TMP_Text instructions;

    private void Start() {
        ChangeImage(recipeData.image);
        ingredients.text = recipeData.ingredients;
        instructions.text = recipeData.instructions;
    }

    public void ChangeImage(Sprite newSprite)
    {
        image.sprite = newSprite;
    }
}
