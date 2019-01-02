using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{
    private int _maxStats;
    private GameSettingsLoader _storage;

    private void Awake()
    {
        _storage = new GameSettingsLoader(Defs.SettingsFileName);
        _maxStats = Defs.MaxStats;
    }

    public void AddNewPointsResult(int point)
    {
        _storage.Update((sett) =>
        {
            sett.Stats.Add(point);
            sett.Stats = sett.Stats.OrderByDescending(x => x).Take(_maxStats).ToList();
            return sett;
        });

    }

    public void SelectPower(Power power)
    {
        _storage.Update((sett) =>
        {
            sett.Power = power;
            return sett;
        });
    }

    public GameSettings Load()
    {
        return _storage.Load();
    }
}
