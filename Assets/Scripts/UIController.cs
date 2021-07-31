using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject panelKnives;
    public GameObject knifeIcon;
    public Color knifeIconColor;
    public int knifeIconCount = 0;

    public void InitialKnifeDisplay(int max)
    {
        for(var i = 0; i < max; i++)
        {
            Instantiate(knifeIcon, panelKnives.transform);
        }
    }

    public void DecreaseKnifeDisplay()
    {
        panelKnives.transform.GetChild(knifeIconCount++).GetComponent<Image>().color = knifeIconColor;
    }
}
