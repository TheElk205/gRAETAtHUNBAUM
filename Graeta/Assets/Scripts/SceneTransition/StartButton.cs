using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void NextScene()
    {
        FindObjectOfType<AudioManager>().Play("Level1Start");
        SceneManager.LoadScene("Scene0");
    }
}