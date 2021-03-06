﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class PlayerHealth : MonoBehaviour {

    [SerializeField] int startingHealth = 100;
    [SerializeField] float timeSinceLastHit = 2f;
    [SerializeField] Slider healthSlider;

    //time between hits
    private float timer = 0f;
    //once we die, stop being able to move
    private CharacterController characterController;
    //play death animation
    private Animator anim;
    private int currentHealth;
    private AudioSource audio;
    private ParticleSystem blood;

    public int CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            if (value < 0)
                currentHealth = 0;
            else
                currentHealth = value;
        }
    }
    
    private void Awake()
    {
        Assert.IsNotNull(healthSlider);
    }


    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        currentHealth = startingHealth;
        audio = GetComponent<AudioSource>();
        blood = GetComponentInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;	
	}

    private void OnTriggerEnter(Collider other)
    {
        if (timer>= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            if (other.tag =="Weapon") {
                TakeHit();
                //again -> timer = time since last hit
                timer = 0; 
            }
        }
    }

    private void TakeHit()
    {
        if(currentHealth>0)
        {
            GameManager.instance.PlayerHit(currentHealth);
            anim.Play("Hurt");
            currentHealth -= 10;
            healthSlider.value = currentHealth;
            audio.PlayOneShot(audio.clip);
            blood.Play();
        }

        if (currentHealth <=0)
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        GameManager.instance.PlayerHit(currentHealth);
        anim.SetTrigger("HeroDie");
        characterController.enabled = false;
        audio.PlayOneShot(audio.clip);
        blood.Play();
    }

    public void PowerUpHealth()
    {
        if (currentHealth <=70)
        {
            currentHealth += 30;
        } else if (currentHealth <startingHealth)
        {
            CurrentHealth = startingHealth;
        }
        healthSlider.value = currentHealth;
    }
}
