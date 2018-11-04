using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public bool IsGameOver;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Wall>() != null)
        {
            IsGameOver = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        BonusPoint bonus = other.GetComponent<BonusPoint>();
        if (bonus != null)
        {
            GetComponent<PointsController>().AddPoints(bonus.Points);
            GameObject.Destroy(bonus.gameObject);
            return;
        }
    }

}
