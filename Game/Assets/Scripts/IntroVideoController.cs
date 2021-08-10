using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroVideoController : MonoBehaviour
{
    public VideoPlayer video;
    private static string path = "C:\\Users\\serra\\OneDrive\\Documentos\\WiP\\Frisia\\Burschenkneipe\\Game\\Assets\\Scripts";
    private float startTime;
    private bool justMovevideo = false;
    private bool StopVideo = false;
    private double timeOfTheVideo;
    
    // Start is called before the first frame update
    void Start()
    {
        call_and_read_time();
        video.time = startTime;
        timeOfTheVideo = startTime;

    }

    // Update is called once per frame
    void Update()
    {
        
        // Clicking in Space stop or continue the video
        if (Input.GetKeyDown(KeyCode.Space))
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
        }
        
        // Clicking in arrow can go back or go foward 10 sec
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            video.time += 15f;
            justMovevideo = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            video.time -= 15f;
            justMovevideo = true;
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            writTXT(video.time);
            SceneManager.LoadScene("menu");
        }
        
        // Restart the video
        if (Input.GetKeyDown(KeyCode.Z))
        {
            video.time = 0;
        }

        justMovevideo = false;


    }
    
    private void call_and_read_time()
    {
        var lines = File.ReadAllLines(path + "\\video_time.txt")
            .Select(x => x.Split(new[] {'[', ']'}, StringSplitOptions.RemoveEmptyEntries));
        foreach (var pair in lines)
        {
            startTime = float.Parse(pair.First());
        }
    }

    private void writTXT(double timeToWrite)
    {
        
        using (StreamWriter outputFile = new StreamWriter(path + "\\video_time.txt"))
        {
            outputFile.WriteLine(timeToWrite.ToString());
        }
    }

}
