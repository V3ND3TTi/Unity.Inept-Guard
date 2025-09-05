using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utilties
{
    public static int PlayerDeaths = 0;

    public static string UpdateDeathCount(ref int countReference)
    {
        countReference += 1;
        return $"Next time you'll be at death #{countReference}.";
    }

    public static void RestartLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    public static bool RestartLevel(int sceneIndex)
    {
        if (sceneIndex < 0)
        {
            throw new ArgumentException("Scene index cannot be negative");
        }

        //Debug.Log($"Player deaths: {PlayerDeaths}");
        //string message = UpdateDeathCount(ref PlayerDeaths);
        //Debug.Log($"Player deaths: {PlayerDeaths}");
        //Debug.Log(message);

        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1.0f;

        return true;
    }
}
