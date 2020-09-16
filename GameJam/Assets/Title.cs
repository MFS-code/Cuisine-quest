using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string lastLoadedScene;

    private void Update() {
        PlayerPrefs.SetString ("lastLoadedScene", SceneManager.GetActiveScene ().name);
        
    }
    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RollCredits(){
        SceneManager.LoadScene("Credits");
    }
    public void Options(){
        SceneManager.LoadScene("Options Menu");
    }

    public void QuitGame(){
        Debug.Log("Game succesfully quited lol");
        Application.Quit();

    }
}
