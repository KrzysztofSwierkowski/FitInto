using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public bool IsGameOver;
    
    private void OnTriggerEnter(Collider other)
    {
        if (ProceedBonusPoint(other))
        {
            return;
        }
        if (ProceedWall(other))
        {
            return;
        }
    }

    private bool ProceedWall(Collider other)
    {
        Wall wall = other.GetComponent<Wall>();
        if (wall != null)
        {
            Shape playerShape = GetComponent<ShapeController>().CurrentShape;
            if( wall.AcceptableShapes.Any(x => x == playerShape) == false)
            {
                IsGameOver = true;
            }
            return true;
        }
        return false;
    }

    private bool ProceedBonusPoint(Collider other)
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
}
