using System.Collections;
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
        StartCoroutine(CountdownToStart());
        // startEnemy audio
        audio.Play();
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
    }
}
