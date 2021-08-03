using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter : MonoBehaviour
{
    // Start variables
    public GameObject bulet;
    public Transform start;
    public ParticleSystem partsSys;
    // left parms
    public GameObject bulet_left;
    public Transform start_left;
    public ParticleSystem partsSys_left;
    // direction parm
    private Boolean dir;

    void Start()
    {   
        // right side
        bulet.SetActive(false); 
        partsSys.Stop();
        // left sidegit 
        bulet_left.SetActive(false); 
        partsSys_left.Stop();
        
        dir = true; // initialize with right
    }
    
   // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shooting();
        }
    }

    void shooting()
    {
        // right side
        if (dir == true)
        {
            // activate bullet
            bulet.SetActive(true); 
            // init shoot
            GameObject shoot = Instantiate(bulet, start.transform.position, start.transform.rotation);
            Destroy(shoot, 0.80f);
            // activate particle systems
            partsSys.Play();
            // change side
            dir = false;
        }
        // left side
        else
        {
            // activate bullet
            bulet_left.SetActive(true);  
            // init shoot
            GameObject shoot = Instantiate(bulet_left, start_left.transform.position, start_left.transform.rotation);
            Destroy(shoot, 0.80f);
            // activate particle systems
            partsSys_left.Play();
            // change side
            dir = true;
        }
        
    }
}
