using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public List<GameObject> gameObjects;

    void Start()
    {
        // refactor, get sprites from resources folder

        int index = Random.Range(0, gameObjects.Count); 
        GameObject.Instantiate(gameObjects[index], transform.position, Quaternion.identity);
    }

    
}
