using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    private Image backgroundImage;
    [SerializeField]
    private List<Sprite> backgroundSprites;
    [SerializeField]
    private TextMeshProUGUI text;

    public GameScene sceneToLoad;
    private int randomNumber;

    IEnumerator LoadSceneAsync(int scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / .9f);
            float prog = (progress * 100f);
            text.text = prog.ToString("F0") + " %";
            yield return null;
        }
    }

    private void OnEnable()
    {
        randomNumber = Random.Range(0, backgroundSprites.Count-1);
        backgroundImage.sprite = backgroundSprites[randomNumber];
        if (sceneToLoad == GameScene.Restart)
        {
            StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().buildIndex));
            GameManager.Instance.OnUpdateScene(GameScene.Restart);
        }
        if (sceneToLoad != GameScene.Menu)
        {
            StartCoroutine(LoadSceneAsync(GameManager.Instance.Map));
        }
        else
        {
            StartCoroutine(LoadSceneAsync(0));
        }
    }
}
