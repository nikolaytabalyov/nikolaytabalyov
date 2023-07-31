using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //variables
    public GameObject[] animalPrefabs;
    private float xSpawnRange = 20;
    private float zSpawnPoint = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            int animalIndex = Random.Range(0,animalPrefabs.Length);
            Vector3 spawnPos = new Vector3(Random.Range(-xSpawnRange,xSpawnRange), 0, zSpawnPoint);
            Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);
        }
    }
}
