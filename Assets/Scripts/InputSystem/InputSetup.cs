using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class InputSetup : MonoBehaviour
{
    //GETTING REFERENCE SCRIPTS
    private PlayerInput playerInput;
    private MultiplayerEventSystem eventSystem;
    //__________________________________________

    //PUBLIC VARIABLES
    public GameObject prefab;
    public string charName;
    public Sprite charImage;
    public bool gameTesting;

    public int PlayerIndex { get; set; }
    public GameObject LastButtonSelected { get; set; }
    //________________________________

    //PRIVATE VARIABLES
    private PlayerManager pm;
    private GameObject prefabCopy;
    public GameObject firstSelectedGameObject;
    //___________________________

    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance._gameState += UpdatedGameState;
            GameManager.Instance._gameScene += UpdatedGameScene;
        }
    }

    private void OnDisable()
    {
        GameManager.Instance._gameState -= UpdatedGameState;
        GameManager.Instance._gameScene -= UpdatedGameScene;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        eventSystem = GetComponent<MultiplayerEventSystem>();
        playerInput = GetComponent<PlayerInput>();

        PlayerIndex = playerInput.playerIndex;

        if (playerInput.playerIndex == 0)
        {
            transform.SetPositionAndRotation(new Vector3(-2, 0, 0), Quaternion.Euler(0, 90, 0));
        }
        else
        {
            transform.SetPositionAndRotation(new Vector3(2, 0, 0), Quaternion.Euler(0, -90, 0));
        }

        if (gameTesting)
        {
            SetActionMap("Game");
        }

        SetSelectedGameObject(firstSelectedGameObject);
    }

    public void SetActionMap(string actionMap)
    {
        playerInput.SwitchCurrentActionMap(actionMap);
    }

    public void InstantiateCharacter()
    {
        if (prefabCopy == null)
        {
            prefabCopy = Instantiate(prefab);
            prefabCopy.transform.SetParent(gameObject.transform);
            prefabCopy.transform.SetPositionAndRotation(gameObject.transform.position, gameObject.transform.rotation);
            pm = prefabCopy.GetComponent<PlayerManager>();
        }
    }

    public void DestroyPrefab()
    {
        if (prefabCopy != null)
        {
            Destroy(prefabCopy);
        }
    }

    public void SetSelectedGameObject(GameObject go)
    {
        if (eventSystem)
        {
            eventSystem.SetSelectedGameObject(go);
        }
        else
        {
            Debug.Log("Could not find any eventsystems");
        }
    }

    public void SetSelectedGameObjectOnJoined()
    {
        Button firstGameObject = FindObjectOfType<Button>();

        if (firstGameObject)
        {
            firstSelectedGameObject = firstGameObject.gameObject;
        }
        else
        {
            Debug.Log("Could not find any buttons");
        }
    }

    public void SetSelectedGameObjectCharacterSelection()
    {
        SetButtonToEventSystem btnSystem = FindObjectOfType<SetButtonToEventSystem>();
        if (btnSystem != null)
        {
            btnSystem.SetButtonToJoinedPlayer(this);
        }
    }

    private void UpdatedGameState(GameStates newState)
    {
        if (pm)
        {
            if (newState != GameStates.Play)
            {
                pm.CanWalk = false;
            }
            else
            {
                pm.CanWalk = true;
            }
        }
        switch (newState)
        {
            case GameStates.IntroPlayer1:
                SetActionMap("Menu");
                if (pm)
                {
                    if (PlayerIndex == 0)
                    {
                        pm.PlayIntro();
                    }
                }
                break;
            case GameStates.IntroPlayer2:
                SetActionMap("Menu");
                if (PlayerIndex == 1)
                {
                    pm.PlayIntro();
                }
                break;
            case GameStates.NewRound:
                SetActionMap("Menu");
                if (pm)
                {
                    pm.CanAttack = false;
                }
                break;
            case GameStates.Ready:
                SetActionMap("Menu");
                if (pm)
                {
                    pm.ResetRound();
                    pm.CanAttack = false;
                }
                break;
            case GameStates.Play:
                SetActionMap("Game");
                if (pm)
                {
                    pm.CanAttack = true;
                }
                break;
            case GameStates.RoundOver:
                SetActionMap("Menu");
                if (pm)
                {
                    pm.CanAttack = false;
                }
                break;
            case GameStates.Gameover:
                SetActionMap("Menu");
                if (pm)
                {
                    pm.CanAttack = false;
                }
                break;
            case GameStates.Pause:
                SetActionMap("Menu");
                if (pm)
                {
                    pm.CanAttack = false;
                }
                break;
            case GameStates.WinnerOutro:
                SetActionMap("Menu");
                if (pm)
                {
                    pm.CanAttack = false;
                    pm.MoveLooser();
                }
                break;
        }
    }

    private void UpdatedGameScene(GameScene newScene)
    {
        switch (newScene)
        {
            case GameScene.Menu:
                DestroyPrefab();
                SetActionMap("Menu");
                break;
            case GameScene.Game:
                InstantiateCharacter();
                break;
            case GameScene.Restart:
                pm.RestartGame();
                break;
        }
    }
}
