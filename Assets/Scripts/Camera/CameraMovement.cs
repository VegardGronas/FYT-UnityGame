using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CustomCamera;

public class CameraMovement : MonoBehaviour
{
    private InputSetup[] players;
    public GameObject leftCollider;
    public GameObject rightCollider;

    private PlayerManager p1;
    private PlayerManager p2;

    [Range(1.6f, 100f)]
    public float offsetZ;
    [Range(-1, 10)]
    public float offsetY;
    public float lerpSpeed;

    private Vector3 targetPosition;

    public Vector3 CustomCameraRotation;

    private void Start()
    {
        players = FindObjectsOfType<InputSetup>();
        foreach (InputSetup player in players)
        {
            if (player.GetComponent<PlayerInput>().playerIndex == 0)
            {
                p1 = player.GetComponentInChildren<PlayerManager>();
            }
            else
            {
                p2 = player.GetComponentInChildren<PlayerManager>();
            }
        }

        if (GameManager.Instance.CurrentGameState == GameStates.IntroPlayer1)
        {
            transform.position = p1.cameraIntroTracker.position;
        }
    }

    private void LateUpdate()
    {
        if (GameManager.Instance.CurrentGameState != GameStates.Play)
        {
            leftCollider.SetActive(false);
            rightCollider.SetActive(false);
        }
        else
        {
            leftCollider.SetActive(true);
            rightCollider.SetActive(true);
            float playerCenter = (p1.gameObject.transform.position.x + p2.gameObject.transform.position.x) / 2;
            float dist = Vector3.Distance(transform.position, new Vector3(playerCenter, 0, 0));
            Vector3 leftPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, dist));
            Vector3 rightPoint = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, dist));
            rightCollider.transform.position = rightPoint;
            leftCollider.transform.position = leftPoint;
        }

        if (GameManager.Instance.CurrentGameState == GameStates.IntroPlayer1)
        {
            transform.SetPositionAndRotation(p1.cameraIntroTracker.transform.position, p1.cameraIntroTracker.transform.rotation);
        }
        else if (GameManager.Instance.CurrentGameState == GameStates.IntroPlayer2)
        {
            transform.SetPositionAndRotation(p2.cameraIntroTracker.transform.position, p2.cameraIntroTracker.transform.rotation);
        }
        else if (GameManager.Instance.CurrentGameState == GameStates.WinnerOutro || GameManager.Instance.CurrentGameState == GameStates.Gameover)
        {
            if (p1 == GameManager.Instance.Winner)
            {
                transform.SetPositionAndRotation(p1.cameraIntroTracker.transform.position, p1.cameraIntroTracker.transform.rotation);
            }
            else if (p2 == GameManager.Instance.Winner)
            {
                transform.SetPositionAndRotation(p2.cameraIntroTracker.transform.position, p2.cameraIntroTracker.transform.rotation);
            }
        }
        else
        {
            targetPosition = CustomCameraClass.GetCenter(p1.cameraTracker.transform.position, p2.cameraTracker.transform.position, -offsetZ, offsetY);
            transform.SetPositionAndRotation(Vector3.Lerp(transform.position, targetPosition, lerpSpeed), Quaternion.Euler(CustomCameraRotation.x, CustomCameraRotation.y, CustomCameraRotation.z));
        }
    }
}
