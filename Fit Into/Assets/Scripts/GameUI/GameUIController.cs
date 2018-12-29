using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public GameObject GameOverMenu;
    public GameObject HUB;
    

    public void SetStatus(GameStatus gameStatus)
    {
        switch (gameStatus)
        {
            case GameStatus.Running:
                {
                    GameOverMenu.SetActive(false);
                    HUB.SetActive(true);
                    break;
                }
            case GameStatus.GameOver:
                {
                    GameOverMenu.SetActive(true);
                    HUB.SetActive(false);
                    break;
                }
            default:
                break;
        }
    }
}
