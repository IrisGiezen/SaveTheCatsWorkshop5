using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    private Animator anim;

    public GameObject target;

    public Camera Scenecamera;
    private Vector3 mousePosition;

    private Ray _ray;
    private RaycastHit _hit;

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
        }


        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
        anim.SetFloat("x", moveDirection.x);
        anim.SetFloat("z", moveDirection.z);
    }
}
