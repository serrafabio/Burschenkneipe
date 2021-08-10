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
    private double timeOfTheVideo = 0f;
    public double pauseVideo;
    private bool videoStopped = false;
    private int maxStops = 1;
    private int countStops = 0;

    private void Start()
    {
        video.Play();
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
        if (pauseVideo > 0)
        {
            if (video.time >= pauseVideo & countStops < maxStops)
            {
                videoStopped = true;
                countStops++;
            }
            
        }
        
    }
}
