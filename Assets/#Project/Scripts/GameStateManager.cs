using UnityEngine;
using System;

public class GameStateManager : MonoBehaviour
{
    public enum GameState
    {
        GameStarting,
        GameRunning,
        GameOver
    }

    public static GameStateManager Instance { get; private set; }

    [SerializeField] private float startCountDown = 3.0f;
    [SerializeField] private float gameTime = 10.0f;

    private GameState currentState;
    private float timer;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetGameState(GameState.GameStarting);
        timer = startCountDown;
    }

    private void Update()
    {
        switch (currentState)
        {
            case GameState.GameStarting:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    SetGameState(GameState.GameRunning);
                    timer = gameTime;
                }
                break;

            case GameState.GameRunning:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    SetGameState(GameState.GameOver);
                }
                break;

            case GameState.GameOver:
                // Handle the game over state, e.g., show a game over screen or restart the game
                break;
        }
    }

    private void SetGameState(GameState newState)
    {
        currentState = newState;
        OnGameStateChanged?.Invoke(newState);
    }
}
