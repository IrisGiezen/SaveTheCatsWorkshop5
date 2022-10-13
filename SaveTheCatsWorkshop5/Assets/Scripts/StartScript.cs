using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    private int scene = 1;

    private void OnMouseDown()
    {
        SceneManager.LoadScene(scene);
    }
}