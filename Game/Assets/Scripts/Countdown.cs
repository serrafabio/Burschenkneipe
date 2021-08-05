using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public int countdownTime;
    public Text countDownDisplay;
    public GameObject Enemy;
    public GameObject Player;
    public GameObject Missil_1;
    public GameObject Missil_2;
    public GameObject Missil_3;
    public ParticleSystem psys_1;
    public ParticleSystem psys_2;
    public ParticleSystem psys_3;
    public AudioSource audio;

    private void Start()
    {
        Enemy.SetActive(false);
        Player.SetActive(false);
        Missil_1.SetActive(false);
        Missil_2.SetActive(false);
        Missil_3.SetActive(false);
        psys_1.Stop();
        psys_2.Stop();
        psys_3.Stop();
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while (countdownTime>0)
        {
            countDownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);

            countdownTime--; 
        }

        countDownDisplay.text = "GO!";

        yield return new WaitForSeconds(1f);
        
        countDownDisplay.gameObject.SetActive(false);
        
        Enemy.SetActive(true);
        Player.SetActive(true);
        audio.Play();
    }
}
