using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.KeyCode;
using SF = UnityEngine.SerializeField;
using H = UnityEngine.HeaderAttribute;


public class PlayerMovement : MonoBehaviour
{
    [H("Player Stats")]
    public float moveSpeed = 5f;
    private float smoothness = 5f;

    [H("Triggers")]
    [SF] private TriggerCommunicator Top;
    [SF] private TriggerCommunicator Bottom;
    [SF] private TriggerCommunicator Left;
    [SF] private TriggerCommunicator Right;
    
    [H("DO NOT SET THESE VALUES. THIS IS FOR DEBUGGING!")]
    [SF] private MoveDirection currentDirection;
    [SF] private Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        GetDirection();
        Vector2 direction = new(0, 0);
        if (currentDirection == MoveDirection.None)
            direction.y = -1;

        if(currentDirection is MoveDirection.LeftRight or MoveDirection.Both)
        {
            direction.x = Input.GetAxis("Horizontal");
        }
        if(currentDirection is MoveDirection.UpDown or MoveDirection.Both)
        {
            direction.y = Input.GetAxis("Vertical");
        }
        //if (direction != Vector2.zero)
        //    direction.Normalize();

        Debug.Log(direction.x);

        rb.velocity = direction * moveSpeed;
    }

    public void GetDirection()
    {
        currentDirection = MoveDirection.None;

        if (Top.isColliding || Bottom.isColliding)
            currentDirection = MoveDirection.LeftRight;
        if(Left.isColliding || Right.isColliding)
            currentDirection = currentDirection == MoveDirection.LeftRight ? MoveDirection.Both : MoveDirection.UpDown;
    }
}

public enum MoveDirection
{
    LeftRight,
    UpDown,
    Both,
    None
}
