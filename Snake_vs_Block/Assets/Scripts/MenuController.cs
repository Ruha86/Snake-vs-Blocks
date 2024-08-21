using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string levelName;
    Scene scene;

    private void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();
        levelName = scene.name;
    }

    public void StartGame(string SceneName)
    {
        SceneManager.LoadScene("Level 1");
    }

    public void SwitchLevel(string SceneName) 
    {
        SceneManager.LoadScene(SceneName);
    }

    public void ReloadLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToMainMenu() 
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        switch (levelName) 
        {
            case ("Level 1"):
                SceneManager.LoadScene("Level 2");
                break;
            case ("Level 2"):
                SceneManager.LoadScene("Level 3");
                break;
            case ("Level 3"):
                SceneManager.LoadScene("Level 1");
                break;
        }
        
    }
}
