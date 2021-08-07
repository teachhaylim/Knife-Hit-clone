using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Script to control start menu scene logic
public class StartMenu : MonoBehaviour
{
    public Text highScore;
    public Image sprite;
    private int spriteIndex = 0;

    private void Awake()
    {
        Time.timeScale = 0f;

        if (PlayerPrefs.GetString("player_sprite") == "")
        {
            PlayerPrefs.SetString("player_sprite", "Player/knife_0");
        }

        sprite.sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("player_sprite")); //load player (knife) sprite based on user saved sprite
        highScore.text = string.Format("High Score - {0}", PlayerPrefs.GetInt("highScore").ToString());  //load user's high score

        //AudioManager.audioManager.Play("background_music");
        //highScore.text = string.Format("High Score - {0}", Userdata.LoadData(Application.dataPath + "/data/userdata.json").score.ToString());

        LevelLoader loader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        loader.LoadNextLevel(0);
    }

    public void ChangePlayerSprite()
    {
        if (spriteIndex >= 9)
        {
            spriteIndex = 0;
        }

        sprite.sprite = Resources.Load<Sprite>(string.Format("Player/knife_{0}", spriteIndex));
        PlayerPrefs.SetString("player_sprite", string.Format("Player/knife_{0}", spriteIndex));
        spriteIndex++;
    }

    public void Play()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("score", 0);
        AudioManager.audioManager.Play("button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
