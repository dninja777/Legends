using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerHealth : MonoBehaviour {


    [SerializeField] int startingHealth = 100;
    [SerializeField] float timeSinceLastHit = 2f;
    
    


    private float timer = 0f;
    private CharacterController characterController;
    private Animator anim;
    private AudioSource audio;
    private int currentHealth;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        audio = GetComponent<AudioSource>();
        currentHealth = startingHealth;
        
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(timer >= timeSinceLastHit && !GameController.shared.GameOver)
        {
            if(other.tag == "weapon")
            {
                TakeHit();
                timer = 0;
            }
        }
    }

    void TakeHit()
    {
        if(currentHealth > 0)
        {
            GameController.shared.PlayerHit(currentHealth);
            anim.Play("Hurt");
            currentHealth -= 10;
            audio.PlayOneShot(audio.clip);
            if(currentHealth <= 0)
            {
                KillPlayer();
            }
        }
    }

    void KillPlayer()
    {
        GameController.shared.PlayerHit(currentHealth);
        anim.SetTrigger("HeroDie");
        audio.PlayOneShot(audio.clip);
        characterController.enabled = false;
    }
}
