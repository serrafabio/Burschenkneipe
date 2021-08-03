using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Missil_Player : MonoBehaviour
{

    // Here we start the parms
    public Rigidbody2D myBody;
    public ParticleSystem explosion;
    Vector2 currentPosition;
    Vector2 previousPosition;
    bool activated;
    private Vector2 velocity;
    
    
    // Start is clled before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        explosion = GetComponentInChildren<ParticleSystem>();
        activated = false;
        currentPosition = myBody.position;
        previousPosition = currentPosition;
        velocity = new Vector2(0.0f, 20.0f);
    }
    
    // The signal must be captured by arduino
    void OnMouseUp()
    {
        activated = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated == true)
        {
            myBody.MovePosition(myBody.position + velocity*Time.fixedDeltaTime);   
            currentPosition = myBody.position;
            if (Math.Abs(previousPosition.x - currentPosition.x) > 0.005)
            {
                explosion.Play();
                Destroy(myBody);
                myBody.Sleep();
                activated = false;
            }

            previousPosition = currentPosition;
        }
        
    }
}
