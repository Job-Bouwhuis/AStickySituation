using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformExtender : MonoBehaviour
{
    Platform platform;

    // Start is called before the first frame update
    void Start()
    {
        platform = GetComponent<Platform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.Translate(platform.translation, Space.World);
        }
    }
}
