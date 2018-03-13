using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour {

    private Transform player;
    private NavMeshAgent nav;
    private Animator anim;
    private EnemyHealth enemyHealth;

    private void Awake()
    {
       // Assert.IsNotNull(player);
    }

    // Use this for initialization
    void Start () {
        player = GameManager.instance.Player.transform;
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!GameManager.instance.GameOver && enemyHealth.IsAlive)
        {
            nav.SetDestination(player.position);
        }            
        else if ((!GameManager.instance.GameOver || GameManager.instance.GameOver) && !enemyHealth.IsAlive)
        {
            nav.enabled = false;
        } else //gameover and enemy is alive
        {
            nav.enabled = false;
            anim.Play("Idle");
        }
            
	}
}
