using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Random = UnityEngine.Random;

public enum GameStates
{
    IntroPlayer1,
    IntroPlayer2,
    Ready,
    Play,
    Pause,
    Resume,
    RoundOver,
    NewRound,
    Gameover,
    CheckWinner,
    WinnerOutro,
}

public enum GameScene
{
    Menu,
    Game,
    Restart,
}

public class GameManager : MonoBehaviour
{
    //INIT GAMEMANAGER
    public static GameManager Instance { get; private set; }
    //_________________________________________________________
    //PUBLIC VARIABLES
    public string currGameState;
    public string currScene;
    public float SetGameTime { get; set; }
    public float AsyncLoadTime { get; private set; }
    public int GamePoints { get; set; }
    public int MaxGamePoints { get; private set; }
    public int CurrentRound { get; private set; }
    public int Map { get; set; }
    public GameStates CurrentGameState { get; private set; }
    public PlayerManager Player1 { get; set; }
    public PlayerManager Player2 { get; set; }
    public PlayerManager Winner { get; private set; }
    public HealthbarsPoints HealthBarPoints { get; set; }
    //____________________________________________________

    public Action<GameStates> _gameState;
    public Action<GameScene> _gameScene;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //GAMETESTING UPDATE STATE WITH DROP DOWN
    //THIS FUNCTION IS CALLED FROM A DROP DOWN UI ELEMENT IN THE TEST SCENE
    public void ChangeGameStateTest(int newState)
    {
        switch (newState)
        {
            case 0:
                OnUpdateGameState(GameStates.IntroPlayer1);
                break;
            case 1:
                OnUpdateGameState(GameStates.IntroPlayer2);
                break;
            case 2:
                OnUpdateGameState(GameStates.Ready);
                break;
            case 3:
                OnUpdateGameState(GameStates.Play);
                break;

        }
    }

    //UPDATE GAMESTATE OUTSIDE OF MANAGER
    public void OnUpdateGameState(GameStates newState)
    {
        if (newState != GameStates.Pause)
        {
            CurrentGameState = newState;
        }

        if (newState == GameStates.RoundOver)
        {
            CurrentRound++;
        }
        _gameState?.Invoke(newState);

        currGameState = newState.ToString();
    }

    public void OnUpdateScene(GameScene newScene)
    {
        _gameScene?.Invoke(newScene);
        currScene = newScene.ToString();
    }

    //Check health if timer runs out
    public void EndGameOnTime()
    {
        if (Player1.player.Health > Player2.player.Health)
        {
            Player2.RemovePoint();
        }
        else if (Player2.player.Health > Player1.player.Health)
        {
            Player1.RemovePoint();
        }
        else
        {
            if (Player1.Points > 0 && Player2.Points > 0)
            {
                HealthBarPoints.RemovePointDraw();
            }
            else
            {
                //For the time bein this is a quick fix when players draw, to avoid bugs when the problem is beein worked on
                int rand = Random.Range(0, 2);
                if (rand == 0)
                {
                    Winner = Player1;
                    PlayWinnerAnimation();
                }
                else
                {
                    Winner = Player2;
                    PlayWinnerAnimation();
                }
            }
        }
    }

    //Checking if players have points left Gameover or new round
    public void CheckWinner()
    {
        if (Player1.Points <= 0)
        {
            Winner = Player2;
            PlayWinnerAnimation();
        }
        else if (Player2.Points <= 0)
        {
            Winner = Player1;
            PlayWinnerAnimation();
        }
        else
        {
            Winner = null;
        }

        if (Winner == null)
        {
            OnUpdateGameState(GameStates.RoundOver);
        }
    }

    //CHECKING FOR NEW SCENE LOADED
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 8)
        {
            GamePoints = 2;
            SetGameTime = 60;
            OnUpdateGameState(GameStates.IntroPlayer1);
            if (Time.timeScale < 1)
            {
                Time.timeScale = 1;
            }
        }
        else if (scene.buildIndex == 1 || scene.buildIndex == 2 || scene.buildIndex == 3)
        {
            OnUpdateScene(GameScene.Game);
            if (Time.timeScale < 1)
            {
                Time.timeScale = 1;
            }
            CurrentRound = 1;
            MaxGamePoints = GamePoints;
        }
        else if (scene.buildIndex == 0)
        {
            if (Time.timeScale < 1)
            {
                Time.timeScale = 1;
            }
            OnUpdateScene(GameScene.Menu);
        }
        else
        {
            if (Time.timeScale < 1)
            {
                Time.timeScale = 1;
            }
            HealthBarPoints = null;
        }
    }


    private void PlayWinnerAnimation()
    {
        Winner.PlayWinnerAnimation();
    }
}
