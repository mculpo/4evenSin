using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteractiveIconManager : Singleton<UIInteractiveIconManager>
{
    [SerializeField] private GameObject interactiveGameObject;
    [SerializeField] private InteractionIconDisplay interactiveIconMenuBehaviour;

    private IItemDisplay interactiveDisplay;

    private GameState gameState;
    private PlayerState playerState;

    void Start()
    {
        Initialize(this);
        interactiveGameObject.SetActive(false);
    }
   
    void OnEnable()
    {
        InputManager.instance.OnActionTriggeredDown += OpenInteractiveTable;
    }

    void OnDisable()
    {
        InputManager.instance.OnActionTriggeredDown -= OpenInteractiveTable;
    }

    private void OpenInteractiveTable(InputManager.InputAction action)
    {
        if(action == InputManager.InputAction.ButtonY && interactiveGameObject.activeSelf)
        {
            if(!interactiveDisplay.IsActive())
            {
                interactiveDisplay.OnDisplayInfo();
                gameState = GameManager.instance.CurrentGameState;
                playerState = GameManager.instance.CurrentPlayerState;

                GameManager.instance.SetGameState(GameState.Menu);
                GameManager.instance.SetPlayerState(PlayerState.NoActions);
            } 
            else 
            {
                OnCloseInfoBossGameObject();
            }
        }
    }

    public void OnCloseInfoBossGameObject()
    {
        interactiveDisplay.OffDisplayInfo();
        GameManager.instance.SetGameState(gameState);
        GameManager.instance.SetPlayerState(playerState);
    }

    public void OnEnableImageInteractive(Transform refPosition, IItemDisplay interactiveDisplay)
    {
        if (this.interactiveIconMenuBehaviour.worldObject == null || 
            this.interactiveIconMenuBehaviour.worldObject != refPosition)
        {
            interactiveGameObject.SetActive(true);
            this.interactiveDisplay = interactiveDisplay;
            this.interactiveIconMenuBehaviour.worldObject = refPosition;
        }
    }

    public void OnDisableImageInteractive(Transform refPosition)
    {
        if (this.interactiveIconMenuBehaviour.worldObject == refPosition)
        {
            this.interactiveIconMenuBehaviour.worldObject = null;
            this.interactiveDisplay = null;
            interactiveGameObject.SetActive(false);
        }
    }
}
