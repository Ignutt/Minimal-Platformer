using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float speed = 4f;
    public Transform player;
    public Vector3 offset = new Vector3(0, 0, -10);

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position,player.position + offset, speed * Time.deltaTime);
    }
}
