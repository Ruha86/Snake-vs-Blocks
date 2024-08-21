using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelNumberText : MonoBehaviour
{
    public Text LevelNumber;
    
    void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();
        LevelNumber.text = scene.name;
    }

}
