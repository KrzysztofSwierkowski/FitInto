using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Ta klasa jest trochę z dupy, bo żeby coś zmienić trzeba wszystkie dane przeserializować.
// Jednak zakłądam, że są to małe dane i nigdy w tym projekcie się nie rozrosną, tak aby to bolało.
// Niech będzie. 
// Podobnie z Immutable. 

[Serializable]
public class GameSettings
{
    public Power Power;
    public List<int> Stats;

    public GameSettings()
    {
        Stats = new List<int>();
        Power = new Power { Type = PowerType.Collect, Level = 1 };
    }
}

[Serializable]
public class Power
{
    public PowerType Type;
    public int Level;
}


public enum PowerType
{
    Collect,
    Missile,
    Slow
}