using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipePagePanel : BaseUIPanel
{
    [Header("Object Reference")]
    [SerializeField] RecipeButtonPanel[] RecipeButtons;
    [SerializeField] Image[] IngredientImages;

    [Header("Asset Reference")]
    [SerializeField] KitchenRuntimeManagerSO KitchenRuntimeManagerSO;
    // 0 = fruit, so leave it null
    [SerializeField] Sprite[] IdToIngredientSprites;

    OrderController orderController;

    protected override void Initialization()
    {
        base.Initialization();
        orderController = OrderController.Instance;

        for (int i = 0; i < RecipeButtons.Length; i++)
        {
            RecipeButtons[i].SetRecipe(KitchenRuntimeManagerSO.AllCakeRecipes[i]);
            RecipeButtons[i].SetRecipePagePanel(this);
        }
        SetFocusRecipe(KitchenRuntimeManagerSO.AllCakeRecipes[0]);
    }

    public void SetFocusRecipe(CakeRecipeData cakeRecipeData)
    {
        IngredientImages[0].sprite = cakeRecipeData.RequiredIngredients.Fruit.Sprite;
        for (int i = 1; i < 4; i++)
        {
            IngredientImages[i].sprite = IdToIngredientSprites[cakeRecipeData.RequiredIngredients.GetIngredientIdAt(i)];
        }
        //cakeRecipeData.RequiredIngredients.
    }


}
