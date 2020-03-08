using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Transform mainCamera;

    // Update is called once per frame
    void Update()
    {
        if (mainCamera.position.y > -10)
            mainCamera.position = new Vector3(player.position.x, player.position.y, mainCamera.position.z);
    }
}
