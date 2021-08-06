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
    public AudioSource audio;
    public GameObject explosion;
    public GameObject explosion_player;

    private void Start()
    {
        
        Enemy.SetActive(false);
        Player.SetActive(false);
        Missil_1.SetActive(false);
        Missil_2.SetActive(false);
        Missil_3.SetActive(false);
        explosion.SetActive(false);
        explosion_player.SetActive(false);
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
