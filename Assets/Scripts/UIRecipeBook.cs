using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRecipeBook : MonoBehaviour
{
    public Transform initParent;
    public List<GameObject> recipes;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject item in recipes)
        {
            GameObject.Instantiate(item, initParent);
        }
    }

}
