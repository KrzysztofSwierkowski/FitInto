using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField]
    private Text _textCurrentScore;

    [SerializeField]
    private Text _textBestScore;

    public void OnMenu()
    {
        Debug.Log("Loading menu scene");
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
    
    public void OnRestartGame()
    {
        GameEngine.Instance.SetGameStatus(GameStatus.Running);
    }

    private void OnEnable()
    {
        _textCurrentScore.text = GameEngine.Instance.PointsController.Points.ToString();
        _textBestScore.text = GameEngine.Instance.GameSettingsManager.Load().Stats.Max().ToString();
    }
}
