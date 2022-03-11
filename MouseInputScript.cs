using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputScript : MonoBehaviour
{

    private Vector3 mousePos;
    private Vector3 mousePosWorld;
    private Vector2 mousePosWorld2D;
    
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject player;
    private Animator playerAnim;

    private RaycastHit2D hit;

    private Vector2 targetPos;
    [SerializeField] private float speed;

    private bool isMoving;
    [SerializeField] private GameObject highlightCircle;
    
    
    void Awake(){
        playerAnim = player.GetComponent<Animator>();
    	highlightCircle.SetActive(false);
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Input.mousePosition;

            // World Space Coordinates
            mousePosWorld = cam.ScreenToWorldPoint(mousePos);

            // Raycast save
            mousePosWorld2D = new Vector2(mousePosWorld.x, mousePosWorld.y);
            hit = Physics2D.Raycast(mousePosWorld2D,Vector2.zero);

            // Test if hit a collider has
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Ground")
                {
                    targetPos = hit.point;
                    isMoving = true;
                    FlipPlayer();
                    
                    highlightCircle.SetActive(true);
                    highlightCircle.transform.position = targetPos;
                }
            }            
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, targetPos, speed*Time.deltaTime);
            playerAnim.SetBool("run",true);
        }

        if (player.transform.position.x == targetPos.x && player.transform.position.y == targetPos.y)
        {
            isMoving = false;
            playerAnim.SetBool("run",false);
            highlightCircle.SetActive(false);
        }
    }

    // Mehtod to Flip the player 
    private void FlipPlayer()
    {
        if (player.transform.position.x > targetPos.x)
        {
            player.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            player.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
