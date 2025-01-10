using UnityEngine;

public enum GameState
{
    Menu,
    Playing,
    PauseGame,
    GameOver
}

public enum PlayerState
{
    NoActions = 0,
    Attack =        1 << 0,         // 1 (00000001)
    Move =          1 << 1,         // 2 (00000010)
    Rotation =      1 << 2,         // 4 (00000100)
    Interactive =   1 << 3,         // 8 (00001000)
    All = Attack | Move | Rotation | Interactive, 
}

public class GameManager : Singleton<GameManager>
{
    public GameState CurrentGameState { get; private set; }
    public PlayerState CurrentPlayerState { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            Initialize(this);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        CurrentGameState = GameState.Playing;
        CurrentPlayerState = PlayerState.Move | PlayerState.Rotation;
    }

    public void SetGameState(GameState newState)
    {
        CurrentGameState = newState;
        Debug.Log($"Game State changed to: {CurrentGameState}");

        // Example: Pause the game if state is PauseGame
        Time.timeScale = (CurrentGameState == GameState.PauseGame) ? 0 : 1;
    }

    public void AddPlayerState(PlayerState newState)
    {
        CurrentPlayerState |= newState;
        Debug.Log($"Added action: {newState}. Current actions: {CurrentPlayerState}");
    }

    public void RemovePlayerState(PlayerState state)
    {
        CurrentPlayerState &= ~state;
        Debug.Log($"Removed action: {state}. Current actions: {CurrentPlayerState}");
    }

    public bool HasPlayerState(PlayerState state)
    {
        return (CurrentPlayerState & state) == state;
    }

    public void SetPlayerState(PlayerState newState)
    {
        CurrentPlayerState = newState;
        Debug.Log($"Player State changed to: {CurrentPlayerState}");
    }

    public void StartGame()
    {
        SetGameState(GameState.Playing);
        SetPlayerState(PlayerState.All);
    }

    public void PauseGame()
    {
        SetGameState(GameState.PauseGame);
        SetPlayerState(PlayerState.NoActions);
    }

    public void ResumeGame()
    {
        SetGameState(GameState.Playing);
        SetPlayerState(PlayerState.All);
    }
    public void MenuGame()
    {
        SetGameState(GameState.Menu);
        SetPlayerState(PlayerState.NoActions);
    }


    public void GameOver()
    {
        SetGameState(GameState.GameOver);
        SetPlayerState(PlayerState.NoActions);
    }
}
