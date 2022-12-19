using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCommunicator : MonoBehaviour
{
    public bool isColliding = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plak"))
        {
            isColliding = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Plak"))
        {
            isColliding = false;
        }
    }
}
