using UnityEngine;

public class ClawsController : MonoBehaviour
{
    private new GameObject gameObject;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Collision Detected");
            Destroy(collision.gameObject);
        }
    }
}