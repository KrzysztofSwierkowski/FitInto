﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PointsController : MonoBehaviour
{

    public int Points { get; private set; }
    public int ThresholdSec = 30;
    public int[] PointsPerSecond;

    private float _pointsDelta;
    private DateTime _startTime;
    
    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        _startTime = DateTime.UtcNow;
        Points = 0;
    }
    
    public void AddPoints(int points)
    {
        Points += points;
    }

    private void Update ()
    {
        if (GameEngine.Instance.Status == GameStatus.GameOver)
        {
            return;
        }
        _pointsDelta += Time.deltaTime * GetMultipler();
        if (_pointsDelta > 1)
        {
            AddPoints((int)_pointsDelta);
            _pointsDelta -= (int)_pointsDelta;
        }
    }

    private int GetMultipler()
    {
        int totalSec = (int)(DateTime.UtcNow - _startTime).TotalSeconds;
        int threshold = totalSec / ThresholdSec;
        if (threshold < PointsPerSecond.Length)
        {
            return PointsPerSecond[threshold];
        }
        return PointsPerSecond.Last();

    }
}
