using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenager : MonoBehaviour
{
    public static LevelMenager Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    public void CompleteLevel()
    {
        int next_scene_index = SceneManager.GetActiveScene().buildIndex + 1;
        if (next_scene_index < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(next_scene_index);
        else 
            Debug.Log("You have won the game");
    }

    public void LevelSelectMenu(int sceneIndex)
    {
        if (sceneIndex < 0 || sceneIndex >= SceneManager.sceneCountInBuildSettings)
            return;
        SceneManager.LoadScene(sceneIndex);
    }
}
