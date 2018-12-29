using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerBase : MonoBehaviour {

    [SerializeField]
    protected double _cooldownSeconds;
    [SerializeField]
    protected int _level;

    public TimeSpan Cooldown { get; protected set; }

    protected void UpdateFrame()
    {
        if (Cooldown <= TimeSpan.Zero)
        {
            Cooldown = TimeSpan.Zero;
            return;
        }
        Cooldown -= TimeSpan.FromSeconds(Time.deltaTime);
    }


    public void Use()
    {
        if (Cooldown > TimeSpan.Zero)
        {
            return;
        }
        SetCooldown();
        UseIntern();
    }

    protected virtual void SetCooldown()
    {
        Cooldown = TimeSpan.FromSeconds(_cooldownSeconds);
    }

    protected abstract void UseIntern();
}
