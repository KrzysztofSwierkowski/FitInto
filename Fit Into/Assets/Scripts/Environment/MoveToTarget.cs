using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    private Transform _target;
    private Vector3 _startPosition;
    private float _timeToReach;
    private float _elapsedTime;

    public void Init(Transform target, float time)
    {
        _timeToReach = time;
        _elapsedTime = 0f;
        _target = target;
        _startPosition = this.transform.position;
    }
    
    private void Update()
    {
        if (_target == null)
        {
            return;
        }
        _elapsedTime += Time.deltaTime / _timeToReach;
        transform.position = Vector3.Lerp(_startPosition, _target.position, _elapsedTime);
    }
}
