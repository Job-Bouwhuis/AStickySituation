using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SF = UnityEngine.SerializeField;
using H = UnityEngine.HeaderAttribute;
using System.Linq;

public class PlayerMovement : MonoBehaviour
{
    [H("Player Stats")]
    public float moveSpeed = 5f;

    [H("Sounds")]
    [SF] private AudioClip[] slimySounds;
    private AudioSource source;

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
        source = gameObject.AddComponent<AudioSource>();

        Top.OnColisionEnter += PlaySound;
        Bottom.OnColisionEnter += PlaySound;
        Left.OnColisionEnter += PlaySound;
        Right.OnColisionEnter += PlaySound;

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Top.gameObject.SetActive(false);
            Right.gameObject.SetActive(false);
            Left.gameObject.SetActive(false);
        }
        else
        {
            Top.gameObject.SetActive(true);
            Right.gameObject.SetActive(true);
            Left.gameObject.SetActive(true);
        }

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

    public void PlaySound()
    {
        source.clip = slimySounds.OrderBy(x => new System.Random().Next()).First();
        source.Play();
    }
}

public enum MoveDirection
{
    LeftRight,
    UpDown,
    Both,
    None
}
