using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    AudioSource source;
    public AudioClip music;
    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.loop = true;
        source.clip = music;
        source.playOnAwake = false;
         
        bool shouldPlay = PlayerPrefs.GetInt("PlayMusic") == 1;
        source.Play();
    }
}
