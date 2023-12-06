using Newtonsoft.Json;
using StarterAssets;
using UnityEngine;

public class GameManager : SingletonGeneric<GameManager>
{
    public UIController UIController;
    public ThirdPersonController player;
    public MBTIQuestionController mbtiQuesController;
    public ReviewQuestionController reviewQuestionController;
    private QuestionController questionController;
    
    public GameState GameState;

    public QuestionController QuestionController { get => questionController;}


    private void Awake()
    {
        Application.targetFrameRate = 60;
        int gameMode = PlayerPrefs.GetInt("gamemode", 0);
        if(gameMode == 0)
        {
            questionController = mbtiQuesController;
            Debug.Log(0);
        }
        else
        {
            questionController = reviewQuestionController;
            Debug.Log(1);
        }
    }

    private void Start()
    {
        StartingGame();

    }
    private void StartingGame()
    {
        Debug.Log("Starting game");
    }
    public void ChangeState(GameState state)
    {
        GameState = state;
        switch (state)
        {
            case GameState.Playing: player.CanMove = true; Cursor.lockState = CursorLockMode.Locked; break;
            case GameState.Answering: player.CanMove = false; Cursor.lockState = CursorLockMode.None; break;
        }
    }

    public void GameVictory()
    {
        UIController.HideUI();
        UIController.ShowVictory();
        SoundManager.Instance.PlaySound(SoundName.Victory);
    }
}

public enum GameState
{
    Playing,
    Answering
}
