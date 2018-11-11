using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapePrinter : MonoBehaviour
{

    private ShapeController _shapeController;

    // Update is called once per frame
    void Update()
    {
        if (_shapeController == null)
        {
            _shapeController = GameObject.FindObjectOfType<ShapeController>();
        }
        GetComponent<Text>().text = "Next shape after " + _shapeController.WallsToNextShape + " walls";

    }
}
