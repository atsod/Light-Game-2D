using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static int currentSceneIndex;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public static void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public static void ReloadScene()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public static void GoToPreviousScene()
    {
        SceneManager.LoadScene(currentSceneIndex - 1);
    }
}
