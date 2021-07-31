using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenu : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void BackHome()
    {
        LevelLoader loader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        loader.LoadNextLevel(-2);
    }
}