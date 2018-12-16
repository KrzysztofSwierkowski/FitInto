using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Shape[] AcceptableShapes;

    public bool AcceptShape(Shape shape)
    {
        return AcceptableShapes.Contains(shape);
    }
}
