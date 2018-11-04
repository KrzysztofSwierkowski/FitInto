using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallBuilder : MonoBehaviour
{
    public GameObject[] WallPrefabs;
    public int WallsInFrontOfPlayer = 5;
    public int DistanceBetweenWalls = 20;

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

    private LinkedList<GameObject> _generatedWalls = new LinkedList<GameObject>();

	private void Update ()
    {
        GenerateWalls();
        DestroyWalls();
	}

    private void GenerateWalls()
    {
        int visibleDistance = (int)MoveController.transform.position.z + DistanceBetweenWalls * WallsInFrontOfPlayer;
        int positionToGenerate = DistanceBetweenWalls;
        if (_generatedWalls.Any())
        {
            GameObject lastWall = _generatedWalls.Last.Value;
            int nextPosibleZ = (int)lastWall.transform.position.z + DistanceBetweenWalls;
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
        if (wall.transform.position.z + DistanceBetweenWalls < MoveController.transform.position.z)
        {
            GameObject.Destroy(wall);
            _generatedWalls.RemoveFirst();
        }
    }

    // TODO PB: To do jakiejś fabryki pójdzie
    private GameObject GenerateRandomWall(int positionToGenerate)
    {
        /* z 9 segmentów o pozycjach:
         * (-3,3)  (0,3)  (3,3)
         * (-3,0)  (0,0)  (0,3)
         * (-3,-3) (0,-3) (-3,3)
         */
        GameObject wall = new GameObject("Wall z:" + positionToGenerate);
        wall.transform.position = new Vector3(0, 0, positionToGenerate);
        System.Random random = new System.Random((int)DateTime.UtcNow.Ticks);
        foreach (int x in new int[] { -3, 0, 3 })
        {
            foreach (int y in new int[] { -3, 0, 3 })
            {
                GameObject segment = GameObject.Instantiate(WallPrefabs[random.Next(0, WallPrefabs.Length)], wall.transform);
                segment.transform.localPosition = new Vector3(x, y, 0);
            }
        }
        return wall;
    }
}
