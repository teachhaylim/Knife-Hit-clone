using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public Text highScore;

    public void Play()
    {
        Time.timeScale = 1f;

        PlayerPrefs.SetInt("score", 0);

        AudioManager.audioManager.Play("button");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Awake()
    {
        Time.timeScale = 0f;

        highScore.text = string.Format("High Score - {0}", Userdata.LoadData(Application.dataPath + "/data/userdata.json").score.ToString());

        LevelLoader loader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();

        loader.LoadNextLevel(0);
    }

    private void Start()
    {
        AudioManager.audioManager.Play("background_music");
    }
}
