using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    public GameStatus Status { get; private set; }

    public static GameEngine Instance { get; private set; }

    public PlayerStatisticsPoints Stats
    {
        get
        {
            return GameObject.FindObjectOfType<PlayerStatisticsPoints>();
        }
    }

    public MoveController MoveController
    {
        get
        {
            return GameObject.FindObjectOfType<MoveController>();
        }
    }

    public ShapeController ShapeController
    {
        get
        {
            return GameObject.FindObjectOfType<ShapeController>();
        }
    }

    public PointsController PointsController
    {
        get
        {
            return GameObject.FindObjectOfType<PointsController>();
        }
    }

    public EnvironmentSettings EnvSettings
    {
        get
        {
            return GameObject.FindObjectOfType<EnvironmentSettings>();
        }
    }


    public void SetGameStatus(GameStatus gameStatus)
    {
        switch (gameStatus)
        {
            case GameStatus.Running:
                {
                    MoveController.ResetState();
                    ShapeController.ResetState();
                    PointsController.ResetState();
                    GameObject.FindObjectOfType<WallBuilder>().ResetState();
                    GameObject.FindObjectOfType<BonusBuilder>().ResetState();
                    break;
                }
            case GameStatus.GameOver:
                {
                    Stats.AddResult(PointsController.Points);
                    break;
                }
            default:
                break;
        }
        Status = gameStatus;
        GameObject.FindObjectOfType<GameUIController>().SetStatus(gameStatus);
    }

    private void Awake()
    {
        Instance = this;
    }
}
