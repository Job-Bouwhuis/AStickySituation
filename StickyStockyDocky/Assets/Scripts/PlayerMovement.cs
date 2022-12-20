using UnityEngine;
using System.Linq;
using SF = UnityEngine.SerializeField;
using H = UnityEngine.HeaderAttribute;

/// <summary>
/// Player movement script. 
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// The speed at which the player moves. this is doubled for falling
    /// </summary>
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
    [SF] private ColliderType currentColliderType;
    [SF] private Rigidbody2D rb;
    [SF] private bool hasPowerup = false;
    [SF] private float baseMoveSpeed;
    // Start is called before the first frame update
    private void Awake()
    {
        baseMoveSpeed = moveSpeed;

        source = gameObject.AddComponent<AudioSource>();

        TopLeft.OnColisionEnter += PlaySound;
        TopRight.OnColisionEnter += PlaySound;
        BottomLeft.OnColisionEnter += PlaySound;
        BottomRight.OnColisionEnter += PlaySound;

        SpeedPower.OnPowerUpActivate += OnSpeedIncrease;
        SpeedPower.OnPowerUpDeactivate += OnSpeedReset;

        rb = GetComponent<Rigidbody2D>();
    }

    // hehehe managed to get this into a one liner.... less readable but im proud of myself XD
    private void FixedUpdate() =>
        rb.velocity = (GetDirection() is AllowedPlayerMovements.None || Input.GetKey(KeyCode.Space)) ? new Vector2(0, -1) * (moveSpeed * 2) :
            new Vector2(currentDirection is AllowedPlayerMovements.LeftRight or AllowedPlayerMovements.Both ? Input.GetAxis("Horizontal") : 0,
            currentDirection is AllowedPlayerMovements.UpDown or AllowedPlayerMovements.Both ? Input.GetAxis("Vertical") : 0) * moveSpeed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    private void OnSpeedIncrease()
    {
        if (hasPowerup)
            return;
        hasPowerup = true;
        moveSpeed *= 2;
    }
    private void OnSpeedDebuff()
    {
        hasPowerup = true;
        moveSpeed = baseMoveSpeed;
        moveSpeed /= 2;
    }
    private void OnSpeedReset()
    {
        if (!hasPowerup)
            return;
        hasPowerup = false;
        moveSpeed = baseMoveSpeed;
    }

    private AllowedPlayerMovements GetDirection()
    {
        currentDirection = AllowedPlayerMovements.None;
        currentColliderType = ColliderType.None;

        bool[] colliders = new bool[4] { TopLeft, BottomRight, TopRight, BottomLeft };
        if (colliders.Where(x => x).Count() == 1)
        {
            currentDirection = AllowedPlayerMovements.Both;
            return currentDirection;
        }

        if (TopLeft && TopRight || BottomLeft && BottomRight)
        {
            currentDirection = AllowedPlayerMovements.LeftRight;
            currentColliderType = BottomLeft && BottomRight ? ColliderType.HorizontalBottom : ColliderType.HorizontalTop;
        }
        else if (TopLeft && BottomLeft || TopRight && BottomRight)
        {
            currentDirection = AllowedPlayerMovements.UpDown;
            currentColliderType = TopRight && BottomRight ? ColliderType.VerticalRight : ColliderType.VerticalLeft;
        }

        if (currentColliderType == ColliderType.HorizontalBottom && (TopRight || TopLeft))
            currentDirection = AllowedPlayerMovements.Both;
        if (currentColliderType == ColliderType.HorizontalTop && (BottomRight || BottomLeft))
            currentDirection = AllowedPlayerMovements.Both;
        if (currentColliderType == ColliderType.VerticalLeft && (TopRight || BottomRight))
            currentDirection = AllowedPlayerMovements.Both;
        if (currentColliderType == ColliderType.VerticalRight && (TopLeft || BottomLeft))
            currentDirection = AllowedPlayerMovements.Both;

        return currentDirection;
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
        VerticalLeft
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
