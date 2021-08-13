using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class videos : MonoBehaviour
{
    public VideoPlayer video;
    public string SceneName;
    private bool StopVideo = false;
    private double timeOfTheVideo = 0d;
    public double pauseVideo_1 = -1.0d;
    public double pauseVideo_2 = -1.0d;
    private bool videoStopped = false;
    private bool pauseVideo1Active = false;
    private bool pauseVideo2Active = false;
    public AudioSource ambientSound;

    private void Start()
    {
        video.Play();
        ambientSound.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        // Clicking in Space stop or continue the video
        if (Input.GetKeyDown(KeyCode.Space) | videoStopped)
        {
            StopVideo = !StopVideo;
            if (StopVideo)
            {
                timeOfTheVideo = video.time;
                video.Stop();
            }
            else
            {
                ambientSound.Stop();
                video.time = timeOfTheVideo;
                video.Play();
            }
            videoStopped = false;
        }

        // Clicking in A will jump the video
        if (Input.GetKeyDown(KeyCode.A) | (video.time + 0.10f >= video.length))
        {
            SceneManager.LoadScene(SceneName);
        }
        
        // Clicking in arrow can go back or go foward 10 sec
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            video.time += 15f;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            video.time -= 15f;
        }
        
        // Pause video
        if (pauseVideo_1 > 0)
        {
           
            if (video.time >= pauseVideo_1 & !pauseVideo1Active)
            {
                videoStopped = true;
                ambientSound.Play();
                pauseVideo1Active = true;
                
            } 
            else if (video.time < pauseVideo_1 & pauseVideo1Active & video.time > 0)
            {
                pauseVideo1Active = false;
            }
        }
        if (pauseVideo_2 > 0)
        {
            if (video.time >= pauseVideo_2 & !pauseVideo2Active)
            {
                videoStopped = true;
                ambientSound.Play();
                pauseVideo2Active = true;
                
            } 
            else if (video.time < pauseVideo_2 & pauseVideo2Active & video.time > 0)
            {
                pauseVideo2Active = false;
            }
        }

        

    }
    
}
