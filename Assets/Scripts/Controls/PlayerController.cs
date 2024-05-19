using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Windows.Speech;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController cc;
    [SerializeField] Rigidbody rb;
    [SerializeField] float playerSpeed;
    [SerializeField] ParticleSystem stepsFx;
    [SerializeField] KeyCode getDirectionKey;

    [SerializeField] Vector3 groundDirection;
    [SerializeField] CombatManager combatManager;
    [SerializeField] NavMeshAgent playerGuide;
    [SerializeField] Animator anim;
    Vector3 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //gets the vectors and the gravity values
        groundDirection.x = Input.GetAxisRaw("Horizontal");
        groundDirection.z = Input.GetAxisRaw("Vertical"); 
     

        moveDir.x = groundDirection.x;
        moveDir.z = groundDirection.z;

        moveDir.y = rb.velocity.y;

        //checks if its grounded
        if (cc.isGrounded)
        {
            rb.velocity = Vector3.zero;
        }
        //moves the player
        cc.Move(moveDir.normalized * playerSpeed * Time.deltaTime);

        //turns the player face 
        if (groundDirection != Vector3.zero)
        {
            if(combatManager.status != PlayerStatus.ATTACKING)
            {
                transform.forward = groundDirection * Time.deltaTime;
            }
            //plays the steps
            anim.SetBool("Walk", true);
            stepsFx.Play();
        }
        if(groundDirection == Vector3.zero)
        {
            stepsFx.Pause();
            anim.SetBool("Walk", false);
        }

        //gets the objective direction
        if(Input.GetKeyDown(getDirectionKey))
        {
            GameManager.Instance.DirectPlayer();
        }

    }
}
