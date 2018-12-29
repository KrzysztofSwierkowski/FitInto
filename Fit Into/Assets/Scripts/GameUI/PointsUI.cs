using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsUI : MonoBehaviour
{
    private PointsController _pointsController;
	
	// Update is called once per frame
	void Update ()
    {
        if (_pointsController == null)
        {
            _pointsController = GameObject.FindObjectOfType<PointsController>();
        }
        GetComponent<Text>().text = GameEngine.Instance.PointsController.Points.ToString();
	}
}
