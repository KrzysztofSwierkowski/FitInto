using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public bool IsGameOver;
    
    private void OnTriggerEnter(Collider other)
    {
        if (ProceedBonusPointEnter(other))
        {
            return;
        }
        if (ProceedWallEnter(other))
        {
            return;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (ProceedWallExit(other))
        {
            return;
        }
    }

    private bool ProceedWallEnter(Collider other)
    {
        Wall wall = other.GetComponent<Wall>();
        if (wall != null)
        {
            Shape playerShape = GetComponent<ShapeController>().CurrentShape;
            if( wall.AcceptableShapes.Any(x => x == playerShape) == false)
            {
                GameOver();
            }
            return true;
        }
        return false;
    }

    private bool ProceedWallExit(Collider other)
    {
        Wall wall = other.GetComponent<Wall>();
        if (wall != null)
        {
            GetComponent<ShapeController>().DecrementWallCounter();
            GameObject.FindObjectOfType<WallBuilder>().DecrementWallCounter();
            return true;
        }
        return false;
    }

    private bool ProceedBonusPointEnter(Collider other)
    {
        BonusPoint bonus = other.GetComponent<BonusPoint>();
        if (bonus != null)
        {
            GetComponent<PointsController>().AddPoints(bonus.Points);
            GameObject.Destroy(bonus.gameObject);
            return true;
        }
        return false;
    }

    private void GameOver()
    {
        IsGameOver = true;
        GameObject.FindObjectOfType<PlayerStatisticsPoints>().AddResult(GetComponent<PointsController>().Points);
    }
}
