using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public int cloudNumber;
    public GameObject cloudPrefab;
    public int mapSize;
    void Start()
    {
        for (int i = 0; i < cloudNumber; i++)
        {
            Instantiate(cloudPrefab,  new Vector3(Random.Range(0, mapSize), 100, Random.Range(0, mapSize)), Quaternion.identity);
        }
    }
}
