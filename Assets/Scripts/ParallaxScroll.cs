using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    private float length, startPosX;
    public GameObject cam;
    public float parallaxEffect;

    void Start()
    {
        startPosX = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        var currentPos = cam.transform.position.x * (1 - parallaxEffect);
        var horizontalDist = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(startPosX + horizontalDist, transform.position.y, transform.position.z);

        if (currentPos > startPosX + length)
            startPosX += length;
        else if (currentPos < startPosX - length)
            startPosX -= length;
    }
}
