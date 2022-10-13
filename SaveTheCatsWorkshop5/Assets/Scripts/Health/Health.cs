using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField]
    public float HpCurrent = 3.0f;

    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    public GameObject GothitScreen;

    public Transform LastCheckpoint;

    // Start is called before the first frame update
    void Start()
    {
        totalhealthBar.fillAmount = HpCurrent / 10;
        LastCheckpoint = transform;
    }

    // Update is called once per frame
    void Update()
    {
        currenthealthBar.fillAmount = HpCurrent / 10;
        if (HpCurrent <= 0)
        {
            Debug.Log("You died");
            SceneManager.LoadScene(3);
        }
    
        if (GothitScreen.GetComponent<Image>().color.a > 0)
        {
            var color = GothitScreen.GetComponent<Image>().color;
            color.a -= 0.01f;
            GothitScreen.GetComponent<Image>().color = color;
        }

        if (transform.position.y < -20)
        {
                SceneManager.LoadScene(1);
        }
    }
    void OnControllerColliderHit(ControllerColliderHit collision)
    {
        Debug.Log(collision);
        if (collision.transform.tag == "Enemy")
        {
            HpCurrent = HpCurrent - 1;
            gotHurt();
        }

        if (collision.gameObject.CompareTag("checkpoint"))
        {
            LastCheckpoint = collision.transform;
        }

        Debug.Log(collision);
        if (collision.transform.tag == "Exit")
        {
            SceneManager.LoadScene(2);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        if (collision.transform.tag == "Enemy")
        {
            HpCurrent = HpCurrent - 1;
            gotHurt();
        }

        if (collision.gameObject.CompareTag("checkpoint"))
        {
            LastCheckpoint = collision.transform;
        }

    }
    void gotHurt()
    {
        var color = GothitScreen.GetComponent<Image>().color;
        color.a = 0.8f;

        GothitScreen.GetComponent<Image>().color = color;
        this.transform.position = LastCheckpoint.position + new Vector3(0,2,0);
    }

}
