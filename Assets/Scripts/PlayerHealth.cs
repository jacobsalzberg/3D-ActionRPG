using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    [SerializeField] int startingHealth = 100;
    [SerializeField] float timeSinceLastHit = 2f;

    //time between hits
    private float timer = 0f;
    //once we die, stop being able to move
    private CharacterController characterController;
    //play death animation
    private Animator anim;
    private int currentHealth;


	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        currentHealth = startingHealth;
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
    }
}
