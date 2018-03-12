using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    [SerializeField] private float range =3f;
    [SerializeField] private float timeBetweenAttacks = 1f;

    private Animator anim;
    private GameObject player;
    private bool playerInRange;
    //boxcollider from the weapon(s), need to account for that
    private BoxCollider[] weaponColliders;

    // Use this for initialization
    void Start () {

        weaponColliders = GetComponentsInChildren<BoxCollider>();
        player = GameManager.instance.Player;
        anim = GetComponent<Animator>();
        StartCoroutine(Attack());

	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
            playerInRange = true;
            //print("playerinrange");
        } else
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

    public void EnemyBeginAttack ()
    {
        foreach (var weapon in weaponColliders)
        {
            weapon.enabled = true;
            print("we");
        }
    }

    public void EnemyEndAttack()
    {
        foreach (var weapon in weaponColliders)
        {
            weapon.enabled = false;
            print("wd");
        }
    }
}