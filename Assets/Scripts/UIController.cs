using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject restartButton;

    [SerializeField]
    private GameObject panelKnives;
    [SerializeField]
    private GameObject knifeIcon;
    [SerializeField]
    private Color knifeIconColor;
    private int knifeIconCount = 0;
    
    public void ShowRestartButton()
    {
        restartButton.SetActive(true);
    }

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
