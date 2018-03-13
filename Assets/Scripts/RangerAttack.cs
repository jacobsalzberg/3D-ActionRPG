using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerAttack : MonoBehaviour
{

    [SerializeField] private float range = 3f;
    [SerializeField] private float timeBetweenAttacks = 1f;

    private Animator anim;
    private GameObject player;
    private bool playerInRange;
    //boxcollider from the weapon(s), need to account for that
    private EnemyHealth enemyHealth;

    // Use this for initialization
    void Start()
    {

        enemyHealth = GetComponent<EnemyHealth>();
        player = GameManager.instance.Player;
        anim = GetComponent<Animator>();
        StartCoroutine(Attack());

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range && enemyHealth.IsAlive)
        {
            playerInRange = true;
            //print("playerinrange");
        }
        else
        {
            playerInRange = false;
        }
    }

    IEnumerator Attack()
    {
        if (playerInRange && !GameManager.instance.GameOver)
        {
            anim.Play("Attack");
            yield return new WaitForSeconds(timeBetweenAttacks);

        }

        yield return null;
        StartCoroutine(Attack());
    }

  
}