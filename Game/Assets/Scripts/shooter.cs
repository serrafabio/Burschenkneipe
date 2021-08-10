using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.IO.Ports;
using System.Security.Cryptography.X509Certificates;

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
    public AudioSource audio_2;
    // Ultra Attack
    public GameObject ultraAttack;
    public Transform startUltraAttack;
    public GameObject explosionUltraAttack;
    public Transform explosionUltraAttackStart;
    private int UltraAttackPower;
    public GameObject LightUltraAttack;
    public Transform _startLightUltraAttack;
    // direction parm
    private bool dir;
    private bool dir_2;
    private bool stopGame = false;
    // Life and shoot
    public int Enemy_life; //{ get; private set; }
    private int Player_power;
    private int intactEnemyLife;
    // globals
    private string path = "C:\\Users\\serra\\OneDrive\\Documentos\\WiP\\Frisia\\Burschenkneipe\\Game\\Assets\\Scripts";
    // Enemy life text
    public Text enemyLifeDisplay;
    // Explosion
    public Animator explosion;
    public GameObject enemy;
    public AudioSource audio_explosion;
    public GameObject audioExplosionGameObject;
    // Label
    public Text enemyHurtPoints_1;
    public Text enemyHurtPoints_2;
    // Arduino 
    private bool isConnected = false;
    private bool ShootViaArduino = false;
    private string portName = "COM3";
    private int baudRate = 9600;
    private int reconnectionDelay = 1;
    private string SERIAL_DEVICE_CONNECTED = "__Connected__";
    private const string SERIAL_DEVICE_DISCONNECTED = "__Disconnected__";
    private SerialPort _serialPort;
    private int countArduinoDisconnect = 0;
    // Change scene
    public string SceneName;
    private bool ultraAttackActive = false;
    
    void Start()
    {   
        // Read the information required
        call_and_read_life();
        enemyLifeDisplay.text = Enemy_life.ToString();
        explosion.enabled = false;
        enemyLifeDisplay.color = Color.green;
        dir = true; // initialize with right
        dir_2 = true; // initialize with right
        
        // Initialize the data from Serial Port
        ConnectToArduino();
    }
    
   // Update is called once per frame
    void Update()
    {
        // Verifying connection via Arduion
        if (!isConnected)
        {
            countArduinoDisconnect++;
            if (countArduinoDisconnect >= 50)
            {
                countArduinoDisconnect = 0;
                ConnectToArduino();
            }
        }
        // receive info from Arduino
        else
        {
            try
            {
                receiveDataSerialPort();
            }
            catch (Exception ex)
            {
            
                Debug.Log("Nothing capture");
                throw ex;
            }  
        }
        
        
        // shooting
        if ((Input.GetKeyDown(KeyCode.Space) | ShootViaArduino) & Enemy_life > 0 )
        {
            // Not Ultra Attack
            ultraAttackActive = false;
            ShootViaArduino = false;
            StartCoroutine(shooting());
        }
        else if (Input.GetKeyDown(KeyCode.A) & Enemy_life > 0)
        {
            // Ultra Attack
            ultraAttackActive = true;
            StartCoroutine(shootingUltraAttack());
        }
        // verify if player wins
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

    private IEnumerator shooting()
    {
        // Play sound
        audio_1.Play();
        // right side (true)
        if (dir == true)
        {
            // init shoot
            GameObject shoot = Instantiate(bulet, start.transform.position, start.transform.rotation);
            shoot.SetActive(true);
            Destroy(shoot, 0.80f);
            yield return new WaitForSeconds(0.6f);
            // activate particle systems
            partsSys.Play();
            // change side
            dir = false;
            dir_2 = true;
        }
        // left side (false)
        else
        {
            // init shoot
            GameObject shoot = Instantiate(bulet_left, start_left.transform.position, start_left.transform.rotation);
            shoot.SetActive(true);
            Destroy(shoot, 0.80f);
            yield return new WaitForSeconds(0.6f);
            // activate particle systems
            partsSys_left.Play();
            // change side
            dir = true;
            dir_2 = false;
        }

        // Each shoot must hurt the enemy
        Enemy_life -= Player_power;

        // call continuation
        HurtEnemyPoints();
    }
    
    private IEnumerator shootingUltraAttack()
    {
        // Play sound
        audio_2.Play();
        // init shoot
        GameObject shoot = Instantiate(ultraAttack, startUltraAttack.position, startUltraAttack.rotation);
        shoot.SetActive(true);
        Animator shootAnimator = shoot.GetComponent<Animator>();
        shootAnimator.enabled = true;
        GameObject light = Instantiate(LightUltraAttack, _startLightUltraAttack.position,
            _startLightUltraAttack.rotation);
        light.SetActive(true);
        Destroy(shoot, 0.8f);
        Destroy(light, 0.40f);
        yield return new WaitForSeconds(0.4f);
        // activate particle systems
        GameObject explosion = Instantiate(explosionUltraAttack, explosionUltraAttackStart.position, explosionUltraAttackStart.rotation);
        explosion.SetActive(true);
        Animator explosionAnimator = explosion.GetComponent<Animator>();
        explosionAnimator.enabled = true;
        Destroy(explosion, 0.5f);

        // Each shoot must hurt the enemy
        Enemy_life -= UltraAttackPower ;
        dir_2 = true;
        
        // call continuation
        HurtEnemyPoints();
        
    }

    private void HurtEnemyPoints()
    {
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
            enemy.GetComponent<Enemy>().CheatActive = true;
        }
        
        // set label
        StartCoroutine(setLabel());
    }
    
    private IEnumerator setLabel()
    {
        if (dir_2)
        {
            enemyHurtPoints_1.enabled = true;
            if (ultraAttackActive)
            {
                enemyHurtPoints_1.color = Color.red;
                enemyHurtPoints_1.fontSize = 45;
                enemyHurtPoints_1.text = "-" + UltraAttackPower.ToString();
            }
            else
            {
                enemyHurtPoints_1.color = new Color(r: 1.00f, g: 0.65f, b:0f);
                enemyHurtPoints_1.fontSize = 30;
                enemyHurtPoints_1.text = "-" + Player_power.ToString();
            }
        }
        else
        {
            enemyHurtPoints_2.enabled = true;
            enemyHurtPoints_2.color = new Color(r: 1.00f, g: 0.65f, b:0f);
            enemyHurtPoints_2.fontSize = 30;
            enemyHurtPoints_2.text = "-" + Player_power.ToString();
            
        }
        yield return new WaitForSeconds(0.9f); // wait
        enemyHurtPoints_1.enabled = false;
        enemyHurtPoints_2.enabled = false;
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
        SceneManager.LoadScene(SceneName);
    }
    
    private void call_and_read_life()
    {
        int cont = 0;
        var lines = File.ReadAllLines(path + "\\globals.txt")
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
            else if (cont == 4)
            {
                UltraAttackPower = Int16.Parse(pair.First());
            }
            else if (cont == 5)
            {
                portName = pair.First().ToString();
            }
            cont += 1;
        }
        
    }
    
    // Connect to arduino
    private void ConnectToArduino()
    {
        try
        {
            _serialPort = new SerialPort();
            _serialPort.PortName = portName;
            _serialPort.BaudRate = baudRate;
            _serialPort.Open();
            _serialPort.ReadTimeout = reconnectionDelay;
            isConnected = true;
            Debug.Log(SERIAL_DEVICE_CONNECTED);
        }
        catch (Exception ex)
        {
           isConnected = false;
            Debug.Log(SERIAL_DEVICE_DISCONNECTED);
            throw ex;
        }
    }

    private void receiveDataSerialPort()
    {
        if (isConnected)
        {
            string received = _serialPort.ReadExisting();
            if (received.Contains("s"))
            {
                ShootViaArduino = true;
            }
        }
    }

}
