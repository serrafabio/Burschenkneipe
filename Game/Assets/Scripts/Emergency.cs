using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Emergency : MonoBehaviour
{
    // public variables
    private float countdownTime;
    private float fixCountDownTime;
    public AudioSource alarm;
    public Animator alert;
    public Text counter;
    private string path = "C:\\Users\\serra\\OneDrive\\Documentos\\WiP\\Frisia\\Burschenkneipe\\Game\\Assets\\Scripts";
    public string SceneName;

    void Start()
    {
        alarm.loop = true;
        call_and_read_timer();
        alarm.Play();
        alert.speed = 0.92f;
    }

    void Update()
    {
        // set timer
        if (countdownTime > 0)
        {
            countdownTime -= Time.deltaTime;
        }
        else
        {
            countdownTime = fixCountDownTime;
        }
        DisplayTime(countdownTime);
        // exit
        if (Input.GetKeyDown(KeyCode.Space))
        {
            alarm.Stop();   
            SceneManager.LoadScene(SceneName);
        }
    }
    
    private void call_and_read_timer()
    {
        int cont = 0;
        var lines = File.ReadAllLines(path +"\\globals.txt")
            .Select(x => x.Split(new[] {'[', ']'}, StringSplitOptions.RemoveEmptyEntries));
        foreach (var pair in lines)
        {
            if (cont == 3)
            {
                countdownTime = float.Parse(pair.First());
                fixCountDownTime = countdownTime;
            }
            cont += 1;
            
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        counter.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }
    
}
