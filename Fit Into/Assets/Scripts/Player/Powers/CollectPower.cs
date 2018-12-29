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
    private float _timeToPlayer = 0.5f;
    
    public void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            base.Use();
        }
        base.UpdateFrame();
    }
    
    protected override void UseIntern()
    {
        MoveController moveController = GameObject.FindObjectOfType<MoveController>();
        foreach(var point in GetPointsInRadius())
        {
            MoveToTarget moveToTarget = point.gameObject.AddComponent<MoveToTarget>();
            moveToTarget.Init(moveController.transform, _timeToPlayer);
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
