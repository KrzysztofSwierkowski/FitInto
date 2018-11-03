using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {

    public float Speed = 2f;
    public float Accelerate = 5;
    public float Bounds = 1.5f;

    DateTime _started;

    private void Start()
    {
        _started = DateTime.UtcNow;
    }

    void FixedUpdate ()
    {
        if (GetComponent<CollisionController>().IsGameOver)
        {
            return;
        }
        Vector3 newPosition = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Accelerate * Time.deltaTime * (float)(DateTime.UtcNow - _started).TotalSeconds / 2 );
        newPosition = newPosition * Speed * Time.deltaTime;
        Vector3 dest = transform.position + newPosition;
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
        transform.position = new Vector3(x,y,dest.z);
    }
}
