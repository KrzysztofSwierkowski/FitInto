using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeController : MonoBehaviour
{
    public Shape CurrentShape;

	// Use this for initialization
	void Start ()
    {
        CurrentShape = Shape.Circle;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
