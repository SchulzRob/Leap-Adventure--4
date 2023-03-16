using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
   public static bool dead = false;
   public GameObject gameOverScreen;

    // Update is called once per frame
    void Update()
    {
        
            if(dead)
            {
                gameOverScreen.SetActive(true);
                Time.timeScale = 0f;
            }
            
        
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        gameOverScreen.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 1");
        Time.timeScale = 1f;
        dead = false;
    }
     public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main menu");
        Time.timeScale = 1f;
        dead = false;
        Time.timeScale = 1f;
        
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
