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
    
    // Start is called before the first frame update
    void Start()
    {
        call_and_read_time();
        video.time = startTime;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            writTXT();
            SceneManager.LoadScene("menu");
        }
        
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

    private void writTXT()
    {
        
        using (StreamWriter outputFile = new StreamWriter(path + "\\video_time.txt"))
        {
            outputFile.WriteLine(video.time.ToString());
        }
    }

}
