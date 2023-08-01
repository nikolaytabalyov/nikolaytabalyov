using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CollisionsAndLifeDecr;

public class DestroyOutOfBounds : MonoBehaviour
{
    //variables
    private float topBound = 30.0f;
    private float lowerBound = -10;
    private float sideBound = 26;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DestroyTopOutOfBoundsAnimals();
        DestroySideOutOfBoundsAnimals();
    }

    void DestroySideOutOfBoundsAnimals()
    {
        if (transform.position.x > sideBound)
            Destroy(gameObject);
        if (transform.position.x < -sideBound)
            Destroy(gameObject);
    }

    void DestroyTopOutOfBoundsAnimals()
    {
        //if object hits top border => Destroy, if hits bottom border => Life--
        if (transform.position.z > topBound)
            Destroy(gameObject);
        else if (transform.position.z < lowerBound)
        {
            Destroy(gameObject);
            if (LivesAndScore.Lives > 0)
            {
                LivesAndScore.Lives--;
                Debug.Log($"Lives = {LivesAndScore.Lives}");
            }
        }
    }
}
