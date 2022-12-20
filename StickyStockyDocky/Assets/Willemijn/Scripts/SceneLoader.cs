using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject soundeffects;
    public GameObject music;
    public void LoadScene (int scene)
    {
        if (scene == 10) { Application.Quit(); }
        else { SceneManager.LoadScene (scene); }
    }
    public void SetMusicValue()
    {
        Debug.Log(music.GetComponent<Toggle>().value);
        PlayerPrefs.SetInt("PlayMusic", music.GetComponent<Toggle>().value ? 1 : 0);
    }
    public void SetSoundEffectValue()
    {
        Debug.Log(soundeffects.GetComponent<Toggle>().value);
        PlayerPrefs.SetInt("PlayEffects", soundeffects.GetComponent<Toggle>().value ? 1 : 0);
    }
}
