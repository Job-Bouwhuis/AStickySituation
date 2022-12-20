using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Toggle soundeffects;
    public Toggle music;
    /// <summary>
    /// Loads the scene at the given BuildIndex. if <paramref name="scene"/> is set to 10 the application will quit
    /// </summary>
    /// <param name="scene"></param>
    public void LoadScene (int scene)
    {
        if (scene == 10) { Application.Quit(); }
        else { SceneManager.LoadScene (scene); }
    }
    public void SetMusicValue()
    {
        Debug.Log(music.isOn);
        PlayerPrefs.SetInt("PlayMusic", music.isOn ? 1 : 0);
    }
    public void SetSoundEffectValue()
    {
        Debug.Log(soundeffects.isOn);
        PlayerPrefs.SetInt("PlayEffects", soundeffects.isOn ? 1 : 0);
    }
}