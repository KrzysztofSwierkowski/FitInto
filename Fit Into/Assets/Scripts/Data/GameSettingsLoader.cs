using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameSettingsLoader
{
    readonly private object _cs = new object();
    readonly private string _filePath;

    public GameSettingsLoader(string filePath)
    {
        _filePath = filePath;
    }

    public GameSettings Load()
    {
        lock (_cs)
        {
            return LoadFromFile();
        }
    }

    public void Save(GameSettings gameSettings)
    {
        lock (_cs)
        {
            SaveToFile(gameSettings);
        }
    }

    public void Update(Func<GameSettings, GameSettings> update)
    {
        lock(_cs)
        {
            Save(update(Load()));
        }
    }

    private GameSettings LoadFromFile()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                string data = File.ReadAllText(_filePath);
                return JsonUtility.FromJson<GameSettings>(data);
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        return new GameSettings();
    }

    private void SaveToFile(GameSettings settings)
    {
        try
        {
            string data = JsonUtility.ToJson(settings);
            File.WriteAllText(_filePath, data);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}
