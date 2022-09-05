using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUI : MonoBehaviour
{
    [SerializeField]
    private GameObject KO;
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private TextMeshProUGUI winnerText;
    [SerializeField]
    private GameObject readyText;
    [SerializeField]
    private GameObject pauseMenu;
    
    //FIx everything
    public TextMeshProUGUI timer;

    public List<PlayerManager> players;

    public bool StartCountDown { get; set; }

    public float GameTime { get; set; }

    [SerializeField]
    private bool inPreviewMode;

    private void Start()
    {
        if (inPreviewMode)
        {
            pauseMenu.SetActive(false);
        }
        else
        {
            gameOverUI.SetActive(false);
            KO.SetActive(false);
            readyText.SetActive(false);
            pauseMenu.SetActive(false);
        }
    }

    public void UpdateGameTime(float time)
    {
        timer.text = time.ToString("F0");
    }

    public void GameOver(string playerName)
    {
        winnerText.text = playerName + " Wins!";
    }

    public void PauseMenu(bool active)
    {
        if (inPreviewMode)
        {
            pauseMenu.SetActive(active);
        }
        else
        {
            gameOverUI.SetActive(false);
            KO.SetActive(false);
            readyText.SetActive(false);
            pauseMenu.SetActive(active);
        }
        if (active)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            GameManager.Instance.OnUpdateGameState(GameStates.Play);
        }
    }

    private void Update()
    {
        if (!inPreviewMode)
        {
            if (StartCountDown)
            {
                if (GameTime > 0)
                {
                    GameTime -= Time.deltaTime;
                    UpdateGameTime(GameTime);
                }
                else
                {
                    if (GameManager.Instance.CurrentGameState == GameStates.Play)
                    {
                        GameManager.Instance.EndGameOnTime();
                    }
                }
            }
        }
    }
}
