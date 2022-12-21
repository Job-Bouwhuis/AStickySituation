using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip clip;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
        bool shouldPlay = PlayerPrefs.GetInt("PlayMusic") == 1;
        Debug.Log(shouldPlay);
        source.playOnAwake = false;
        source.clip = clip;
        source.loop = true;

        if (shouldPlay)
            source.Play();
    }
}
