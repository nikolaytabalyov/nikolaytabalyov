using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private Rigidbody enemyRb;
    [SerializeField] private float speed = 3;
    private float lowerBound = -10.0f;

    void Start() {
        player = GameObject.Find("Player");
        enemyRb = GetComponent<Rigidbody>();
    }
    void FixedUpdate() {
        FollowPlayer();
        DestroyOffPlatform();
    }

    private void FollowPlayer(){
        Vector3 direction = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(direction * speed);
    }

    private void DestroyOffPlatform(){
        if(transform.position.y < lowerBound)
            Destroy(gameObject);
    }

}
