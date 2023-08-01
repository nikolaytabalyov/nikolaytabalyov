using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //variables
    public GameObject[] animalPrefabs;
    // spawn ranges
    private float xSpawnRange = 10;
    private float zSpawnBotRange = 5;
    private float zSpawnTopRange = 15;
    // spawn points
    private float zSpawnPoint = 20;
    private float xSpawnPoint = 25;
    // time
    private float spawnDelay = 2;
    private float spawnInterval = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", spawnDelay, spawnInterval);
        InvokeRepeating("SpawnRandomAnimalFromLeft", spawnDelay, spawnInterval);
        InvokeRepeating("SpawnRandomAnimalFromRight", spawnDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnRandomAnimal()
    {
        int animalIndex = Random.Range(0, 3);
        Vector3 spawnPos = new Vector3(Random.Range(-xSpawnRange, xSpawnRange), 0, zSpawnPoint);
        Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);
    }
    void SpawnRandomAnimalFromLeft()
    {
        int animalIndex = Random.Range(3, 6);
        Vector3 spawnPos = new Vector3(-xSpawnPoint, 0, Random.Range(zSpawnBotRange, zSpawnTopRange));
        //animalPrefabs[animalIndex].transform.Rotate(Vector3.up, -90);
        Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);
        //animalPrefabs[animalIndex].transform.Rotate(Vector3.up, 90);
    }
    void SpawnRandomAnimalFromRight()
    {
        int animalIndex = Random.Range(6, animalPrefabs.Length);
        Vector3 spawnPos = new Vector3(xSpawnPoint, 0, Random.Range(zSpawnBotRange, zSpawnTopRange));
        //animalPrefabs[animalIndex].transform.Rotate(Vector3.up, 90);
        Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);
        //animalPrefabs[animalIndex].transform.Rotate(Vector3.up, -90);
    }
}
