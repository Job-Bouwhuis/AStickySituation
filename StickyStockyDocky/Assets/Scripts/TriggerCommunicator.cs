using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCommunicator : MonoBehaviour
{
    public bool isColliding = false;
    public Action OnColisionEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plak"))
        {
            isColliding = true;
            OnColisionEnter?.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Plak"))
        {
            isColliding = false;
        }
    }

    public static implicit operator bool(TriggerCommunicator self) => self.isColliding;
}
