using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveController : MonoBehaviour {

    public float BasicSpeed = 10f;
    public float SpeedToTurnMultipler = 0.5f;
    public int ThresholdSpeedSec = 30;
    public float[] SpeedMultiplers;
    public float Bounds = 1.5f;

    private DateTime _startTime;

    private void Start()
    {
        _startTime = DateTime.UtcNow;
    }

    void FixedUpdate ()
    {
        if (GetComponent<CollisionController>().IsGameOver)
        {
            return;
        }
        transform.position = GetNewPosition();
    }
    
    private Vector3 GetNewPosition()
    {
        float speedMultipler = GetSpeedMultipler();
        Vector3 newPosition = new Vector3(
            Input.GetAxis("Horizontal") * speedMultipler * SpeedToTurnMultipler, 
            Input.GetAxis("Vertical") * speedMultipler * SpeedToTurnMultipler, 
            BasicSpeed * speedMultipler * Time.fixedDeltaTime);
        Vector3 dest = transform.position + newPosition;
        return BoundsVector(dest);
    }

    private float GetSpeedMultipler()
    {
        int totalSec = (int)(DateTime.UtcNow - _startTime).TotalSeconds;
        int threshold = totalSec / ThresholdSpeedSec;
        if (threshold < SpeedMultiplers.Length)
        {
            return SpeedMultiplers[threshold];
        }
        return SpeedMultiplers.Last();
    }

    private Vector3 BoundsVector(Vector3 dest)
    {
        float x = dest.x;
        float y = dest.y;
        if (x > 0)
        {
            x = Math.Min(Bounds, x);
        }
        else
        {
            x = Math.Max(-Bounds, x);
        }
        if (y > 0)
        {
            y = Math.Min(Bounds, y);
        }
        else
        {
            y = Math.Max(-Bounds, y);
        }
        return new Vector3(x, y, dest.z);
    }
}
