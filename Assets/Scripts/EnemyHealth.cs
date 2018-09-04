using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyHealth : MonoBehaviour {

    [SerializeField] private int startingHealth = 20;
    [SerializeField] private float timeSinceLastHit = 1f;
    [SerializeField] float disapearSpeed = 2f;

    private float timer;
    private CharacterController characterController;
    private Animator anim;
    private NavMeshAgent nav;
    private new AudioSource audio;
    private int currentHealth;
    private bool isAlive;
    private new Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;
    private bool disapearEnemy = false;

    public bool IsAlive
    {
        get { return isAlive; }
    }

   

    // Use this for initialization
    void Start () {
        capsuleCollider = GetComponent<CapsuleCollider>();
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        nav = GetComponent<NavMeshAgent>();
        isAlive = true;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(disapearEnemy)
        {
            transform.Translate(-Vector3.up * disapearSpeed * Time.deltaTime);
        }
	}

     void OnTriggerEnter(Collider other)
    {
        if(timer >= timeSinceLastHit && !GameController.shared.GameOver)
        {
            if(other.tag == "PlayerWeapon")
            {
                TakeHit();
                timer = 0f;
            }
        }
    }


     void TakeHit()
    {
        if(currentHealth > 0)
        {
            GameController.shared.EnemyHit(currentHealth);
            audio.PlayOneShot(audio.clip);
            anim.Play("Hurt");
            currentHealth -= 10;
        }

        if(currentHealth <= 0)
        {
            isAlive = false;
            KillEnemy();
        }
    }


     void KillEnemy()
    {
        capsuleCollider.enabled = false;
        nav.enabled = false;
        anim.SetTrigger("EnemyDie");
        rigidbody.isKinematic = true;

        StartCoroutine(RemoveEnemy());
    }


    IEnumerator RemoveEnemy()
    {
        yield return new WaitForSeconds(4f);
        disapearEnemy = true;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
