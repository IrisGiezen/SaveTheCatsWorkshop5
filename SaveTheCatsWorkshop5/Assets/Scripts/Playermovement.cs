using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Playermovement : MonoBehaviour
{
    CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private float forwardInput;
    private float sidewardInput;

    private Vector3 moveDirection = Vector3.zero;
    private Animator anim;

    public GameObject target;

    public Camera Scenecamera;

    public GameObject Claw;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
                anim.SetBool("Isjumping", true);
            }
        }
        else
        {
            anim.SetBool("Isjumping", false);
        }

        if (Input.GetButton("Fire2"))
        {
            anim.SetTrigger("attacking");
            Instantiate(Claw, transform.position + new Vector3(0,0.2f,0.35f), transform.rotation);
        }


        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
        anim.SetFloat("x", moveDirection.x);
        anim.SetFloat("z", moveDirection.z);
    }


    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        forwardInput = movementVector.y;
        sidewardInput = movementVector.x;

    }

}
