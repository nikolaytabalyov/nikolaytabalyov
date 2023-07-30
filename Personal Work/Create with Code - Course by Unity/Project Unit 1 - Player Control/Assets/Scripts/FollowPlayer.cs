using System.Collections;
using System.Collections.Generic;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;
    public Camera cam;
    private Vector3 offset;
    
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, cam.transform.position.y, cam.transform.position.z);
        //offset = cam.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Player.transform.position + offset;
    }
}
