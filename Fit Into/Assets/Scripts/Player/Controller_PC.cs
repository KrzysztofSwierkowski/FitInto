using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Only for debug purpose
/// </summary>
class Controller_PC : MonoBehaviour
{
    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (x != 0 || y != 0)
        {
            if (Math.Abs(x) >= Math.Abs(y))
            {
                TryJumpHorizontal(x);
            }
            else
            {
                TryJumpVertical(y);
            }
        }
    }



    private void TryJumpHorizontal(float x)
    {
        if (x < 0)
        {
            // Left
            GameEngine.Instance.MoveController.Move(UnityEngine.EventSystems.MoveDirection.Left);
        }
        else
        {
            // Right
            GameEngine.Instance.MoveController.Move(UnityEngine.EventSystems.MoveDirection.Right);
        }
    }

    private void TryJumpVertical(float y)
    {
        if (y < 0)
        {
            // down
            GameEngine.Instance.MoveController.Move(UnityEngine.EventSystems.MoveDirection.Down);
        }
        else
        {
            // up
            GameEngine.Instance.MoveController.Move(UnityEngine.EventSystems.MoveDirection.Up);
        }
    }
}
