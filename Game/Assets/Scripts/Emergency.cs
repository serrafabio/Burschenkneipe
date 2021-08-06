using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Emergency : MonoBehaviour
{
    // public variables
    public int countdownTime;
    public AudioSource alarm;
    
    private void Start()
    {
        alarm.loop = true;
        alarm.Play();
        //StartCoroutine(CountdownToStart());
    }
    
    /*
     If the timer is wished
    private IEnumerator CountdownToStart()
    {
        alarm.Play();
        while (countdownTime>0)
        {
            yield return new WaitForSeconds(1f);
            countdownTime--; 
        }

        yield return new WaitForSeconds(1f);
        alarm.Stop();
    }
    */

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            alarm.Stop();   
            SceneManager.LoadScene("lose");
        }
    }
}
