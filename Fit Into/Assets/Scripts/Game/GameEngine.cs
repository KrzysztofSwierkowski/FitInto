using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    public GameStatus Status { get; private set; }

    public static GameEngine Instance { get; private set; }

    public GameSettingsManager GameSettingsManager
    {
        get
        {
            return GameObject.FindObjectOfType<GameSettingsManager>();
        }
    }

    public MoveController MoveController
    {
        get
        {
            return GameObject.FindObjectOfType<MoveController>();
        }
    }

    public PowerController PowerController
    {
        get
        {
            return GameObject.FindObjectOfType<PowerController>();
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
                    PowerController.ResetStatus();
                    GameObject.FindObjectOfType<WallBuilder>().ResetState();
                    GameObject.FindObjectOfType<BonusBuilder>().ResetState();
                    GameObject.FindObjectOfType<AudioManager>().ResetMusic();
                    break;
                }
            case GameStatus.GameOver:
                {
                    GameSettingsManager.AddNewPointsResult(PointsController.Points);
                    GameObject.FindObjectOfType<WallBuilder>().ResetState();
                    GameObject.FindObjectOfType<BonusBuilder>().ResetState();
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
