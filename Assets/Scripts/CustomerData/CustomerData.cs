using UnityEngine;

[CreateAssetMenu(fileName = "NewCustomer", menuName = "WB/Customer")]
public class CustomerData : ScriptableObject
{
    public string customerName;
    public Sprite customerSprite;
    public string customerNeededFood;
    public int likeness;
}
