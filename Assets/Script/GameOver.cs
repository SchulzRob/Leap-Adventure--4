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
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene("Level 1");
        Time.timeScale = 1f;
        dead = false;
    }
     public void MainMenu()
    {
        SceneManager.LoadScene("Main menu");
        dead = false;
        Time.timeScale = 1f;
        
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
