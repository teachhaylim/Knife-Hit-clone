using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject panelKnives;
    public GameObject knifeIcon;
    public Text scoreText;
    public Color knifeIconColor;
    private int knifeIconCount = 0;

    public void InitialKnifeDisplay(int max)
    {
        knifeIconCount = max - 1;

        foreach (Transform child in panelKnives.transform)
        {
            Destroy(child.gameObject);
        }

        for (var i = 0; i < max; i++)
        {
            Instantiate(knifeIcon, panelKnives.transform);
        }
    }

    public void DecreaseKnifeDisplay()
    {
        panelKnives.transform.GetChild(knifeIconCount--).GetComponent<Image>().color = knifeIconColor;
    }
}
