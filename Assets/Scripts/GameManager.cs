using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    [SerializeField] private GameObject player;

    //alternative: public bool GameOver {get; private set;} 
    private bool gameOver = false;

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
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayerHit(int currentHP)
    {
        if (currentHP > 0)
            gameOver = false;
        else
            gameOver = true;
    }
}
