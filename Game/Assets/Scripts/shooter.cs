using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;

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
    private Boolean stopGame = false;
    // Life and shoot
    private int Enemy_life;
    private int Player_power;
    private int intactEnemyLife;
    // globals
    private string path = Directory.GetCurrentDirectory();
    // Enemy life text
    public Text enemyLifeDisplay;
    // Explosion
    public Animator explosion;
    public GameObject enemy;
    public AudioSource audio_explosion;
    public GameObject audioExplosionGameObject;
    
    void Start()
    {   
        // Read the information required
        call_and_read_life();
        enemyLifeDisplay.text = Enemy_life.ToString();
        explosion.enabled = false;
        enemyLifeDisplay.color = Color.green;
        dir = true; // initialize with right
    }
    
   // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) & Enemy_life > 0)
        {
            shooting();
        }
        // TODO verify if player wins
        if (Enemy_life <= 0 & stopGame)
        {
            audioExplosionGameObject.SetActive(true);
            stopGame = false;
            explosion.enabled = true;
            enemy.SetActive(false);
            Debug.Log("Congratulations Player Won!");
            StartCoroutine(waitForSound());
            
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
        if (Enemy_life <= 0)
        {
            Enemy_life = 0;
            stopGame = true;
        }
        
        // Actualize Life
        enemyLifeDisplay.text = Enemy_life.ToString();
        
        // Change colour to sinalize life status
        if (Enemy_life >= (int)Math.Round((0.66f) * intactEnemyLife))
        {
            enemyLifeDisplay.color = Color.green;
        }
        else if (Enemy_life >= (int)Math.Round((0.33f) * intactEnemyLife) & Enemy_life < (int)Math.Round((0.66f) * intactEnemyLife))
        {
            enemyLifeDisplay.color = Color.yellow;
        }
        else
        {
            enemyLifeDisplay.color = Color.red;
        }
    }

    private IEnumerator waitForSound()
    {
        audio_1.Stop();
        audio_explosion.Play();
        //Wait Until Sound has finished playing
        while (audio_explosion.isPlaying)
        {
            yield return null;
        }
        // Wait 1 sec before change 
        StartCoroutine(MyEvent());
    }
    
    private IEnumerator MyEvent()
    {
        yield return new WaitForSeconds(2f); // wait
        // Change scene
        
        SceneManager.LoadScene("Won");
    }
    
    private void call_and_read_life()
    {
        int cont = 0;
        var lines = File.ReadAllLines(path + "\\Assets\\Scripts\\"+ "globals.txt")
            .Select(x => x.Split(new[] {'[', ']'}, StringSplitOptions.RemoveEmptyEntries));
        foreach (var pair in lines)
        {
            if (cont == 1)
            {
                Enemy_life = Int16.Parse(pair.First());
                intactEnemyLife = Enemy_life;
            }
            else if (cont == 2)
            {
                Player_power = Int16.Parse(pair.First());
            }
            cont += 1;
        }
        
    }
    
}
