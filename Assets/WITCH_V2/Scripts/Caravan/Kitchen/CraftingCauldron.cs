// This script handles the crafting system, ingredients and fruit are added by observer pattern
// The system uses event-based architecture to trigger crafting based on the addition of ingredients and fruit.
using AC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Event struct representing an ingredient being added to the cauldron.
public struct IngredientToCauldronEvent
{
    public Ingredient.EType Ingredient;

    public IngredientToCauldronEvent(Ingredient.EType type)
    {
        Ingredient = type;
    }
}
// Event struct representing a fruit being added to the cauldron.
public struct FruitToCauldronEvent
{
    public string FruitName;
    public FruitCakeIngredient FruitCakeIngredient;
    public FruitItemSO FruitItemSO;

    public FruitToCauldronEvent(FruitCakeIngredient ingredient)
    {
        FruitName = ingredient.FruitName;
        FruitCakeIngredient = ingredient;
        FruitItemSO = ingredient.FruitItemSO;
    }
}
// Evebt struct representing a cake got crafted
public struct CakeCraftedEvent
{
    public BaseCakeSO Result;

    public CakeCraftedEvent(BaseCakeSO resultBaseCake)
    {
        this.Result = resultBaseCake;
    }
}
// CraftingCauldron class that subscribes to ingredient and fruit events to manage the crafting process.
public class CraftingCauldron : CaravanObject, IEventSubcriber<IngredientToCauldronEvent>, IEventSubcriber<FruitToCauldronEvent>, IEventSubcriber<ObjectHoldEvent>
{
    //[Header("Setting")]
    //[SerializeField] float ShowResultDuration = 1; // Time to display the crafted cake.
    //[SerializeField] float ResultFadeoutDuration = .75f; // Time for the fade-out effect.

    [Header("Runtime Tracker")]
    [SerializeField] Ingredient IngredientsInCauldron; // List of ingredients added.

    [Header("Asset Reference")]
    [SerializeField] KitchenRuntimeManagerSO KitchenRuntimeManagerSO; // Reference to the Kitchen Manager, which contain All Recipe and Crafting Logic.
    [Header("Scene Object Reference")]
    [SerializeField] SpriteRenderer ResultCakeRenderer; // Renderer for the resulting cake image.
    [SerializeField] Hotspot CraftingHotSpot; // Hotspot that trigger Crafting Process
    [Header("Animation Reference")]
    [SerializeField] Animation Animation;
    [SerializeField] AnimationClip FailAnimClip;
    
    private Inventory _inventory;

    const string NOFRUIT = "";

    protected override void Initialization()
    {
        base.Initialization();
        Color c = Color.white;
        c.a = 0;
        ResultCakeRenderer.color = c;
        _inventory = Inventory.Instance;
    }

    protected override void OnObjectEnabled()
    {
        base.OnObjectEnabled();
        EventBusRegister.EventBusSubcribe<IngredientToCauldronEvent>(this);
        EventBusRegister.EventBusSubcribe<FruitToCauldronEvent>(this);
        EventBusRegister.EventBusSubcribe<ObjectHoldEvent>(this);
    }
    protected override void OnObjectDisable()
    {
        base.OnObjectDisable();
        EventBusRegister.EventBusUnscribe<IngredientToCauldronEvent>(this);
        EventBusRegister.EventBusUnscribe<FruitToCauldronEvent>(this);
        EventBusRegister.EventBusUnscribe<ObjectHoldEvent>(this);
    }

    /// <summary>
    /// Handle the event where an ingredient is added to the cauldron.
    /// </summary>
    /// <param name="eventType"></param>
    public void OnEventBusTrigger(IngredientToCauldronEvent eventType)
    {
        IngredientsInCauldron.Add(eventType.Ingredient);
    }

    /// <summary>
    /// Handle the event where a fruit is added to the cauldron.
    /// </summary>
    /// <param name="eventType"></param>
    public void OnEventBusTrigger(FruitToCauldronEvent eventType)
    {
        if (IngredientsInCauldron.Fruit == null)
        {
            IngredientsInCauldron.Fruit = eventType.FruitItemSO;
        }
        else
        {
            IngredientsInCauldron.Fruit = eventType.FruitItemSO;
        }
    }

    /// <summary>
    /// When object got hold, this hotspot will disable and vice versa
    /// </summary>
    /// <param name="eventType"></param>
    public void OnEventBusTrigger(ObjectHoldEvent eventType)
    {
        CraftingHotSpot.TurnOff();
        if (eventType.Holdable == null)
        {
            Invoke("TunrOnHotspot", eventType.Cooldown);
        }
    }

    private void TunrOnHotspot()
    {
        CraftingHotSpot.TurnOn();
    }

    /// <summary>
    /// Initiates the cake crafting process by checking for a valid fruit and ingredient combination.
    /// If both are present, it crafts the cake, shows the result, and clears the cauldron for the next use.
    /// </summary>
    public void StartCakeCrafting()
    {
        // Check if a fruit has been added to the cauldron.
        if (IngredientsInCauldron.Fruit == null)
        {
            Debug.Log("No fruit added");
        }
        // Check if any ingredients have been added to the cauldron.
        if (IngredientsInCauldron.Count <= 0)
        {
            Debug.Log("No ingredients added");
            return;
        }

        // Call the CraftCake method from the KitchenRuntimeManagerSO to determine the crafted recipe
        // based on the fruit and the ingredients added.
        CakeRecipeData getCake = KitchenRuntimeManagerSO.CraftCake(IngredientsInCauldron);
        if (getCake)
        {
            OnCraftingSuccess(getCake);
        }
        else
        {
            OnCraftingFail();
        }

        // Clear the cauldron of any fruit and ingredients for the next crafting session.
        ClearCauldron();
        CraftingHotSpot.TurnOff();
    }

    private void OnCraftingFail()
    {
        ResultCakeRenderer.sprite = null;
        //Animation.Play(FailAnimClip.name);
        EventBus.TriggerEvent(new CakeCraftedEvent(null));
    }

    private void OnCraftingSuccess(CakeRecipeData getCake)
    {
        ResultCakeRenderer.sprite = getCake.ResultBaseCake.CakeSprite;
        //_inventory.AddBaseCake(getCake.ResultBaseCake);
        EventBus.TriggerEvent(new CakeCraftedEvent(getCake.ResultBaseCake));
    }

    ///// <summary>
    /////  Coroutine to display the crafted cake and fade it out over time.
    ///// </summary>
    ///// <param name="result"></param>
    ///// <returns></returns>
    //private IEnumerator ShowResultCakeRoutine(CakeRecipeData result)
    //{
    //    ResultCakeRenderer.sprite = result.image;
    //    ResultCakeRenderer.color = Color.white;

    //    yield return new WaitForSeconds(ShowResultDuration);

    //    float _fadeTimer = 0;
    //    while (_fadeTimer < ResultFadeoutDuration)
    //    {
    //        _fadeTimer += Time.deltaTime;
    //        // change its alpha
    //        Color color = Color.white;
    //        color.a = (ResultFadeoutDuration-_fadeTimer) / ResultFadeoutDuration;
    //        ResultCakeRenderer.color = color;
    //        yield return new WaitForEndOfFrame();
    //    }
    //}

    /// <summary>
    /// Clear ingredients and fruit that was added to caludron
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void ClearCauldron()
    {
        IngredientsInCauldron.Clear();
    }

    /// <summary>
    /// Use in OnRelease Interaction of Hold_Milk
    /// </summary>
    /// <param name="i"></param>
    public void ForceAddIngredient(int i)
    {
        EventBus.TriggerEvent(new IngredientToCauldronEvent((Ingredient.EType)(i)));
    }
}
