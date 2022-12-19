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
    public float smoothness = 5f;

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

        Vector2 direction = new(0, 0);
        if(currentDirection is MoveDirection.LeftRight or MoveDirection.Both)
        {
            if (Input.GetKey(A))
                direction.x--;
            if (Input.GetKey(D)) 
                direction.x++;
        }
        if(currentDirection is MoveDirection.UpDown or MoveDirection.Both)
        {
            if (Input.GetKey(W))
                direction.y++;
            if(Input.GetKey(S))
                direction.y--;
        }
        if (direction != Vector2.zero)
            direction.Normalize();

        rb.velocity = Vector2.Lerp(rb.velocity, direction * moveSpeed, smoothness);
    }

    public void GetDirection()
    {
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
    Both
}
