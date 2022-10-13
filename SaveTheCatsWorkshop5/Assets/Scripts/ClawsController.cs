using UnityEngine;

public class ClawsController : MonoBehaviour
{

    private void Awake()
    {
        Destroy(gameObject, 0.2f);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Collision Detected");
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Collision Detected");
            Destroy(collider.gameObject);
        }
    }

}