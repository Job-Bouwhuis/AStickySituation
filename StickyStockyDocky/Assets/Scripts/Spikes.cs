using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private bool shouldCount;
    private float time = 0;
    public float debuffTimeInSeconds = 5;
    public bool onSpikes;

    /// <summary>
    /// Gets invoked when the player enters collision with spikes
    /// </summary>
    public static Action OnPlayerDebuffActivate = delegate { };
    /// <summary>
    /// Gets invoked after <see cref="debuffTimeInSeconds"/> has elapsed after the player has left the collision with spikes
    /// </summary>
    public static Action OnPlayerDebuffDeactivate = delegate { };
    
    // Update is called once per frame
    void Update()
    {
        if (!shouldCount)
            return;

        time += Time.deltaTime;
        if(time > debuffTimeInSeconds)
        {
            shouldCount = false;
            time = 0;
            OnPlayerDebuffDeactivate();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            onSpikes = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            OnPlayerDebuffActivate();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shouldCount = true;
            onSpikes = false;
        }
            
    }
}
