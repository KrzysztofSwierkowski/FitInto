using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

    public class PlayerStatisticsPoints : MonoBehaviour
    {
        public int ResultsCount;

        public void AddResult(int point)
        {
            List<int> current = LoadData();
            current.Add(point);
            SaveData(current.OrderByDescending(x => x).Take(ResultsCount).ToList());
        }

        public List<int> GetResults()
        {
            return LoadData().Take(ResultsCount).ToList();
        }

        private List<int> LoadData()
        {
            if (File.Exists(GetPath()) == false)
            {
                return new List<int>();
            }
            string[] lines = File.ReadAllLines(GetPath());
            List<int> result = new List<int>();
            foreach(string str in lines)
            {
                result.Add(int.Parse(str));
            }
            return result;
        }

        private void SaveData(IEnumerable<int> data)
        {
            File.WriteAllLines(GetPath(), data.Select(x => x.ToString()).ToArray());
        }

        private string GetPath()
        {
            return Defs.StatsFileName;
        }
    }
