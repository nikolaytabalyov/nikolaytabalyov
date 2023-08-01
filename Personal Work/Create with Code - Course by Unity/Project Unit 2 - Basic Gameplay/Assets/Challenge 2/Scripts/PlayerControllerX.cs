using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    private double timeStart;
    private double curTime;
    // Update is called once per frame
    void Update()
    {
        curTime = Time.timeAsDouble;
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && (curTime - timeStart) > 0.5)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            timeStart = Time.timeAsDouble;
        }
        
    }
}
