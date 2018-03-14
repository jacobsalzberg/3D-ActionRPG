using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    [SerializeField] private GameObject player;
    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] GameObject[] powerUpSpawns;
    [SerializeField] GameObject tanker;
    [SerializeField] GameObject ranger;
    [SerializeField] GameObject soldier;
    [SerializeField] Text levelText;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject healthPowerUp;
    [SerializeField] GameObject speedPowerUp;
    [SerializeField] int maxPowerUps;

    //alternative: public bool GameOver {get; private set;} 
    private bool gameOver = false;
    private int currentLevel;
    private float generatedSpawnTime = 1f;
    private float currentSpawnTime=0f;
    private float powerUpSpawnTime = 5f;
    private float currentPowerUpSpawnTime = 0f;
    private GameObject newEnemy;
    private int powerups = 0;

    private List<EnemyHealth> enemies = new List<EnemyHealth>();
    private List<EnemyHealth> killedEnemies = new List<EnemyHealth>();

    public void RegisterEnemy(EnemyHealth enemy)
    {
        enemies.Add(enemy);
    }

    public void KilledEnemy(EnemyHealth enemy)
    {
        killedEnemies.Add(enemy);
    }

    public void RegisterPowerUp()
    {
        powerups++;
    }

    public bool GameOver
    {
        get
        {
            return gameOver;
        }
    }

    public GameObject Player
    {
        get
        {
            return player;
        }
    }

    public GameObject Arrow
    {
        get
        {
            return arrow;
        }
    }

    private void Awake()
    {
        //make sure there's only one instance of gamemanager.cs
        if (instance == null)
        {
            instance = this;
        } else if (instance != this) 
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    // Use this for initialization
    void Start () {
        StartCoroutine(Spawn());
        currentLevel = 1;
	}
	
	// Update is called once per frame
	void Update () {
        currentSpawnTime += Time.deltaTime;
        currentPowerUpSpawnTime += Time.deltaTime;
	}

    public void PlayerHit(int currentHP)
    {
        if (currentHP > 0)
            gameOver = false;
        else
            gameOver = true;
    }

    IEnumerator Spawn()
    {
        //check that spawnt time is greater than the current time

        if (currentSpawnTime > generatedSpawnTime)
            currentSpawnTime = 0;
            if (enemies.Count < currentLevel) 
        {
            int randomNumber = Random.Range(0, spawnPoints.Length - 1);
            GameObject spawnLocation = spawnPoints[randomNumber];
            int randomEnemy = Random.Range(0, 3); //inclusive than EXCLUSIVE
            if (randomEnemy==0)
            {
                newEnemy = Instantiate(soldier) as GameObject;
            } else if (randomEnemy ==1)
            {
                newEnemy = Instantiate(ranger);
            } else if (randomEnemy ==2) 
                newEnemy = Instantiate(tanker);

            newEnemy.transform.position = spawnLocation.transform.position;
        }
            if (killedEnemies.Count ==currentLevel)
        {
            //we want to clear our enemies and our killed enemy arrays (resets the count)
            enemies.Clear();
            killedEnemies.Clear();

            yield return new WaitForSeconds(3f);
            currentLevel++;
            levelText.text = "Level " + currentLevel;
        }

        yield return null;
        StartCoroutine(Spawn());
    }
}
