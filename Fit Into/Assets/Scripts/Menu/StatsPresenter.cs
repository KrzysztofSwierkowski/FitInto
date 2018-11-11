using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class StatsPresenter : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        PlayerStatisticsPoints points = GameObject.FindObjectOfType<PlayerStatisticsPoints>();
        StringBuilder sb = new StringBuilder();
        List<int> stats = points.GetResults();
        for(int i = 0; i <stats.Count;++i)
        {
            sb.AppendLine(i+1 + ". " + stats[i]);
        }
        GetComponent<Text>().text = sb.ToString();
	}

}
