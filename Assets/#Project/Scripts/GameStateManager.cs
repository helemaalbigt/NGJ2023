using UnityEngine;
using System;

public class GameStateManager : MonoBehaviour
{
    public enum GameState
    {
        Waiting,
        GameStarting,
        GameRunning,
        GameOver
    }

    public static GameStateManager Instance { get; private set; }

    [SerializeField] private float startCountDown = 3.0f;
    [SerializeField] private float gameTime = 10.0f;
    [SerializeField] private float gameoverTime = 5f;
    private GameState currentState;
    public float timer;

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
        SetGameState(GameState.Waiting);
        timer = startCountDown;
    }

    private void Update()
    {
        switch (currentState)
        {
            case GameState.Waiting:
                break;
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
                    timer = gameoverTime;
                    SetGameState(GameState.GameOver);
                }
                break;

            case GameState.GameOver:
                // Handle the game over state, e.g., show a game over screen or restart the game
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = startCountDown;
                    SetGameState(GameState.Waiting);
                }
                break;
        }
    }

    public void SetGameState(GameState newState)
    {
        currentState = newState;
        OnGameStateChanged?.Invoke(newState);
    }
}
