using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Script to control restart menu scene logic
public class RestartMenu : MonoBehaviour
{
    public Text score;

    private void Awake()
    {
        score.text = PlayerPrefs.GetInt("score").ToString();
        AudioManager.audioManager.Play("game_over");
        //AudioManager.audioManager.Stop("background_music");
    }

    public void Restart()
    {
        PlayerPrefs.SetInt("score", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void BackHome()
    {
        LevelLoader loader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        loader.LoadNextLevel(-2);
    }
}