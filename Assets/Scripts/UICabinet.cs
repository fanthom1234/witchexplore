using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICabinet : MonoBehaviour
{
    public bool isShowRecipeBook = false;
    public GameObject RecipeBookGO;
    public List<ItemData> items;
    public UnityEngine.UI.Image imagePlaceholder;
    
    // Inventory
    public List<Image> images;

    private void Start() {
        RecipeBookGO.SetActive(false);

        Refresh();
    }

    public void ToggleRecipeBookGO()
    {
        isShowRecipeBook = !isShowRecipeBook;
        RecipeBookGO.SetActive(isShowRecipeBook);
    }

    public void RemoveItem(ItemData itemData)
    {
        StartCoroutine(RemoveItemRoutine(itemData));
    }

    IEnumerator RemoveItemRoutine(ItemData itemData)
    {
        items?.Remove(itemData);
        
        while (items.Contains(itemData))
        {
            yield return null; // Wait for one frame
        }

        Refresh();
    }

    public void ReplacePlaceholder(Sprite sprite)
    {
        imagePlaceholder.sprite = sprite;
    }

    public void Refresh()
    {
        ReplacePlaceholder(null);

        if (items.Count <= 0)
            return;

        // int i = 0;
        // foreach (var image in images)
        // {
        //     if (items?[i] != null)
        //     {
        //         image.sprite = items[i]?.sprite;
        //         image.name = items[i]?.itemName;
        //         image.GetComponent<UIItem>().item = items[i];
        //         i++;
        //     }
        //     else
        //     {
        //         image.sprite = null;
        //         image.name = "noimg";  
        //     }
        // }

        foreach (var image in images)
        {
            image.sprite = null;
        }

        int itemsCount = items.Count;
        for (int i = 0; i < itemsCount; i++)
        {
            images[i].sprite = items[i].sprite; 
            images[i].name = items[i]?.DisplayName;
            images[i].GetComponent<UIItem>().item = items[i];
        }
    }
}
