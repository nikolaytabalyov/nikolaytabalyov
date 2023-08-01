using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CollisionsAndLifeDecr : MonoBehaviour
{
    // created a class for Lives and Score values so they can be accessed
    public static class LivesAndScore
    {
        // Initializing lives value
        private static int lives = 3;
        public static int Lives 
        {
            get { return lives; }
            set { lives = value; } 
        }
        // Initializing score value
        private static int score = 0;
        public static int Score
        {
            get { return score; }
            set { score = value; }
        }
    }

    private void Start()
    {
        Debug.Log($"Lives = {LivesAndScore.Lives}, Score = {LivesAndScore.Score}");
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        if (LivesAndScore.Lives > 0)
        {
            LivesAndScore.Lives--;
            Debug.Log($"Lives = {LivesAndScore.Lives}");
        }
    }
}
