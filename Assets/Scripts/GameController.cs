using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UIController))]
public class GameController : MonoBehaviour
{
    public static GameController instance { get; set; }
    [SerializeField]
    private int knifeCount;
    [SerializeField]
    private Vector2 spawnLocation;
    [SerializeField]
    private GameObject knifeObject;
    public UIController uiController { get; set; }

    private void Awake()
    {
        instance = this;
        uiController = GetComponent<UIController>();
    }

    private void Start()
    {
        uiController.InitialKnifeDisplay(knifeCount);
        SpawnKnives();
    }

    public void CheckKnifeHit()
    {
        if(knifeCount > 0)
        {
            SpawnKnives();
        }
        else
        {
            SetGameOver(true);
        }
    }

    private void SpawnKnives()
    {
        knifeCount--;
        Instantiate(knifeObject, spawnLocation, Quaternion.identity);
    }

    public void SetGameOver(bool option)
    {
        StartCoroutine("GameOverScreen", option);
    }

    private IEnumerator GameOverScreen(bool option)
    {
        if (option)
        {
            yield return new WaitForSecondsRealtime(0.3f);

            RestartGame();
        }
        else
        {
            Debug.Log("Show Restart BTn");
            uiController.ShowRestartButton();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}