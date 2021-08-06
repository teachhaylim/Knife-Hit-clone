using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script to control level transition logic
public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    // Load specific scene based on param index and trigger transition
    public void LoadNextLevel(int index)
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + index));
    }

    IEnumerator LoadLevel(int index)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(index);
    }
}
