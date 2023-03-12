using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform ForwardOrintation;
    public Camera camera;
    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public virtual void SetupBehavior(Transform orientation, Camera cam, PlayerController playercontroller)
    {
        ForwardOrintation = orientation;
        camera = cam;
        player = playercontroller;
    }
    public virtual void UpdateCameraMovement()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraMovement();
    }
}
