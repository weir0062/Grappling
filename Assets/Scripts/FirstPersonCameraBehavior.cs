using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCameraBehavior : CameraBehavior
{
    float XRotation;
    float YRotation;
    public float MouseXSensitivity = 1.0f;
    public float MouseYSensitivity = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
   public override void  UpdateCameraMovement() 
    {

        float MouseX = Input.GetAxisRaw("Mouse X") * MouseXSensitivity;
        float MouseY = Input.GetAxisRaw("Mouse Y") * MouseYSensitivity;

        YRotation += MouseX;
        XRotation -= MouseY;

        XRotation = Mathf.Clamp(XRotation, -90f, 90f);

        //       transform.rotation = Quaternion.Euler(XRotation, YRotation, 0);
        if(player)
        {

        player.transform.rotation = Quaternion.Euler(0, YRotation, 0);
        }
        ForwardOrintation.rotation = Quaternion.Euler(0, YRotation, 0);
        camera.transform.rotation = Quaternion.Euler(XRotation, camera.transform.rotation.eulerAngles.y, camera.transform.rotation.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
