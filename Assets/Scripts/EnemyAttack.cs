using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {


    [SerializeField] private float range = 3f;
    [SerializeField] private float timeBetweenAttacks = 1f;

    private Animator anim;
    private GameObject player;
    private bool playerInRange;
    private BoxCollider[] weaponColliders;
    private EnemyHealth enemyHealth;

   
    // Use this for initialization
    void Start () {
        weaponColliders = GetComponentsInChildren<BoxCollider>();
        player = GameController.shared.Player;
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        StartCoroutine(Attack());
	}
	
	// Update is called once per frame
	void Update () {

        //every frame it checks to see  if player is in range to be attacked
        if(Vector3.Distance(transform.position, player.transform.position) < range && enemyHealth.IsAlive)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }

	}
        IEnumerator Attack()
    {
        if(playerInRange && !GameController.shared.GameOver != false)
        {
            anim.Play("Attack");
            yield return new WaitForSeconds(timeBetweenAttacks);

        }
        yield return null;
        StartCoroutine(Attack());
    }

    //methods is from by animationevent of the same name to enable te colliders at a certain frame
    public void EnemyBeginAttack()
    {
        foreach(var weapon in weaponColliders)
        {
            weapon.enabled = true;
        }
    }
    //methods is from by animationevent of the same name to disable te colliders at a certain frame
    public void EnemyEndAttack()
    {
        foreach(var weapon in weaponColliders)
        {
            weapon.enabled = false;
        }
    }

}
