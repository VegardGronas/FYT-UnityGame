using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarsPoints : MonoBehaviour
{
    private PlayerManager[] players;

    [SerializeField]
    private Image pointImage;
    [SerializeField]
    private List<GameObject> parents;
    [SerializeField]
    private List<Image> healtBars;

    public List<Image> pointsLeft;
    public List<Image> pointsRight;

    private void Start()
    {
        GameManager.Instance.HealthBarPoints = this;
        SetUpPoints(GameManager.Instance.GamePoints);

        players = FindObjectsOfType<PlayerManager>();
    }

    public void SetUpPoints(int points)
    {
        for (int i = 0; i < points; i++)
        {
            RectTransform rectPointLeft;
            RectTransform rectPointRight;

            Image pointCopyLeft = Instantiate(pointImage);
            rectPointLeft = pointCopyLeft.GetComponent<RectTransform>();
            pointCopyLeft.transform.SetParent(parents[0].transform);
            rectPointLeft.anchoredPosition = new Vector3(rectPointLeft.sizeDelta.x * i, 0, 0);
            pointsLeft.Add(pointCopyLeft);


            Image pointCopyRight = Instantiate(pointImage);
            rectPointRight = pointCopyRight.GetComponent<RectTransform>();
            pointCopyRight.transform.SetParent(parents[1].transform);
            rectPointRight.anchoredPosition = new Vector3(rectPointLeft.sizeDelta.x * i, 0, 0);
            pointsRight.Add(pointCopyRight);
        }
    }

    public void RemovePoint(int playerIndex)
    {
        switch (playerIndex)
        {
            case 0:
                if (pointsLeft.Count > 0)
                {
                    Destroy(pointsLeft[^1], 1f);
                    pointsLeft.RemoveAt(pointsLeft.Count - 1);
                }
                break;
            case 1:
                if (pointsRight.Count > 0)
                {
                    Destroy(pointsRight[^1], 1f);
                    pointsRight.RemoveAt(pointsRight.Count - 1);
                }
                break;
        }

        GameManager.Instance.CheckWinner();
    }

    public void RemovePointDraw()
    {
        foreach (PlayerManager player in players)
        {
            player.Points--;
        }
        if (pointsRight.Count > 0)
        {
            Destroy(pointsRight[^1], 1f);
            pointsRight.RemoveAt(pointsRight.Count - 1);
        }
        if (pointsLeft.Count > 0)
        {
            Destroy(pointsLeft[^1], 1f);
            pointsLeft.RemoveAt(pointsLeft.Count - 1);
        }
        GameManager.Instance.CheckWinner();
    }

    public void UpdateHealth(int playerIndex, float health)
    {
        switch (playerIndex)
        {
            case 0:
                healtBars[0].fillAmount = health;
                break;
            case 1:
                healtBars[2].fillAmount = health;
                break;
        }
    }
}
