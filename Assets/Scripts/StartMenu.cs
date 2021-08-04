using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public Text highScore;
    public Image sprite;
    private int spriteIndex = 0;

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

        sprite.sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("player_sprite"));

        //highScore.text = string.Format("High Score - {0}", Userdata.LoadData(Application.dataPath + "/data/userdata.json").score.ToString());
        highScore.text = string.Format("High Score - {0}", PlayerPrefs.GetInt("highScore").ToString());

        //var temp = Resources.Load<TextAsset>("Data/userdata");
        //Debug.Log(JsonUtility.FromJson<Userdata>(temp.text).score);

        //LevelLoader loader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();

        //loader.LoadNextLevel(0);
    }

    public void ChangePlayerSprite()
    {
        if(spriteIndex >= 9)
        {
            spriteIndex = 0;
        }

        sprite.sprite = Resources.Load<Sprite>(string.Format("Player/knife_{0}", spriteIndex));

        PlayerPrefs.SetString("player_sprite", string.Format("Player/knife_{0}", spriteIndex));

        spriteIndex++;
    }

    private void Start()
    {
        AudioManager.audioManager.Play("background_music");
    }
}
