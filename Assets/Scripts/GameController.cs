using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance { get; set; }
    public Vector2 spawnLocation;
    public Transform[] targets;
    public GameObject currentGameObject;
    public GameObject knifeObject;

    [HideInInspector]
    public int knifeCount;
    [HideInInspector]
    public UIController uiController;

    private Userdata user;
    private int currentScore = 0;
    private string path;

    private void Awake()
    {
        instance = this;
        uiController = GetComponent<UIController>();
        knifeCount = new System.Random().Next(5, 7);

        //path = Application.dataPath + "/data/userdata.json";
        //user = Userdata.LoadData(path);

        //if(PlayerPrefs.GetInt("score") == 0)
        //{
        //    currentScore = 0;
        //    uiController.scoreText.text = currentScore.ToString();
        //}
        //else
        //{
        //    currentScore = PlayerPrefs.GetInt("score");
        //    uiController.scoreText.text = currentScore.ToString();
        //}
    }

    private void Start()
    {
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

            PlayerPrefs.SetInt("score", currentScore);

            if(PlayerPrefs.GetInt("highScore") < currentScore)
            {
                PlayerPrefs.SetInt("highScore", currentScore);
            }

            //if (user.score < currentScore)
            //{
            //    user.score = currentScore;
            //    user.SaveData(path, user);
            //}
        }
    }
    
    public void RestartGame()
    {
        Destroy(currentGameObject);

        currentGameObject = Instantiate(targets[new System.Random().Next(0, targets.Length)].gameObject, new Vector2(0, 3), Quaternion.identity);
        knifeCount = new System.Random().Next(5, 7);
        uiController.InitialKnifeDisplay(knifeCount);
        SpawnKnives();
    }
}