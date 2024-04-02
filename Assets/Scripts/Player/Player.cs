using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 7f;
    private Rigidbody2D rigidBody2D;
    private PlayerInventoryHolder playerInventory;
    public Vector3 moveDirection { get; private set; }


    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        playerInventory = GetComponent<PlayerInventoryHolder>();
    }

    private void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            playerInventory.GetNumpadItem(0);
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            playerInventory.GetNumpadItem(1);
        }
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            playerInventory.GetNumpadItem(2);
        }
        if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            playerInventory.GetNumpadItem(3);
        }
        if (Keyboard.current.digit5Key.wasPressedThisFrame)
        {
            playerInventory.GetNumpadItem(4);
        }
        if (Keyboard.current.digit6Key.wasPressedThisFrame)
        {
            playerInventory.GetNumpadItem(5);
        }
        if (Keyboard.current.digit7Key.wasPressedThisFrame)
        {
            playerInventory.GetNumpadItem(6);
        }
        if (Keyboard.current.digit8Key.wasPressedThisFrame)
        {
            playerInventory.GetNumpadItem(7);
        }

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Move();

    }

    public void OnMove(InputValue inputValue)
    {
        Vector2 direction = inputValue.Get<Vector2>();
        moveDirection = new Vector3(direction.x, direction.y, 0f);
    }

    private void Move()
    {
        rigidBody2D.velocity = moveDirection * speed;
    }
}
 