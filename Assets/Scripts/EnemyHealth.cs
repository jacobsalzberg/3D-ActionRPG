using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] private int startingHealth = 20;
    [SerializeField] private float timeSineceLastHit = 0.5f;
    [SerializeField] private float dissapearSpeed = 2f;

    private AudioSource audio;
    private float timer = 0f;
    private Animator anim;
    private NavMeshAgent nav;
    public bool IsAlive { get; set; }
    private Rigidbody rigidBody;
    private CapsuleCollider capsuleCollider;
    private int currentHealth;

    public bool dissapearEnemy = false;


    //getter

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        IsAlive = true;
        currentHealth = startingHealth;
	}
        
        
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (dissapearEnemy)
        {
            transform.Translate(-Vector3.up * dissapearSpeed * Time.deltaTime);
        }

	}

    private void OnTriggerEnter(Collider other)
    {
        if(timer >= timeSineceLastHit && !GameManager.instance.GameOver)
        {
            if (other.tag == "PlayerWeapon")
            {
                TakeHit();
                timer = 0f;
            } 
        }
    }

    void TakeHit()
    {
        if (currentHealth >0)
        {
            audio.PlayOneShot(audio.clip);
            anim.Play("Hurt");
            currentHealth -= 10;
        }

        if (currentHealth <=0)
        {
            IsAlive = false;
            KillEnemy();
        }
    }

    void KillEnemy ()
    {
        capsuleCollider.enabled = false;
        nav.enabled = false;
        anim.SetTrigger("EnemyDie");
        rigidBody.isKinematic = true;

        StartCoroutine(RemoveEnemy());
    }

    IEnumerator RemoveEnemy()
    {
        //wait for seconds after enemy dies
        yield return new WaitForSeconds(4f);
        //start to sink the enemy
        dissapearEnemy = true;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
