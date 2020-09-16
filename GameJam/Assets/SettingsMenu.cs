using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string lastLoadedScene;

    public void setVolume(float volume){
        audioMixer.SetFloat("volumeMusic", volume);
    }

    public void goBack(){
        string sceneName = PlayerPrefs.GetString("lastLoadedScene");
          SceneManager.LoadScene(sceneName);
    }
}
