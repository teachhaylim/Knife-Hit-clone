using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameController : MonoBehaviour
{
    // Core Gameplay variables
    public static GameController instance;
    public Vector2 spawnLocation;
    public Transform[] targets;
    public GameObject currentGameObject, playerObject;
    [HideInInspector]
    public int knifeCount;

    private int knifeIconCount = 0, currentScore = 0;

    //UI variables
    public GameObject panelKnives, knifeIcon;
    public Text scoreText;
    public Color knifeIconColor;

    // Logic userdata (unused)
    //private Userdata user;
    //private string path;

    // Set variabls value and core logic
    private void Awake()
    {
        instance = this;
        knifeCount = new System.Random().Next(5, 7);
        currentGameObject = Instantiate(targets[new System.Random().Next(0, targets.Length)].gameObject, new Vector2(0, 3), Quaternion.identity);
        InitialKnifeDisplay();
        SpawnKnives();

        // Logic for save and load userdata (unused)
        //path = Application.dataPath + "/data/userdata.json";
        //user = Userdata.LoadData(path);
    }

    #region Core Gameplay methods
    public void CheckKnifeHit()
    {
        currentScore++;
        scoreText.text = currentScore.ToString();

        if (knifeCount > 0)
        {
            SpawnKnives();
        }
        else
        {
            SetGameOver(true);
        }
    }

    public void SetGameOver(bool option)
    {
        StartCoroutine(GameOverScreen(option));
    }

    public void RestartGame()
    {
        Destroy(currentGameObject);

        currentGameObject = Instantiate(targets[new System.Random().Next(0, targets.Length)].gameObject, new Vector2(0, 3), Quaternion.identity);
        knifeCount = new System.Random().Next(5, 7);
        InitialKnifeDisplay();
        SpawnKnives();
    }

    private void SpawnKnives()
    {
        knifeCount--;
        Instantiate(playerObject, spawnLocation, Quaternion.identity);
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
            // Load restart scene
            LevelLoader loader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
            loader.LoadNextLevel(1);

            // Set score for restart scene
            PlayerPrefs.SetInt("score", currentScore);

            // Check and set new high score
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
    #endregion

    #region UI methods
    public void InitialKnifeDisplay()
    {
        knifeIconCount = knifeCount - 1;

        foreach (Transform child in panelKnives.transform)
        {
            Destroy(child.gameObject);
        }

        for (var i = 0; i < knifeCount; i++)
        {
            Instantiate(knifeIcon, panelKnives.transform);
        }
    }

    public void DecreaseKnifeDisplay()
    {
        panelKnives.transform.GetChild(knifeIconCount--).GetComponent<Image>().color = knifeIconColor;
    }
    #endregion

    //User data class (unused)
    public class Userdata
    {
        public int score = 0;
        public string knife_sprite = "knife_0";

        public void SaveData(string path, Userdata userdata)
        {
            var temp = JsonUtility.ToJson(userdata);

            File.WriteAllText(path, temp);
        }

        public static Userdata LoadData(string path)
        {
            //var file = File.ReadAllText(path);

            //Userdata data = JsonUtility.FromJson<Userdata>(File.ReadAllText(path));
            //knife_sprite = data.knife_sprite;
            //score = data.score;

            return JsonUtility.FromJson<Userdata>(File.ReadAllText(path));
        }

        public Userdata() { }
        public Userdata(int _score, string _knife_sprite)
        {
            this.score = _score;
            this.knife_sprite = _knife_sprite;
        }
    }
}