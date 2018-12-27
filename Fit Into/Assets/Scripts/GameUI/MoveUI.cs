using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveUI : MonoBehaviour
{

    public void OnMove(int iMoveDirection)
    {
        MoveDirection moveDirection = (MoveDirection)iMoveDirection;
    }
}
