using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

 public class  PlayerMovement : MonoBehaviour 
{
  [SerializeField] private float speed = 3f;

  private Rigidbody2D playerRb;
  private Vector2 moveInput;

  public Vector2 MoveInput => moveInput;
   
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        Keyboard kb = Keyboard.current;
        if (kb == null)
        {
            moveInput = Vector2.zero;
            return;
        }

        float moveX = 0f;
        float moveY = 0f;

        if (kb.aKey.isPressed || kb.leftArrowKey.isPressed) moveX -= 1f;
        if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) moveX += 1f;
        if (kb.sKey.isPressed || kb.downArrowKey.isPressed) moveY -= 1f;
        if (kb.wKey.isPressed || kb.upArrowKey.isPressed) moveY += 1f;

        moveInput = new Vector2(moveX, moveY).normalized;
  }

    private void FixedUpdate()
    {
        playerRb.MovePosition(playerRb.position + moveInput * speed * Time.fixedDeltaTime);
    } 

}