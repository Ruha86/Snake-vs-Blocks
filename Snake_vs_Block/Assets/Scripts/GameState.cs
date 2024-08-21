using UnityEngine;

public class GameState : MonoBehaviour
{
    public Player Controls;
    public GameObject UI;
    public GameObject LoseScreen;
    public GameObject WinScreen;

    public enum State
    {
        Play,
        Win,
        Lose,
    }

    private void Awake()
    {
        UI.SetActive(true);
        LoseScreen.SetActive(false);
        WinScreen.SetActive(false);
    }

    public State CurrentState { get; private set; }

    public void OnPlayerDead() 
    {
        if (CurrentState != State.Play) return;
        CurrentState = State.Lose;
        Controls.enabled = false;
        Debug.Log("Game Over!");
        UI.SetActive(false);
        LoseScreen.SetActive(true);
    }

    public void OnPlayerWin() 
    {
        if (CurrentState != State.Play) return;
        CurrentState = State.Win;
        Controls.enabled = false;
        Debug.Log("You win!");
        UI.SetActive(false);
        WinScreen.SetActive(true);
    }

    public int LevelIndex
    {
        get => PlayerPrefs.GetInt(LevelIndexKey, 0);
        set
        {
            PlayerPrefs.SetInt(LevelIndexKey, value);
            PlayerPrefs.Save();
        }
    }
    private const string LevelIndexKey = "LevelIndex";
}
