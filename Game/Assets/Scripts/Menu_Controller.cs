using System;
using Newtonsoft.Json.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Controller : MonoBehaviour
{
    private bool isMuted = false;
    
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void muteMusic()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
    }
}
