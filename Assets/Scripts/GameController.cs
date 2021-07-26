using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance { get; set; }
    public int knifeCount;
    public Vector2 spawnLocation;
    public GameObject knifeObject;
    public UIController uiController;
    public Userdata user;

    private void Awake()
    {
        instance = this;
        uiController = GetComponent<UIController>();
        knifeCount = new System.Random().Next(4, 10);

        //TODO rework userdata
        //user = new Userdata();
        //user.SaveData();
        //user.LoadData();
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
            uiController.ShowRestartMenu();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}