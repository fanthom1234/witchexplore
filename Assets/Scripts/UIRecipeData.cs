using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRecipeData : MonoBehaviour
{
    public CakeRecipeData recipeData;
    public UnityEngine.UI.Image image;
    public TMP_Text ingredients;
    public TMP_Text instructions;

    private void Start() {
        ChangeImage(recipeData.ResultBaseCake.CakeSprite);
        ingredients.text = recipeData.craftingString;
        instructions.text = recipeData.instructions;
    }

    public void ChangeImage(Sprite newSprite)
    {
        image.sprite = newSprite;
    }
}
