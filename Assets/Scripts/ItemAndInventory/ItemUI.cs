using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    public Item item;
    public TMP_Text textHolder;
    public Image imageHolder;

    private void Start() {
        if (item)
            Display(item);
    }

    public void Display(Item item)
    {
        this.item = item;
        // textHolder.text = item.itemName;
        imageHolder.sprite = item.itemSprite;
    }
}
