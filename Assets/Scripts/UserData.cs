using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Userdata
{
    public int score = 0;
    public string knife_sprite = "knife_0";

    public void SaveData(string path, Userdata userdata)
    {
        var temp = JsonUtility.ToJson(userdata);

        File.WriteAllText(path, temp);
    }

    public void LoadData(string path)
    {
        var file = File.ReadAllText(path);

        Userdata data = JsonUtility.FromJson<Userdata>(file);
        knife_sprite = data.knife_sprite;
        score = data.score;
    }

    public Userdata() { }
    public Userdata(int _score, string _knife_sprite)
    {
        this.score = _score;
        this.knife_sprite = _knife_sprite;
    }
}
