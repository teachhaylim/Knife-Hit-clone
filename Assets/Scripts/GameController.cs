using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UIController))]
public class GameController : MonoBehaviour
{
    public static GameController instance { get; set; }
    public int knifeCount;
    public Vector2 spawnLocation;
    public GameObject knifeObject;
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

public class UserData
{
    public int score = 0;
    public string knife_sprite = "knife_0";

    public void SaveData()
    {

    }

    public void LoadData()
    {

    }

    public UserData() { }
    public UserData(int _score, string _knife_sprite)
    {
        this.score = _score;
        this.knife_sprite = _knife_sprite;
    }
}