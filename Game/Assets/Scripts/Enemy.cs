using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    // Start objects in Scene
    public GameObject buletEnemy;
    public Transform startEnemy;
    public ParticleSystem partsSysEnemy;
    public AudioSource audioEnemyShoot;
    // parms of life and power of the missil
    private int Enemy_power = 5;
    private int Player_life;
    private bool stopGame = false;
    private int intactPlayerLife;
    public bool CheatActive = false;
    // cheater for last phase
    public int Cheater = 1;
    // globals
    private string path = "C:\\Users\\serra\\OneDrive\\Documentos\\WiP\\Frisia\\Burschenkneipe\\Game\\Assets\\Scripts";
    // Player life text
    public Text playerLifeDisplay;
    // List
    private List<float> RandomTime = new List<float>{1f,2f,3f,4f};
    private List<int> RandomShooting;
    // Explosion
    public Animator explosion;
    public AudioSource audio_player;
    public GameObject audioExplosionGameObject;
    // Labels
    public Text playerHurtPoints;
    // Change Scene
    public string SceneName;
    // If I activate the shooting
    private int manualShootPower = 2;
    private float manualTimeOfWait = 6.0f;
    private bool wait = false;
    private bool activatePause = false;
    private bool shootImediately = false;
    private int manualShootPowerImediately = 40;
    

    void Start()
    {   
        // right side
        call_and_read_life();
        playerLifeDisplay.text = Player_life.ToString();
        playerLifeDisplay.color = Color.green;
        StartCoroutine(shooting());
        
    }
    
    // Update is called once per frame
    void Update()
    {
        // Shooting automatic every time interval
        if (Player_life > 0 & !wait)
        {
            wait = true;
            StartCoroutine(shooting());
        }
        
        // My controller over the enemy
        // button to shoot from enemy
        if (Input.GetKeyDown(KeyCode.M))
        {
            shootImediately = true;
        }
        // button to choose 5 sec with 2 points
        if (Input.GetKeyDown(KeyCode.N))
        {
            activatePause = true;
        }
        
        // verify if enemy wins
        if (Player_life <= 0 & stopGame)
        {
            audioExplosionGameObject.SetActive(true);
            stopGame = false;
            explosion.enabled = true;
            StartCoroutine(waitForSound());
        }
    }

    private IEnumerator shooting()
    {
        if (!shootImediately)
        {
            if (!activatePause)
            {
                var random = new System.Random();
                int time_index = random.Next(RandomTime.Count);
                if (Cheater > 1)
                {
                    if (CheatActive)
                    {
                        time_index = 1;
                    }
                }
                yield return new WaitForSeconds(RandomTime[time_index]); // wait
            
                if (Cheater > 1)
                {
                    if (CheatActive)
                    {
                        time_index = random.Next(RandomTime.Count);
                    }
                }
        
                // Select the shooting power
                if (RandomTime[time_index] <= 1f)
                {
                    RandomShooting = new List<int>{3, 4, 5};
                }
                else if (RandomTime[time_index] < 4f & RandomTime[time_index] > 2f)
                {
                    RandomShooting = new List<int>{6, 7, 8};
                }
                else
                {
                    RandomShooting = new List<int>{9, 10, 11, 12, 13, 14, 15};
                }
                int i = random.Next(RandomShooting.Count);

                Enemy_power = RandomShooting[i];
            }

            else
            {
                yield return new WaitForSeconds(manualTimeOfWait);
                Enemy_power = manualShootPower;
                activatePause = false;
            }
        }
        else
        {
            yield return new WaitForSeconds(0.05f); // wait
            Enemy_power = 40;
            shootImediately = false;
        }
        
        if (Cheater > 1)
        {
            if (CheatActive) { Enemy_power *= Cheater; }
        }
        
        // Play sound
        audioEnemyShoot.Play();
        // init shoot
        GameObject shoot = Instantiate(buletEnemy, startEnemy.transform.position, startEnemy.transform.rotation);
        shoot.SetActive(true);
        Destroy(shoot, 0.80f);
        
        // Wait until the missil arrives
        yield return new WaitForSeconds(0.55f);
        
        // activate particle systems
        partsSysEnemy.Play();
        //  each shoot must hurt the player ship
        Player_life -= Enemy_power;
        
        // Actualize the life status label
        if (Player_life <= 0)
        {
            Player_life = 0;
            stopGame = true;
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
        
        // set label
        playerHurtPoints.enabled = true;
        if (Enemy_power >= 8)
        {
            playerHurtPoints.color = Color.red;
        }
        else
        {
            playerHurtPoints.color = new Color(r: 1.00f, g: 0.65f, b:0f);
        }
        
        playerHurtPoints.text = "-" + Enemy_power.ToString();
        
        wait = false;
        
        yield return new WaitForSeconds(0.6f); // wait
        
        // disable label
        playerHurtPoints.enabled = false;
        
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator waitForSound()
    {
        audioEnemyShoot.Stop();
        audio_player.Play();
        //Wait Until Sound has finished playing
        while (audio_player.isPlaying)
        {
            yield return null;
        }
        // Wait 1 sec before change 
        StartCoroutine(FinishGame());
    }
    
    private IEnumerator FinishGame()
    {
        yield return new WaitForSeconds(2f); // wait
        
        // Change scene
        SceneManager.LoadScene(SceneName);
    }


    // Read the file with player life information
    private void call_and_read_life()
    {
        int cont = 0;
        var lines = File.ReadAllLines(path + "\\globals.txt")
            .Select(x => x.Split(new[] {'[', ']'}, StringSplitOptions.RemoveEmptyEntries));
        foreach (var pair in lines)
        {
            if (cont == 0)
            {
                Player_life = Int16.Parse(pair.First());
                intactPlayerLife = Player_life;
            }
            else if (cont == 6)
            {
                manualShootPowerImediately = Int16.Parse(pair.First());
            }
            else if (cont == 7)
            {
                manualTimeOfWait = float.Parse(pair.First());
            }
            else if (cont == 8)
            {
                manualShootPower = Int16.Parse(pair.First());
            }
            cont += 1;
            
        }
    }
    
    
    
}
