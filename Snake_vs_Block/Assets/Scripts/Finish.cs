using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameState GameState;
    public Transform Player;

    private void OnTriggerEnter(Collider Player)
    {
        GameState.OnPlayerWin();
    }
}
