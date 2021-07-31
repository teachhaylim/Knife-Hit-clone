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

    private void Awake()
    {
        instance = this;
        uiController = GetComponent<UIController>();
        knifeCount = new System.Random().Next(4, 5);
        user.path = Application.dataPath + "/data/userdata.json";

        //TODO rework userdata
        //user = new Userdata();
        //user.SaveData();
        //user.LoadData();
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
        if(knifeCount > 0)
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
        if (option)
        {
            yield return new WaitForSecondsRealtime(0.3f);

            RestartGame();
        }
        else
        {
            LevelLoader loader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
            loader.LoadNextLevel(1);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}