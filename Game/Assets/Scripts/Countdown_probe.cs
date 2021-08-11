using System.Collections;
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

    private void Start()
    {
        
        Player.SetActive(false);
        Missil_1.SetActive(false);
        Missil_2.SetActive(false);
        ultraAttack.SetActive(false);
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
        
        Player.SetActive(true);
    }
}
