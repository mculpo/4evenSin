using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeSceneHandle : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private List<PlayerState> playerStates;
    void Awake()
    {
        GameManager.instance.SetPlayerState(PlayerState.NoActions);
        GameManager.instance.SetGameState(gameState);
        
        foreach(PlayerState states in playerStates)
        {
            GameManager.instance.AddPlayerState(states);
        }
        Time.timeScale = 1;
    }
}
