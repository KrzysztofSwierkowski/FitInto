using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionController : MonoBehaviour
{

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
                GameEngine.Instance.SetGameStatus(GameStatus.GameOver);
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
            FindObjectOfType<AudioManager>().Play("WallEntry");
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
            Debug.Log("Collecting bonus " + bonus.Points);
            GameEngine.Instance.PointsController.AddPoints(bonus.Points);
            GameObject.Destroy(bonus.gameObject);
            return true;
        }
        return false;
    }
}
