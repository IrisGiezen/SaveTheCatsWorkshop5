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
  
    void FixedUpdate()
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
            Instantiate(Claw, transform.position + new Vector3(0, 0.2f, 0.35f), transform.rotation);
        }

        if (Input.GetKey(KeyCode.J))
            transform.Rotate(-Vector3.up * 90 * Time.deltaTime);

        if (Input.GetKey(KeyCode.L))
            transform.Rotate(Vector3.up * 90 * Time.deltaTime);

        


        moveDirection.y -= gravity * Time.deltaTime;
        anim.SetFloat("x", moveDirection.x);
        anim.SetFloat("z", moveDirection.z);

        // Move the controller
        moveDirection = this.transform.TransformDirection(moveDirection);
        characterController.Move(moveDirection * Time.deltaTime);

    }


    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        forwardInput = movementVector.y;
        sidewardInput = movementVector.x;
    }

    void OnControllerColliderHit(ControllerColliderHit collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            Debug.Log("Collision Food Detected");
            Destroy(collision.gameObject);
            speed += 2;
        }

        if (collision.gameObject.CompareTag("NotFood"))
        {
            Debug.Log("Collision Not Food Detected");
            Destroy(collision.gameObject);
            speed -= 2;
        }

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Food"))
        {
            Debug.Log("Collider Food Detected");
            Destroy(collider.gameObject);
            speed += 2;
        }

        if (collider.gameObject.CompareTag("NotFood"))
        {
            Debug.Log("Collider Not Food Detected");
            Destroy(collider.gameObject);
            speed -= 2;
        }
    }
}