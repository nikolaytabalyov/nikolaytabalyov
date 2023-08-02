using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    private Vector3 startPosition;
    private float travelDistance;

    // Start is called before the first frame update
    void Start()
    {
        travelDistance = gameObject.GetComponent<BoxCollider>().size.x / 2;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < startPosition.x - travelDistance)
        {
            transform.position = startPosition;
        }
    }
}
