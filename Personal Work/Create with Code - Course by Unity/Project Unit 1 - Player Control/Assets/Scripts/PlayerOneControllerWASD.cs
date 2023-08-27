using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneControllerWASD : MonoBehaviour
{
    public static class StaticValues
    {
        private static bool drivercamon = false;
        public static bool DriverCamOn
        {
            get { return drivercamon; }
            set { drivercamon = value; }
        }
    }

    //private variables
    public Camera ThirdPerson;
    public Camera DriverCam;
    private float speed = 10.0f;
    private float turnSpeed = 35.0f;
    private float input, horizontal_input;
    private bool cameraButton;


    void CameraSwitch(bool cameraButton)
    {
        if (cameraButton == true)
        {
            switch (StaticValues.DriverCamOn)
            {
                case false:
                    ThirdPerson.enabled = false;
                    DriverCam.enabled = true;
                    StaticValues.DriverCamOn = true;
                    break;
                case true:
                    ThirdPerson.enabled = true;
                    DriverCam.enabled = false;
                    StaticValues.DriverCamOn = false;
                    break;
            }
        }
    }
   

    // Update is called once per frame
    private void Update() {
        //getting input
        input = Input.GetAxis("VerticalWASD");
        horizontal_input = Input.GetAxis("HorizontalWASD");
        cameraButton = Input.GetKeyDown(KeyCode.LeftShift);
    }
    private void FixedUpdate() {
        //camera switch 
        CameraSwitch(cameraButton);
        //moving
        transform.Translate(Vector3.forward * Time.deltaTime * speed * input);
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontal_input);
    }
}
