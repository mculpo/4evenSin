using UnityEngine;

public enum GameState
{
    Menu,
    Playing,
    PauseGame,
    GameOver
}

public class GameManager : Singleton<GameManager>
{
    public GameState currentState;

    private void Awake()
    {
        Initialize(this);
        currentState = GameState.Playing;
    }
}
