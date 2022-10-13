using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    private int scene = 3;

    private void OnMouseDown()
    {
        SceneManager.LoadScene(scene);
    }
}