using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController shared = null;

    [SerializeField] GameObject player;
    private bool gameOver = false;


    public bool GameOver
    {
        get { return gameOver; }
    }

    public GameObject Player
    {
        get { return player; }
    }


    private void Awake()
    {
        if(shared == null)
        {
            shared = this;
        }
        else if(shared != this)
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
        if(currentHP > 0)
        {
            gameOver = false;
        } else
        {
            gameOver = true;
        }
    }

    public void EnemyHit(int currentHP)
    {
        if (currentHP > 0)
        {
            gameOver = false;
        }
        else
        {
            gameOver = true;
        }

    }
}
