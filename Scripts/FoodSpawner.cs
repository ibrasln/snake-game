using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{

    [SerializeField] GameObject foodPrefab;

    [SerializeField] Transform bottomBorder, topBorder, rightBorder, leftBorder;

    private void Start()
    {
        InvokeRepeating("SpawnFood", 3f, 4f);
    }

    public void SpawnFood()
    {
        int x = (int)Random.Range(leftBorder.position.x, rightBorder.position.x);
        int y = (int)Random.Range(bottomBorder.position.y, topBorder.position.y);

        Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity);
    }

}
