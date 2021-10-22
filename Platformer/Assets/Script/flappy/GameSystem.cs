using UnityEngine;
using System.Collections;

public class GameSystem : MonoBehaviour
{
    public static GameSystem instance;

    public FlappyChar flappyChar;
    public CanvasGroup gameStateCg;
    public CanvasGroup hudCg;
    public AudioSource music;

    private void Awake()
    {
        instance = this;
    }

    public enum GameState
    {
        None,
        Playing,
    }

    public GameState state;

    private void Start()
    {
        EndGame();
    }

    public void EndGame()
    {
        state = GameState.None;
        flappyChar.OnEndGame();

        gameStateCg.alpha = 1;
        gameStateCg.blocksRaycasts = true;
        gameStateCg.interactable = true;
        hudCg.alpha = 0;
        music.Stop();
    }

    public void StartGame()
    {
        state = GameState.Playing;
        flappyChar.OnStartGame();

        gameStateCg.alpha = 0;
        gameStateCg.blocksRaycasts = false;
        gameStateCg.interactable = false;
        hudCg.alpha = 1;

        FlappyCamera.instance.SyncPos();
        music.Play();
    }
}
