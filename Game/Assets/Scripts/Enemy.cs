using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Start objects in Scene
    public GameObject bulet;
    public Transform start;
    public ParticleSystem partsSys;
    public AudioSource audio_1;
    // parms of life and power of the missil
    private int Enemy_power;
    private int Player_life;
    private Boolean wait = true;
    // globals
    private string path = Directory.GetCurrentDirectory() ;
    // Player life text
    public Text playerLifeDisplay;

    void Start()
    {   
        // right side
        call_and_read_life();
        playerLifeDisplay.text = Player_life.ToString();
        StartCoroutine(MyEvent());
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
        // Shooting automatic every time interval
        if (wait == true)
        {
            wait = false;
            StartCoroutine(MyEvent());
            shooting();
            
        }
        
        
        // TODO verify if enemy wins
        if (Player_life <= 0)
        {
            Debug.Log("Fuck! The enemy wons!");
        }
    }

    void shooting()
    {
        // Play sound
        audio_1.Play();
        // init shoot
        GameObject shoot = Instantiate(bulet, start.transform.position, start.transform.rotation);
        shoot.SetActive(true);
        Destroy(shoot, 0.80f);
        // activate particle systems
        partsSys.Play();
        //  each shoot must hurt the player ship
        Player_life -= Enemy_power;
        playerLifeDisplay.text = Player_life.ToString();
    }

    private IEnumerator MyEvent()
    {

        yield return new WaitForSeconds(5f); // wait
        //Launch Train
        wait = true;
    }


    // TODO Read the TXT File
    private void call_and_read_life()
    {
        int cont = 0;
        var lines = File.ReadAllLines(path + "\\Assets\\Scripts\\"+ "globals.txt")
            .Select(x => x.Split(new[] {'[', ']'}, StringSplitOptions.RemoveEmptyEntries));
        foreach (var pair in lines)
        {
            if (cont == 0)
            {
                Player_life = Int16.Parse(pair.First());
            }
            else if (cont == 1)
            {
                Enemy_power = Int16.Parse(pair.First());
            }
            cont += 1;
            
        }
    }
    
    // TODO Write in TXT File if the enemy is destroyed
    
    
}
