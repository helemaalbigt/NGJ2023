using UnityEngine;
using System;
using Oculus.Interaction.Samples;
using TMPro;
using UnityEngine.SceneManagement;

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
    public GameState currentState = GameState.Waiting;
    public float timer;
    public TextMeshProUGUI scoreText;
    public int score;

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
                    score = 0;
                    UpdateScoreUi();
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
                    SceneManager.LoadScene(0);
                }
                break;
        }
    }

    public void UpdateScoreUi() {
        scoreText.text = score.ToString();
    }

    public void SetGameState(GameState newState)
    {
        currentState = newState;
        OnGameStateChanged?.Invoke(newState);
    }
}
