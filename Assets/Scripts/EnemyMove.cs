﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class EnemyMove : MonoBehaviour {

    [SerializeField] Transform player;
    private NavMeshAgent nav;
    private Animator anim;
    private EnemyHealth enemyHealth;

    private void Awake()
    {
        Assert.IsNotNull(player);
        
    }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
	}
	
	// Update is called once per frame
	void Update () {
        
        if (!GameController.shared.GameOver && enemyHealth.IsAlive)
        {
            nav.SetDestination(player.position);
        }
        else if(!GameController.shared.GameOver || GameController.shared.GameOver && !enemyHealth.IsAlive)
        {
            nav.enabled = false;
            
        }
        else
        {
            nav.enabled = false;
            anim.Play("Idle");
        }
	}
}
