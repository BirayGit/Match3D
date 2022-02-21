using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform spawnPoints;
    public GameObject itemParent;

    public void SpawnPrefabs()
    {
        GameObject spawnedItem;
        spawnedItem =  Instantiate(itemPrefab, spawnPoints.transform);

        
    }

}
