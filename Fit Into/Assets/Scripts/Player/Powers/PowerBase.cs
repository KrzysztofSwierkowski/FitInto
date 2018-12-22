using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerBase : MonoBehaviour {

    [SerializeField]
    protected double _cooldownSeconds;
    [SerializeField]
    protected int _level;

    public TimeSpan Cooldown
    {
        get
        {
            return TimeSpan.FromSeconds(_cooldownSeconds);
        }
    }
    public TimeSpan ToUse
    {
        get
        {
            TimeSpan fromUse = DateTime.UtcNow - _lastUsed;
            if (fromUse >= Cooldown)
            {
                return TimeSpan.Zero;
            }
            return Cooldown - fromUse;
        }
    }
    private DateTime _lastUsed = DateTime.UtcNow;


    public void Use()
    {
        if (ToUse != TimeSpan.Zero)
        {
            return;
        }
        UseIntern();
        _lastUsed = DateTime.UtcNow;
    }

    protected abstract void UseIntern();
}
