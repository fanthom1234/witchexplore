using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIShop : MonoBehaviour
{
    // public List<GameObject> customers;
    public UnityEvent events;
    public GameObject currentCustomer; // save duay

    public List<CustomerData> queueCustomerData;

    public GameObject itemComponent;
    public ItemData item;
    public UICabinet cabinet;

    public GameObject customerPrefab;

    public Transform customerParent;
    public Transform initPos; 
    public Transform midPos; 
    public Transform endPos;
    
    public int speed = 5;

    private void OnEnable() {
        StartCoroutine(MoveObject(SpawnNewCustomer().transform, midPos.position, speed));
    }

    private void OnDisable() {
        Destroy(currentCustomer);
    }

    public void Sell(bool isSell)
    {
        if (currentCustomer == null)
                return;

        if (isSell)
        {
            StartCoroutine(MoveObject(currentCustomer.transform, endPos.position, speed, true));

            itemComponent.GetComponent<UIItem>().item = null;
            cabinet.RemoveItem(item);

            GetNewCustomer();
        }
        else
        {
            StartCoroutine(MoveObject(currentCustomer.transform, endPos.position, speed, true));

            GetNewCustomer();
        }
    }

    public void GetNewCustomer()
    {
        // save customer?

        // make new list
        
        List<CustomerData> newCustomerData = queueCustomerData;

        currentCustomer = null;

        if (newCustomerData.Count <= 0)
            return;

        newCustomerData.RemoveAt(0);

        queueCustomerData = newCustomerData;

        currentCustomer = SpawnNewCustomer();

        StartCoroutine(MoveObject(currentCustomer.transform, midPos.position, speed));
    }

    private GameObject SpawnNewCustomer()
    {
        // get customer from save or cache

        // else create new obj
        
        CustomerData currCustomerData = queueCustomerData?.First(); 
        // create GO -> assign values
        currentCustomer = Instantiate(customerPrefab);
        currentCustomer.name = currCustomerData.customerName;
        currentCustomer.transform.position = initPos.position;
        currentCustomer.GetComponentInChildren<TMP_Text>().text = currCustomerData.customerNeededFood;
        currentCustomer.transform.SetParent(customerParent, true);

        // save current customer!!

        // Set the position of the UI element (assuming initPos is a UI element)
        RectTransform rectTransform = currentCustomer.GetComponent<RectTransform>();
        // rectTransform.anchoredPosition = initPos.GetComponent<RectTransform>().anchoredPosition;

        // Add an Image component and set its sprite
        Image image = currentCustomer.GetComponent<Image>();
        image.sprite = currCustomerData.customerSprite;

        return currentCustomer;
    }

    IEnumerator MoveObject(Transform objectToMove, Vector3 endPosition, float moveSpeed, bool isDestroyAfter = false)
    {
        float currentDuration = 0;
        Vector3 startPosition = objectToMove.position;

        while (currentDuration < 1)
        {
            currentDuration += Time.deltaTime * moveSpeed;
            objectToMove.position = Vector3.Lerp(startPosition, endPosition, currentDuration);
            yield return null;
        }

        if (isDestroyAfter)
            Destroy(objectToMove.gameObject);
    }
}
