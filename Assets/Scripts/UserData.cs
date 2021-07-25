using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Userdata
{
    public int score = 0;
    public string knife_sprite = "knife_0";
    public void SaveData()
    {

    }

    public void LoadData()
    {

    }

    public UserData() { }
    public UserData(int _score, string _knife_sprite)
    {
        this.score = _score;
        this.knife_sprite = _knife_sprite;
    }

}
