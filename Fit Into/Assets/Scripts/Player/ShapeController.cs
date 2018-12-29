using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShapeController : MonoBehaviour
{
    [Serializable]
    public class ShapeMap
    {
        [SerializeField]
        public Shape Shape;
        [SerializeField]
        public GameObject Object;
    }

    [Serializable]
    public class ShapeChangeScheme
    {
        [SerializeField]
        public ShapeMap[] PossibleShapes;
        [SerializeField]
        public int WallsToChange;
    }

    public ShapeChangeScheme[] Schemes;
    public Shape CurrentShape { get; private set; }
    public Shape NextShape
    {
        get
        {
            return NextShapeMap.Shape;
        }
    }
    public ShapeMap NextShapeMap { get; private set; }

    public int WallsToNextShape { get; private set; }
    private int _schemeNumber;

    public void DecrementWallCounter()
    {
        WallsToNextShape--;
        if (WallsToNextShape <= 0)
        {
            ++_schemeNumber;
            ChangeShape();
        }
    }

    public void ResetState()
    {
        _schemeNumber = 0;
        ChooseNextShape(_schemeNumber);
        ChangeShape();
    }
    
    void Start ()
    {
        ResetState();
    }


    private void ChangeShape()
    {
        ShapeMap shape = NextShapeMap;
        CurrentShape = shape.Shape;
        foreach(var sch in Schemes)
        {
            foreach(var obj in sch.PossibleShapes.Select(x => x.Object))
            {
                obj.SetActive(false);
            }
        }
        shape.Object.SetActive(true);
        ChooseNextShape(_schemeNumber + 1);
    }

    private void ChooseNextShape(int shemeNumber)
    {
        ShapeChangeScheme scheme = null;
        if (shemeNumber >= Schemes.Length)
        {
            scheme = Schemes.Last();
        }
        else
        {
            scheme = Schemes[shemeNumber];
        }
        WallsToNextShape = scheme.WallsToChange;
        System.Random random = new System.Random((int)DateTime.UtcNow.Ticks);
        NextShapeMap = scheme.PossibleShapes[random.Next(0, scheme.PossibleShapes.Length)];
    }
}
