using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScript : MonoBehaviour
{
    private int scene = 2;

    private void OnMouseDown()
    {
        SceneManager.LoadScene(scene);
    }
}