using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GameSystem : MonoBehaviour
{
    public static GameSystem instance;

    public FlappyChar flappyChar;
    public CanvasGroup gameStateCg;
    public CanvasGroup hudCg;
    public CanvasGroup retryCg;

    public CollectibleManager collectibleManager;

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

        gameStateCg.alpha = 1;
        gameStateCg.blocksRaycasts = true;
        gameStateCg.interactable = true;
    }

    public void OnDie()
    {
        state = GameState.None;
        flappyChar.OnEndGame();

        gameStateCg.alpha = 0;
        gameStateCg.blocksRaycasts = false;
        gameStateCg.interactable = false;

        hudCg.alpha = 0;
        music.Stop();


        retryCg.alpha = 0;
        retryCg.blocksRaycasts = false;
        retryCg.interactable = false;
        retryCg.DOFade(1, 1).OnComplete(() =>
        {
            retryCg.blocksRaycasts = true;
            retryCg.interactable = true;
        });
    }

    public void EndGame()
    {
        state = GameState.None;
        flappyChar.OnEndGame();


        retryCg.alpha = 0;
        retryCg.blocksRaycasts = false;
        retryCg.interactable = false;

        hudCg.alpha = 0;
        music.Stop();
    }

    public void StartGame()
    {
        collectibleManager.ResetCollectibles();
        flappyChar.OnStartGame();
        FlappyCamera.instance.SyncPos();

        gameStateCg.blocksRaycasts = false;
        gameStateCg.interactable = false;
        if (gameStateCg.alpha == 1)
            gameStateCg.DOFade(0, 1f).OnComplete(OnStartGame);

        retryCg.blocksRaycasts = false;
        retryCg.interactable = false;
        if (retryCg.alpha == 1)
            retryCg.DOFade(0, 1f).OnComplete(OnStartGame);
    }

    void OnStartGame()
    {
        hudCg.alpha = 1;
        state = GameState.Playing;
      
        music.Play();
    }
}
