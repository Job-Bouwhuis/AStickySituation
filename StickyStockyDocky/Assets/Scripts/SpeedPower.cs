using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPower : MonoBehaviour
{
    /// <summary>
    /// Gets invoked when the player collides with a powerup object
    /// </summary>
    public static Action OnPowerUpActivate;
    /// <summary>
    /// Gets invoked when the powerup should be dissabled
    /// </summary>
    public static Action OnPowerUpDeactivate;

    public float powerUpDurationInSeconds = 5;

    private bool shouldCount = false;
    private float time = 0;
    [SerializeField] private GameObject Graphic;

    private void Update()
    {
        if (!shouldCount)
            return;

        time += Time.deltaTime;
        if (time > powerUpDurationInSeconds)
        {
            shouldCount = false;
            time = 0;
            OnPowerUpDeactivate?.Invoke();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnPowerUpActivate?.Invoke();
            Graphic.SetActive(false);
            GetComponent<BoxCollider2D>().enabled = false;
            shouldCount = true;
        }
    }
}