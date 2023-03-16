using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level 1");
        PauseMenu.gameOnPause = false;
        Time.timeScale = 1f;
    }

    public void Options()
    {
        Debug.Log("Options not implemented yet");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

