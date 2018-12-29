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

    public void ResetState()
    {

        foreach (var wall in _generatedWalls.ToArray())
        {
            _generatedWalls.Remove(wall);
            GameObject.Destroy(wall.gameObject);
        }
        _schemeNumber = 0;
        ChangeScheme();
    }

    private void Start()
    {
        ResetState();
    }

    private void Update ()
    {
        if (GameEngine.Instance.Status == GameStatus.GameOver)
        {
            return;
        }
        GenerateWalls();
        DestroyWalls();
	}

    private void GenerateWalls()
    {
        int positionToGenerate = GameEngine.Instance.EnvSettings.DistanceBetweenWalls;
        if (_generatedWalls.Any())
        {
            GameObject lastWall = _generatedWalls.Last.Value;
            int nextPosibleZ = (int)lastWall.transform.position.z + GameEngine.Instance.EnvSettings.DistanceBetweenWalls;
            int visibleDistance = (int)GameEngine.Instance.MoveController.transform.position.z + GameEngine.Instance.EnvSettings.DistanceBetweenWalls * GameEngine.Instance.EnvSettings.DistancesVisible;
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
        if (wall.transform.position.z + GameEngine.Instance.EnvSettings.DistanceBetweenWalls < GameEngine.Instance.MoveController.transform.position.z)
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
        foreach(Rail rail in GameEngine.Instance.EnvSettings.RailsDefinitions)
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
        int generatedInFront = (int)((positionToGenerate - GameEngine.Instance.MoveController.transform.position.z) / GameEngine.Instance.EnvSettings.DistanceBetweenWalls);
        Shape shape = Shape.Cone;
        if (GameEngine.Instance.ShapeController.WallsToNextShape < generatedInFront)
        {
            shape = GameEngine.Instance.ShapeController.NextShape;
        }
        else
        {
            shape = GameEngine.Instance.ShapeController.CurrentShape;
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

    private LinkedList<GameObject> _generatedWalls = new LinkedList<GameObject>();
}
