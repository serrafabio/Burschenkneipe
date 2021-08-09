using System;
using System.Media;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sings_Controller : MonoBehaviour
{
    private bool isMuted = false;
    public GameObject playSimbol;
    public GameObject pauseSimbol;
    public AudioSource music;
    private bool musicPlay = false;
    private float timeOfTheMusic = 0;

    private void Start()
    {
        music.Stop();
        pauseSimbol.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void playMusic()
    {
        musicPlay = !musicPlay;
        if (musicPlay)
        {
            music.time = timeOfTheMusic;
            playSimbol.SetActive(false);
            pauseSimbol.SetActive(true);
            music.Play();
        }
        else
        {
            playSimbol.SetActive(true);
            pauseSimbol.SetActive(false);
            music.Stop(); 
        }
        
    }

    private void Update()
    {
        if (musicPlay)
        {
            timeOfTheMusic = music.time;
        }

        if (music.time + 0.5f > music.clip.length & !musicPlay)
        {
            timeOfTheMusic = 0f;
            musicPlay = true;
            playMusic();
        } 
    }
}