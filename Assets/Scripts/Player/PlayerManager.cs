using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;


public enum BodyTransforms
{
    LeftArm,
    RightArm,
    LeftLeg,
    RightLeg
}

public class PlayerManager : MonoBehaviour
{
    //REFERENCE TO OTHER SCRIPTS 
    public CharacterScripableObjects characterInfo;
    public PlayerObjectScript player;
    private PlayerHealthManager playerHealth;
    private PlayerInput playerInput;
    private Animator anim;
    private HealthbarsPoints uiHealthBars;
    private bool winner;
    private PlayerMovement playerMovement;
    //________________________________

    //PUBLIC VARIABLES
    public Transform cameraIntroTracker;
    public Transform cameraTracker;
    public Transform highestPoint;
    public Transform lowestPoint;
    public Transform front;
    public Transform back;
    //__________________________

    //GAMEOBJECTS
    [SerializeField]
    private GameObject introDirector;
    [SerializeField]
    private GameObject outroDirector;
    //_____________________

    public bool gameTesting;
    //GETTERS AND SETTERS
    public int Points { get; set; }

    public int PlayerIndex { get; set; }

    public bool CanAttack { get; set; }

    public int VfxAnimationPhase { get; set; }
    public bool CanWalk { get; set; }
    //___________________________

    private void Start()
    {
        if (gameTesting)
        {
            CanWalk = true;
        }
        winner = false;
        playerMovement = GetComponent<PlayerMovement>();
        playerInput = GetComponentInParent<PlayerInput>();
        player = new PlayerObjectScript(characterInfo.name, characterInfo.maxHealth, playerInput.playerIndex);
        PlayerIndex = playerInput.playerIndex;
        playerHealth = GetComponent<PlayerHealthManager>();
        anim = GetComponent<Animator>();
        uiHealthBars = FindObjectOfType<HealthbarsPoints>();

        if (PlayerIndex == 0)
        {
            GameManager.Instance.Player1 = this;
        }
        else
        {
            GameManager.Instance.Player2 = this;
        }

        Points = GameManager.Instance.GamePoints;
    }

    public void PlayIntro()
    {
        if (introDirector != null)
        {
            introDirector.GetComponent<PlayableDirector>().Play();
        }
    }

    public void EndIntro()
    {
        if (PlayerIndex == 0)
        {
            GameManager.Instance.OnUpdateGameState(GameStates.IntroPlayer2);
        }
        else
        {
            GameManager.Instance.OnUpdateGameState(GameStates.Ready);
        }
    }

    public void EndWinnerAnimation()
    {
        GameManager.Instance.OnUpdateGameState(GameStates.Gameover);
    }

    public void ResetRound()
    {
        player.Health = characterInfo.maxHealth;
        playerHealth.ResetRound();
        if (uiHealthBars == null)
        {
            uiHealthBars = FindObjectOfType<HealthbarsPoints>();
            uiHealthBars.UpdateHealth(PlayerIndex, player.Health / characterInfo.maxHealth);
        }
        else
        {
            uiHealthBars.UpdateHealth(PlayerIndex, player.Health / characterInfo.maxHealth);
        }
    }

    public void RestartGame()
    {
        anim.SetBool("Winner", false);
        player.Health = characterInfo.maxHealth;
        Points = GameManager.Instance.GamePoints;
        playerHealth.ResetRound();
        outroDirector.GetComponent<PlayableDirector>().Stop();
        if (uiHealthBars == null)
        {
            uiHealthBars = FindObjectOfType<HealthbarsPoints>();
            uiHealthBars.UpdateHealth(PlayerIndex, player.Health / characterInfo.maxHealth);
        }
    }

    public int PlayerRotation { get; set; }

    public void UpdatePlayerHealth(float newHealth)
    {
        player.Health -= newHealth;
        if (player.Health <= 0)
        {
            playerHealth.IsDead = true;
            anim.SetBool("Dead", true);
            anim.SetBool("CheckDeath", true);
            RemovePoint();
        }
        if (uiHealthBars != null)
        {
            uiHealthBars.UpdateHealth(PlayerIndex, player.Health / characterInfo.maxHealth);
        }
        else
        {
            uiHealthBars = FindObjectOfType<HealthbarsPoints>();
        }
    }

    public void RemovePoint()
    {
        Points--;
        uiHealthBars.RemovePoint(PlayerIndex);
    }

    public void PlayWinnerAnimation()
    {
        winner = true;
    }

    public void MoveLooser()
    {
        transform.position = transform.parent.transform.position;
    }

    private void Update()
    {
        if (winner && playerMovement.grounded)
        {
            if (anim.GetInteger("Attacks") < 1)
            {
                anim.SetBool("Winner", true);
                GameManager.Instance.OnUpdateGameState(GameStates.WinnerOutro);
                outroDirector.GetComponent<PlayableDirector>().Play();
                winner = false;
            }
        }
    }
}
