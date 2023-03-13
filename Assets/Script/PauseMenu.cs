using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
   public static bool gameOnPause = false;
   public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) ||Input.GetKeyDown(KeyCode.Joystick1Button7)) 
        {
            if(gameOnPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameOnPause = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameOnPause = true;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main menu");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
