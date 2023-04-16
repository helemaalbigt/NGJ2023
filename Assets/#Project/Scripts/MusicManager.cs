using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource music;
    private void Awake()
    {
        music = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        GameStateManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        GameStateManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void HandleGameStateChanged(GameStateManager.GameState newState)
    {
        switch (newState)
        {
            case GameStateManager.GameState.GameStarting:
                Debug.Log("Game Starting!");
                break;

            case GameStateManager.GameState.GameRunning:
                Debug.Log("Game Running!");
                music.Play();
                break;

            case GameStateManager.GameState.GameOver:
                Debug.Log("Game Over!");
                music.Stop();
                break;
        }
    }
}
