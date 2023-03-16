using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelEnd : MonoBehaviour
{
    // Start is called before the first frame update
   public static bool finish = false;
   public GameObject levelEndScreen;

    // Update is called once per frame
    void Update()
    {
        
            if(finish)
            {
                levelEndScreen.SetActive(true);
                Time.timeScale = 0f;
            }
            
        
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        levelEndScreen.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 1");
        Time.timeScale = 1f;
        finish = false;
    }
     public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main menu");
        Time.timeScale = 1f;
        finish = false;
        Time.timeScale = 1f;
        
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
