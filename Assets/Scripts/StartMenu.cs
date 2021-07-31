using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void Play()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Awake()
    {
        Time.timeScale = 0f;

        LevelLoader loader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();

        loader.LoadNextLevel(0);
    }
}
