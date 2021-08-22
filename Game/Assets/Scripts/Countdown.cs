using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public GameObject psys_4;
    public AudioSource audio;
    public GameObject explosion;
    public GameObject explosion_player;
    public Text enemyHurtPoints;
    public Text enemyHurtPoints_2;
    public Text playerHurtPoints;
    public GameObject ultraAttack;
    public GameObject planet;
    public GameObject LigthUltraAttack;
    public GameObject SuperLight;
    // labels
    public GameObject labelEnemy;
    public GameObject labelPlayer;
    public GameObject labelNames;
    public GameObject bonusLabel;

    private void Start()
    {
        
        Enemy.SetActive(false);
        Player.SetActive(false);
        Missil_1.SetActive(false);
        Missil_2.SetActive(false);
        Missil_3.SetActive(false);
        explosion.SetActive(false);
        explosion_player.SetActive(false);
        ultraAttack.SetActive(false);
        planet.SetActive(false);
        psys_4.SetActive(false);
        psys_1.Stop();
        psys_2.Stop();
        psys_3.Stop();
        enemyHurtPoints.enabled = false;
        playerHurtPoints.enabled = false;
        enemyHurtPoints_2.enabled = false;
        LigthUltraAttack.SetActive(false);
        SuperLight.SetActive(false);
        labelEnemy.SetActive(false);
        labelPlayer.SetActive(false);
        labelNames.SetActive(false);
        bonusLabel.SetActive(false);
        StartCoroutine(CountdownToStart());
        // startEnemy audio
        audio.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
        }
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
        planet.SetActive(true);
        labelEnemy.SetActive(true);
        labelPlayer.SetActive(true);
        labelNames.SetActive(true);
    }
}
