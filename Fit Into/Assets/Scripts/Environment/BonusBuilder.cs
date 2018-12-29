using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BonusBuilder : MonoBehaviour
{
    public int BonusPerDistance;
    public GameObject[] BonusPointPrefabs;
    public int GapToWall = 3;

    public void ResetState()
    {
        foreach (var point in _generatedPoints.ToArray())
        {
            _generatedPoints.Remove(point);
            GameObject.Destroy(point.gameObject);
        }
    }

    private void Update()
    {
        if (GameEngine.Instance.Status == GameStatus.GameOver)
        {
            return;
        }
        GenerateBonuses();
        DestroyBonuses();
    }

    private void GenerateBonuses()
    {
        int visibleDistance = CalculateDistance((int)GameEngine.Instance.MoveController.transform.position.z) + GameEngine.Instance.EnvSettings.DistancesVisible;
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
        int playerDistance = CalculateDistance((int)GameEngine.Instance.MoveController.transform.position.z);
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
        pointsParent.transform.position = new Vector3(0, 0, distanceNumber * GameEngine.Instance.EnvSettings.DistanceBetweenWalls);
        for (int i = 0; i< BonusPerDistance;++i)
        {
            GameObject bonusPoint = GameObject.Instantiate(BonusPointPrefabs[random.Next(0, BonusPointPrefabs.Length)]);
            bonusPoint.transform.parent = pointsParent.transform;
            Rail randomRail = GameEngine.Instance.EnvSettings.RailsDefinitions[random.Next(0, GameEngine.Instance.EnvSettings.RailsDefinitions.Length)];
            bonusPoint.transform.localPosition = new Vector3(randomRail.WorldPositionX, randomRail.WorldPositionY, random.Next(GapToWall, GameEngine.Instance.EnvSettings.DistanceBetweenWalls - GapToWall));
        }
        _generatedPoints.Add(pointsParent);
    }

    private int CalculateDistance(int zPosition)
    {
        return zPosition / GameEngine.Instance.EnvSettings.DistanceBetweenWalls;
    }

    private List<GameObject> _generatedPoints = new List<GameObject>();
}
