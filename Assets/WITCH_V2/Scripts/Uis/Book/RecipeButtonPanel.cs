using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeButtonPanel : BaseButtonPanel
{
    [Header("Object Reference")]
    [SerializeField] Image ButtonGraphic;
    [SerializeField] TextMeshProUGUI RecipeNameText;

    //[Header("Asset Reference")]
    //[SerializeField] KitchenRuntimeManagerSO KitchenRuntimeManagerSO;

    OrderController orderController;

    public CakeRecipeData ThisRecipe;
    private RecipePagePanel _recipePagePanel;

    protected override void Initialization()
    {
        base.Initialization();
    }

    protected override void OnClick()
    {
        base.OnClick();
        if (_recipePagePanel)
        {
            _recipePagePanel.SetFocusRecipe(ThisRecipe);
        }
    }

    public void SetRecipe(CakeRecipeData cakeRecipeData)
    {
        ThisRecipe = cakeRecipeData;
        ButtonGraphic.sprite = cakeRecipeData.ResultBaseCake.CakeSprite;
        RecipeNameText.text = cakeRecipeData.recipeName;
    }

    public void SetRecipePagePanel(RecipePagePanel recipePagePanel)
    {
        _recipePagePanel = recipePagePanel;
    }
}
