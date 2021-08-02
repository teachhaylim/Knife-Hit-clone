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
    public Transform[] targets;
    public GameObject currentGameObject;
    private int currentScore = 0;
    private string path;

    private void Awake()
    {
        instance = this;
        uiController = GetComponent<UIController>();
        knifeCount = new System.Random().Next(5, 7);
        path = Application.dataPath + "/data/userdata.json";
        user = Userdata.LoadData(path);

        if(PlayerPrefs.GetInt("score") == 0)
        {
            currentScore = 0;
            uiController.scoreText.text = currentScore.ToString();
        }
        else
        {
            currentScore = PlayerPrefs.GetInt("score");
            uiController.scoreText.text = currentScore.ToString();
        }
    }

    private void Start()
    {
        //Dirty Technique
        currentGameObject = Instantiate(targets[new System.Random().Next(0, targets.Length)].gameObject, new Vector2(0, 3), Quaternion.identity);

        uiController.InitialKnifeDisplay(knifeCount);
        SpawnKnives();
    }

    public void CheckKnifeHit()
    {
        currentScore++;
        uiController.scoreText.text = currentScore.ToString();

        if (knifeCount > 0)
        {
            SpawnKnives();
        }
        else
        {
            //TODO randomly clone new target object;

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
        StartCoroutine(GameOverScreen(option));
    }

    private IEnumerator GameOverScreen(bool option)
    {
        PlayerPrefs.SetInt("score", currentScore);

        if (option)
        {
            yield return new WaitForSecondsRealtime(0.3f);

            RestartGame();
        }
        else
        {
            LevelLoader loader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
            loader.LoadNextLevel(1);

            if (user.score < currentScore)
            {
                user.score = currentScore;
                user.SaveData(path, user);
            }
        }
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}