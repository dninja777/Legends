using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private LayerMask layerMask;

    private CharacterController characterController;
    private Vector3 currentLookTarget = Vector3.zero;
    private Animator anim;
    private BoxCollider[] swordColliders;

    

   

    // Use this for initialization
    void Start () {

        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        swordColliders = GetComponentsInChildren<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameController.shared.GameOver)
        {
            Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            characterController.SimpleMove(moveDirection * moveSpeed);

            if (moveDirection == Vector3.zero)
            {
                anim.SetBool("isWalking", false);
            }
            else
            {
                anim.SetBool("isWalking", true);
            }

            if (Input.GetMouseButtonDown(0))
            {
                anim.Play("Chop");
            }
            if (Input.GetMouseButtonDown(1))
            {
                anim.Play("SpinAttack");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.Play("Jump");
            }
        }
	}

    private void FixedUpdate()
    {
        if (!GameController.shared.GameOver)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, ray.direction * 500, Color.blue);

            if (Physics.Raycast(ray, out hit, 500, layerMask, QueryTriggerInteraction.Ignore))
            {
                if (hit.point != currentLookTarget)
                {
                    currentLookTarget = hit.point;
                }

                Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10f);
            }

        }
    }

    public void BeginAttack()
    {
        foreach(var weapon in swordColliders)
        {
            weapon.enabled = true;
        }
    }

    public void EndAttack()
    {
        foreach (var weapon in swordColliders)
        {
            weapon.enabled = false;
        }
    }
}
