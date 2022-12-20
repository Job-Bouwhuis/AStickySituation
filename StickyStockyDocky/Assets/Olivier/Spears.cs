using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spears : MonoBehaviour
{

    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit");
            SceneManager.LoadScene(0);
            Destroy(collision.gameObject);
        }
    }
    /*
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hitted");
        if(gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit");
            SceneManager.LoadScene(0);
        }
    } */

}
