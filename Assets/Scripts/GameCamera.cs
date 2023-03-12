using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    PlayerController player;
    CameraBehavior currentCameraBehavior;
    public Transform ForwardOrintation;

    public GameObject playerObject;
    
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        player = playerObject.GetComponent<PlayerController>();
        currentCameraBehavior = new FirstPersonCameraBehavior();
        currentCameraBehavior.SetupBehavior(ForwardOrintation, camera, player);
    }

    // Update is called once per frame
    void Update()
    {
        currentCameraBehavior.UpdateCameraMovement();
    }
}
