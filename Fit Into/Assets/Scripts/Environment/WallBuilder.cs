using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallBuilder : MonoBehaviour
{
    [Serializable]
    public class WallChangeScheme
    {
        [SerializeField]
        public GameObject[] WallPrefabs;
        [SerializeField]
        public int WallsToChange;
    }
    public WallChangeScheme[] Schemes;
    
    public void DecrementWallCounter()
    {
        _wallsToChangeScheme--;
        if (_wallsToChangeScheme <= 0)
        {
            _schemeNumber++;
            ChangeScheme();
        }
    }

    private void Start()
    {
        _schemeNumber = 0;
        ChangeScheme();
    }

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
        List<GameObject> generatedWalls = new List<GameObject>();
        foreach(Rail rail in EnvSettings.RailsDefinitions)
        {
            GameObject segment = GameObject.Instantiate(_currentPrefabs[random.Next(0, _currentPrefabs.Length)], wall.transform);
            segment.transform.localPosition = new Vector3(rail.WorldPositionX, rail.WorldPositionY, 0);
            generatedWalls.Add(segment);
        }
        FixWall(wall, generatedWalls, positionToGenerate);
        return wall;
    }

    private void FixWall(GameObject wall, List<GameObject> generatedWalls, int positionToGenerate)
    {
        int generatedInFront = (int)((positionToGenerate - MoveController.transform.position.z) / EnvSettings.DistanceBetweenWalls);
        Shape shape = Shape.Cone;
        if (ShapeController.WallsToNextShape < generatedInFront)
        {
            shape = ShapeController.NextShape;
        }
        else
        {
            shape = ShapeController.CurrentShape;
        }
        if (generatedWalls.Any(x => x.GetComponent<Wall>().AcceptShape(shape)) == false)
        {
            System.Random random = new System.Random((int)DateTime.UtcNow.Ticks);
            Debug.Log("Need to fix walls.");
            GameObject oneOfWalls = generatedWalls[random.Next(0, generatedWalls.Count)];
            GameObject segment = GameObject.Instantiate(_currentPrefabs.First(x => x.GetComponent<Wall>().AcceptShape(shape)), wall.transform);
            segment.transform.localPosition = oneOfWalls.transform.localPosition;
            GameObject.Destroy(oneOfWalls);
        }
    }

    private void ChangeScheme()
    {
        if (_schemeNumber >= Schemes.Length)
        {
            _currentPrefabs = Schemes.Last().WallPrefabs;
            _wallsToChangeScheme = Schemes.Last().WallsToChange;
        }
        else
        {
            _currentPrefabs = Schemes[_schemeNumber].WallPrefabs;
            _wallsToChangeScheme = Schemes[_schemeNumber].WallsToChange;
        }
    }



    private int _schemeNumber;
    private int _wallsToChangeScheme;
    private GameObject[] _currentPrefabs;
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
    private ShapeController _shapeController;
    private ShapeController ShapeController
    {
        get
        {
            if (null == _shapeController)
            {
                _shapeController = GameObject.FindObjectOfType<ShapeController>();
            }
            return _shapeController;
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
