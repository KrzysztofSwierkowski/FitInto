using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallBuilder : MonoBehaviour
{
    public GameObject[] WallPrefabs;

	private void Update ()
    {
        GenerateWalls();
        DestroyWalls();
	}

    private void GenerateWalls()
    {
        int positionToGenerate = EnvSettings.DistanceBetweenWalls;
        if (_generatedWalls.Any())
        {
            GameObject lastWall = _generatedWalls.Last.Value;
            int nextPosibleZ = (int)lastWall.transform.position.z + EnvSettings.DistanceBetweenWalls;
            int visibleDistance = (int)MoveController.transform.position.z + EnvSettings.DistanceBetweenWalls * EnvSettings.DistancesVisible;
            if (nextPosibleZ > visibleDistance)
            {
                return;
            }
            positionToGenerate = nextPosibleZ;
        }
        GameObject newWall = GenerateRandomWall(positionToGenerate);
        _generatedWalls.AddLast(newWall);
    }

    private void DestroyWalls()
    {
        if (_generatedWalls.Any() == false)
        {
            return;
        }
        GameObject wall = _generatedWalls.First.Value;
        if (wall.transform.position.z + EnvSettings.DistanceBetweenWalls < MoveController.transform.position.z)
        {
            GameObject.Destroy(wall);
            _generatedWalls.RemoveFirst();
        }
    }
    
    private GameObject GenerateRandomWall(int positionToGenerate)
    {
        GameObject wall = new GameObject("Wall z:" + positionToGenerate);
        wall.transform.position = new Vector3(0, 0, positionToGenerate);
        System.Random random = new System.Random((int)DateTime.UtcNow.Ticks);
        foreach(Rail rail in EnvSettings.RailsDefinitions)
        {
            GameObject segment = GameObject.Instantiate(WallPrefabs[random.Next(0, WallPrefabs.Length)], wall.transform);
            segment.transform.localPosition = new Vector3(rail.WorldPositionX, rail.WorldPositionY, 0);
        }
        return wall;
    }



    private MoveController _moveController;
    private MoveController MoveController
    {
        get
        {
            if (null == _moveController)
            {
                _moveController = GameObject.FindObjectOfType<MoveController>();
            }
            return _moveController;
        }
    }
    private EnvironmentSettings _envSettings;
    private EnvironmentSettings EnvSettings
    {
        get
        {
            if (null == _envSettings)
            {
                _envSettings = GameObject.FindObjectOfType<EnvironmentSettings>();
            }
            return _envSettings;
        }
    }

    private LinkedList<GameObject> _generatedWalls = new LinkedList<GameObject>();
}
