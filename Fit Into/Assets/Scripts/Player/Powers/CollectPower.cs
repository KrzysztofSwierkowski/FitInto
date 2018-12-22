using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CollectPower : PowerBase
{
    [SerializeField]
    private double _radiusPerLevel;

    [SerializeField]
    private float _moveForce;

    public void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            base.Use();
        }
    }
    
    protected override void UseIntern()
    {
        List<BonusPoint> toPull = GetPointsInRadius();
        MoveController moveController = GameObject.FindObjectOfType<MoveController>();
        foreach (var bonus in toPull)
        {
            Vector3 diff = (moveController.transform.position - bonus.transform.position);
            Rigidbody rigit = bonus.GetComponent<Rigidbody>();
            rigit.velocity = diff * _moveForce;
        }

    }

    List<BonusPoint> GetPointsInRadius()
    {
        List<BonusPoint> result = new List<BonusPoint>();
        foreach (BonusPoint point in GameObject.FindObjectsOfType<BonusPoint>())
        {
            float distanceSqr = (transform.position - point.transform.position).sqrMagnitude;
            if (distanceSqr < _radiusPerLevel * (_level + 1))
            {
                result.Add(point);
            }
        }
        return result;
    }
}
