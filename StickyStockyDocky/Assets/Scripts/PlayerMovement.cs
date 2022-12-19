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
    [SF] private TriggerCommunicator TopLeft;
    [SF] private TriggerCommunicator TopRight;
    [SF] private TriggerCommunicator BottomLeft;
    [SF] private TriggerCommunicator BottomRight;

    [H("DO NOT SET THESE VALUES. THIS IS FOR DEBUGGING!")]
    [SF] private AllowedPlayerMovements currentDirection;
    [SF] private Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();

        TopLeft.OnColisionEnter += PlaySound;
        TopRight.OnColisionEnter += PlaySound;
        BottomLeft.OnColisionEnter += PlaySound;
        BottomRight.OnColisionEnter += PlaySound;

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            currentDirection = AllowedPlayerMovements.None;
        }
        else
            GetDirection();

        Vector2 direction = new(0, 0);
        if (currentDirection == AllowedPlayerMovements.None)
            direction.y = -1;

        if (currentDirection is AllowedPlayerMovements.LeftRight or AllowedPlayerMovements.Both)
        {
            direction.x = Input.GetAxis("Horizontal");
        }
        if (currentDirection is AllowedPlayerMovements.UpDown or AllowedPlayerMovements.Both)
        {
            direction.y = Input.GetAxis("Vertical");
        }
        //if (direction != Vector2.zero)
        //    direction.Normalize(); 

        Debug.Log(direction.x);

        rb.velocity = direction * moveSpeed;
    }

    private void GetDirection()
    {
        currentDirection = AllowedPlayerMovements.None;
        ColliderType mode = ColliderType.None;
        if(TopLeft && TopRight || BottomLeft && BottomRight)
        {
            currentDirection = AllowedPlayerMovements.LeftRight;
            mode = BottomLeft && BottomRight ? ColliderType.HorizontalBottom : ColliderType.HorizontalTop;
        }
        else if (TopLeft && BottomLeft || TopRight && BottomRight)
        {
            currentDirection = AllowedPlayerMovements.UpDown;
            mode = TopRight && BottomRight ? ColliderType.VerticalRight : ColliderType.VerticalLeft;
        }

        if (mode == ColliderType.HorizontalBottom && TopRight || TopLeft)
            currentDirection = AllowedPlayerMovements.Both;
        if(mode == ColliderType.HorizontalTop && BottomRight || BottomLeft)
            currentDirection = AllowedPlayerMovements.Both;
        if(mode == ColliderType.VerticalLeft && TopRight || BottomRight)
            currentDirection = AllowedPlayerMovements.Both;
        if(mode == ColliderType.VerticalRight && TopLeft || BottomLeft)
            currentDirection = AllowedPlayerMovements.Both;
    }

    /// <summary>
    /// Defines the type of collision that has taken place. used for movement logic
    /// </summary>
    private enum ColliderType
    {
        /// <summary>
        /// None
        /// </summary>
        None,
        /// <summary>
        /// The two colliders register a collision
        /// </summary>
        HorizontalTop,
        /// <summary>
        /// The bottom two colliders register a collision
        /// </summary>
        HorizontalBottom,
        /// <summary>
        /// The right two colliders register a collision
        /// </summary>
        VerticalRight,
        /// <summary>
        /// the left two colliders register a collision
        /// </summary>
        VerticalLeft,
        /// <summary>
        /// Allows movement in all 4 directions. atleast 4 colliders should register a collision for this to be the situation
        /// </summary>
        AllowAllMovement
    }
    private void PlaySound()
    {
        source.clip = slimySounds.OrderBy(x => new System.Random().Next()).First();
        source.Play();
    }
}

/// <summary>
/// The types of movement allowed for the player
/// </summary>
public enum AllowedPlayerMovements
{
    /// <summary>
    /// Undefined.
    /// </summary>
    None,
    /// <summary>
    /// Player may move only left or right
    /// </summary>
    LeftRight,
    /// <summary>
    /// Player may only move up or down
    /// </summary>
    UpDown,
    /// <summary>
    /// Player may move in all 4 directions
    /// </summary>
    Both
}
