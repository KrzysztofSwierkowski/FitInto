using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    public void OnMenu()
    {
        Debug.Log("Loading menu scene");
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void OnRestartGame()
    {

    }
}
