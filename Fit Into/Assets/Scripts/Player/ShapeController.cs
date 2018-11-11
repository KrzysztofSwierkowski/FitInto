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

	// Use this for initialization
	void Start ()
    {
        _schemeNumber = 0;
        ChangeShape();
    }


    private void ChangeShape()
    {
        ShapeChangeScheme scheme = null;
        if (_schemeNumber >= Schemes.Length)
        {
            scheme = Schemes.Last();
        }
        else
        {
            scheme = Schemes[_schemeNumber];
        }
        WallsToNextShape = scheme.WallsToChange;
        System.Random random = new System.Random((int)DateTime.UtcNow.Ticks);
        ShapeMap shape = scheme.PossibleShapes[random.Next(0, scheme.PossibleShapes.Length)];
        CurrentShape = shape.Shape;
        foreach(var sch in Schemes)
        {
            foreach(var obj in sch.PossibleShapes.Select(x => x.Object))
            {
                obj.SetActive(false);
            }
        }
        shape.Object.SetActive(true);
    }
}
