using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    private int Enemy_power = 5;
    private int Player_life;
    private Boolean wait = true;
    private int intactPlayerLife;
    // globals
    private string path = Directory.GetCurrentDirectory() ;
    // Player life text
    public Text playerLifeDisplay;
    // List
    private List<float> RandomTime = new List<float>{1f,2f,3f,4f};
    private List<int> RandomShooting;

    void Start()
    {   
        // right side
        call_and_read_life();
        playerLifeDisplay.text = Player_life.ToString();
        playerLifeDisplay.color = Color.green;
        StartCoroutine(MyEvent());
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
        // Shooting automatic every time interval
        if (wait == true)
        {
            shooting();
            wait = false;
            StartCoroutine(MyEvent());

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
        
        // Actualize the life status label
        if (Player_life <= 0)
        {
            Player_life = 0;
        }
        playerLifeDisplay.text = Player_life.ToString();
        // Sinalize by coulor
        if (Player_life >= (int)Math.Round((0.66f) * intactPlayerLife))
        {
            playerLifeDisplay.color = Color.green;
        }
        else if (Player_life >= (int)Math.Round((0.33f)*intactPlayerLife) & Player_life < (int)Math.Round((0.66f)*intactPlayerLife)) 
        {
            playerLifeDisplay.color = Color.yellow;
        }
        else
        {
            playerLifeDisplay.color = Color.red;
        }
    }

    private IEnumerator MyEvent()
    {
        var random = new System.Random();
        int time_index = random.Next(RandomTime.Count);
        yield return new WaitForSeconds(RandomTime[time_index]); // wait
        //Launch Train
        wait = true;
        // Select the shooting power
        if (RandomTime[time_index] <= 1f)
        {
            RandomShooting = new List<int>{2, 3, 4};
        }
        else if (RandomTime[time_index] < 4f & RandomTime[time_index] > 2f)
        {
            RandomShooting = new List<int>{5, 6, 7};
        }
        else
        {
            RandomShooting = new List<int>{8, 9, 10};
        }
        int i = random.Next(RandomShooting.Count);
        Enemy_power = RandomShooting[i];
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
                intactPlayerLife = Player_life;
            }
            cont += 1;
            
        }
    }
    
    // TODO Write in TXT File if the enemy is destroyed
    
    
}
