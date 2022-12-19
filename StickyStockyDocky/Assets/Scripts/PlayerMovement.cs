using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.KeyCode;
using SF = UnityEngine.SerializeField;

public class PlayerMovement : MonoBehaviour
{
    [Header("DO NOT SET THESE VALUES. THIS IS FOR DEBUGGING!")]
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
        if(currentDirection is MoveDirection.LeftRight or MoveDirection.Both)
        {

        }

    }

    public void SetDirection(MoveDirection direction)
    {
        currentDirection = direction;
    }
}

public enum MoveDirection
{
    LeftRight,
    UpDown,
    Both
}
