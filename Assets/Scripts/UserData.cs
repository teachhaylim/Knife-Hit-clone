using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Userdata
{
    public int score = 0;
    public string knife_sprite = "knife_0";
    public string path;

    public void SaveData()
    {
        //Semi-Done write userdat object to json
        //TODO save data based on user choice
        //FIXME unity throw warning

        Userdata userdata = new Userdata();
        userdata.score = 10;
        userdata.knife_sprite = "knife_23";

        var temp = JsonUtility.ToJson(userdata);

        File.WriteAllText(path, temp);
    }

    public void LoadData()
    {
        //DONE read json object back, -> assign result to correct fields

        var file = File.ReadAllText(path);

        Userdata data = JsonUtility.FromJson<Userdata>(file);
        knife_sprite = data.knife_sprite;
        score = data.score;

        Debug.Log(data.score);
        Debug.Log(data.knife_sprite);
    }

    public Userdata() { }
    public Userdata(int _score, string _knife_sprite)
    {
        this.score = _score;
        this.knife_sprite = _knife_sprite;
    }

}
