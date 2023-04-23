using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    // Associated to the player gameObject in MainGame
    // Allows the player gameObject to move according to the commande of the player
    // Adapts the position of the hair accroding to the movement


    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    private Vector2 movement;

    private void Start()
    {
        // get animator and rb associated to the object (player)
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // processing inputs
        movement.x = Input.GetAxisRaw("Horizontal"); // value between -1 (left) and 1 (right)
        movement.y = Input.GetAxisRaw("Vertical"); // value between -1 (down) and 1 (up)
        
        // Adapt the position of the hair
        // Moving left or right or up
        if (movement.x == movement.y && movement.x > 0 || movement.x != movement.y && movement.y > 0 || movement.y==0 && movement.x != 0)
        {
            GameManager.manager.ChangeHairYPosition(true);
        }
        // moving down or idle
        else
        {
            GameManager.manager.ChangeHairYPosition(false);
        }

        // set peramators for movement animation  
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }


    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

}
