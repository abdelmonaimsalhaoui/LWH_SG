using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] 
    private float cameraSpeed;
    [SerializeField] 
    private Transform player;
    
    // The camera moves only between [leftEdge , rightEdge]
    [SerializeField] 
    private Transform leftEdge;
    [SerializeField] 
    private Transform rightEdge;

    private void Update()
    {
        // Follow Player 
        transform.position = new Vector3(Mathf.Clamp(player.position.x,leftEdge.position.x,rightEdge.position.x),0,-10);

    }
}
