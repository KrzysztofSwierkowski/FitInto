using System;
using System.Collections;
using System.Collections.Generic;
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

    public Shape CurrentShape;
    public Shape NextShape;

    public float TimeToChange;
    public float TimeLeftToChange;

    public ShapeMap[] ShapeObjects;

	// Use this for initialization
	void Start ()
    {
        CurrentShape = Shape.Sphere;
        TimeLeftToChange = TimeToChange;
    }
	
	// Update is called once per frame
	void Update ()
    {
        TimeLeftToChange -= Time.deltaTime;
        if (TimeLeftToChange <= 0)
        {
            ChangeShape();
        }
	}

    private void ChangeShape()
    {
        TimeLeftToChange = TimeToChange;
        CurrentShape = NextShape;
        System.Random random = new System.Random((int)DateTime.UtcNow.Ticks);
        List<Shape> allShapes = new List<Shape>();
        foreach(Shape shape in Enum.GetValues(typeof(Shape)))
        {
            allShapes.Add(shape);
        }
        allShapes.Remove(CurrentShape);
        NextShape = allShapes[random.Next(0, allShapes.Count)];
        foreach(var shapeMap in ShapeObjects)
        {
            shapeMap.Object.SetActive(shapeMap.Shape == CurrentShape);
        }
    }
}
