using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInPlayerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject menuGame;
    
    private GameState gameState;
    private PlayerState playerState;

    private void OnEnable()
    {
        InputManager.instance.OnActionTriggeredDown += OpenMenu;
    }

    private void OnDisable()
    {
        InputManager.instance.OnActionTriggeredDown -= OpenMenu;
    }

    private void OpenMenu(InputManager.InputAction action)
    {
        if(action == InputManager.InputAction.Start)
        {
            if (!menuGame.activeSelf)
            {
                gameState = GameManager.instance.CurrentGameState;
                playerState = GameManager.instance.CurrentPlayerState;
                GameManager.instance.SetGameState(GameState.Menu);
                GameManager.instance.SetPlayerState(PlayerState.NoActions);
                Time.timeScale = 0;
                menuGame.SetActive(true);
            } else
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        GameManager.instance.SetGameState(gameState);
        GameManager.instance.SetPlayerState(playerState);
        Time.timeScale = 1;
        menuGame.SetActive(false);
    }

}


