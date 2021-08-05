using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class shooter : MonoBehaviour
{
    // Start variables
    public GameObject bulet;
    public Transform start;
    public ParticleSystem partsSys;
    public AudioSource audio_1;
    // left parms
    public GameObject bulet_left;
    public Transform start_left;
    public ParticleSystem partsSys_left;
    // direction parm
    private Boolean dir;
    // Life and shoot
    private int Enemy_life;
    private int Player_power;
    // globals
    private string path = Directory.GetCurrentDirectory();
    // Enemy life text
    public Text enemyLifeDisplay;

    void Start()
    {   
        // Read the information required
        call_and_read_life();
        enemyLifeDisplay.text = Enemy_life.ToString();
        dir = true; // initialize with right
    }
    
   // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shooting();
        }
        // TODO verify if player wins
        if (Enemy_life <= 0)
        {
            Debug.Log("Congratulations Player Won!");
        }
    }

    void shooting()
    {
        // Play sound
        audio_1.Play();
        // right side
        if (dir == true)
        {
            // init shoot
            GameObject shoot = Instantiate(bulet, start.transform.position, start.transform.rotation);
            shoot.SetActive(true);
            Destroy(shoot, 0.80f);
            // activate particle systems
            partsSys.Play();
            // change side
            dir = false;
        }
        // left side
        else
        {
            // init shoot
            GameObject shoot = Instantiate(bulet_left, start_left.transform.position, start_left.transform.rotation);
            shoot.SetActive(true);
            Destroy(shoot, 0.80f);
            // activate particle systems
            partsSys_left.Play();
            // change side
            dir = true;
        }
        // Each shoot must hurt the enemy
        Enemy_life -= Player_power;
        enemyLifeDisplay.text = Enemy_life.ToString();
    }
    
    private void call_and_read_life()
    {
        int cont = 0;
        var lines = File.ReadAllLines(path + "\\Assets\\Scripts\\"+ "globals.txt")
            .Select(x => x.Split(new[] {'[', ']'}, StringSplitOptions.RemoveEmptyEntries));
        foreach (var pair in lines)
        {
            if (cont == 2)
            {
                Enemy_life = Int16.Parse(pair.First());
            }
            else if (cont == 3)
            {
                Player_power = Int16.Parse(pair.First());
            }
            cont += 1;
        }
        
    }
    
}
