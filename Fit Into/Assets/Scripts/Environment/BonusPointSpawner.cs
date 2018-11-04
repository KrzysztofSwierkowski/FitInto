using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BonusPointSpawner : MonoBehaviour
{
    public int BonusPerDistance;
    public GameObject[] BonusPointPrefabs;
    public int DistancesInFrontOfPlayer = 5;
    public int GapToWall = 3;
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

    private List<GameObject> _generatedPoints = new List<GameObject>();

    private void Update()
    {
        GenerateBonuses();
        DestroyBonuses();
    }

    private void GenerateBonuses()
    {
        int visibleDistance = CalculateDistance((int)MoveController.transform.position.z) + DistancesInFrontOfPlayer;
        int distanceNumber = 0;
        if (_generatedPoints.Any())
        {
            GameObject lastWall = _generatedPoints.Last();
            int newDistanceNumber = CalculateDistance((int)lastWall.transform.position.z) + 1;
            if (newDistanceNumber > visibleDistance)
            {
                return;
            }
            distanceNumber = newDistanceNumber;
        }
        GeneratePoints(distanceNumber);
    }

    private void DestroyBonuses()
    {
        int playerDistance = CalculateDistance((int)MoveController.transform.position.z);
        foreach(var pointBehind in _generatedPoints.Where(x => CalculateDistance((int)x.transform.position.z) < playerDistance).ToArray())
        {
            _generatedPoints.Remove(pointBehind);
            GameObject.Destroy(pointBehind.gameObject);
        }
    }
    
    private void GeneratePoints(int distanceNumber)
    {
        System.Random random = new System.Random((int)DateTime.UtcNow.Ticks);
        GameObject pointsParent = new GameObject(string.Format("BonusPoint '{0}'", distanceNumber));
        pointsParent.transform.position = new Vector3(0, 0, distanceNumber * DistanceBetweenWalls);
        for (int i = 0; i< BonusPerDistance;++i)
        {
            GameObject bonusPoint = GameObject.Instantiate(BonusPointPrefabs[random.Next(0, BonusPointPrefabs.Length)]);
            bonusPoint.transform.parent = pointsParent.transform;
            bonusPoint.transform.localPosition = new Vector3(
                (float)random.NextDouble() * 6 - 3,
                (float)random.NextDouble() * 6 - 3,
                random.Next(GapToWall, DistanceBetweenWalls - GapToWall)
                );
        }
        _generatedPoints.Add(pointsParent);
    }

    private int CalculateDistance(int zPosition)
    {
        return zPosition / DistanceBetweenWalls;
    }
}
