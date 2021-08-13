using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Countdown_probe : MonoBehaviour
{
    public int countdownTime;
    public Text countDownDisplay;
    public GameObject Player;
    public GameObject Missil_1;
    public GameObject Missil_2;
    public AudioSource audio;
    public GameObject ultraAttack;
    public GameObject LigthUltraAttack;
    public GameObject SuperLight;
    public GameObject tutorial;
    public GameObject labelEnemy;
    public GameObject labelPlayer;
    public GameObject labelNames;
    private bool blink;
    private bool wait=true; 

    private void Start()
    {
        
        Player.SetActive(false);
        Missil_1.SetActive(false);
        Missil_2.SetActive(false);
        ultraAttack.SetActive(false);
        LigthUltraAttack.SetActive(false);
        SuperLight.SetActive(false);
        tutorial.SetActive(false);
        labelEnemy.SetActive(false);
        labelPlayer.SetActive(false);
        labelNames.SetActive(false);
        StartCoroutine(CountdownToStart());
        // startEnemy audio
        audio.Play();
    }

    private void Update()
    {
        if (!wait)
        {
            wait = true;
            StartCoroutine(Blink());
        }
    }

    private IEnumerator Blink()
    {
        blink = !blink;
        if (blink)
        {
            yield return new WaitForSeconds(1.5f);
            tutorial.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            tutorial.SetActive(true);
        }

        wait = false;
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
        
        Player.SetActive(true);
        tutorial.SetActive(true);
        labelEnemy.SetActive(true);
        labelPlayer.SetActive(true);
        labelNames.SetActive(true);
        wait = false;
    }
}
