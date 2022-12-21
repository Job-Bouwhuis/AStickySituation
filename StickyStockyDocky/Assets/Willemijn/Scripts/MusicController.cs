using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource Source;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.GetInt("PlayMusic");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
