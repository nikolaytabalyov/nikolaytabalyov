using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CollisionsAndLifeDecr;

public class CollisionsAndScoreIncr : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Destroy(other.gameObject);
        LivesAndScore.Score++;
        Debug.Log($"Score = {LivesAndScore.Score}");
    }
}
