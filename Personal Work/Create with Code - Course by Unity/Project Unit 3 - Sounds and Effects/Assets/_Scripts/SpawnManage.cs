using UnityEngine;

public class SpawnManage : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private Vector3 spawnPosition = new Vector3(25, 0, 0);
    private float startDelay = 2;
    private float spawnRate = 2;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", startDelay, spawnRate);
    }


    void SpawnObstacle()
    {
        if (playerControllerScript.gameOver == false)
            Instantiate(obstaclePrefab, spawnPosition, obstaclePrefab.transform.rotation);

    }
}
