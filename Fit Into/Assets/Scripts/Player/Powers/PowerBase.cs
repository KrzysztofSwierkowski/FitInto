using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerBase : MonoBehaviour {
    
    [SerializeField]
    public float BasicCooldown;

    [SerializeField]
    public Sprite Image;
    
    public int Level { get; set; }
    
    public float Cooldown { get; protected set; }

    public void Update()
    {
        if (Cooldown <= 0)
        {
            Cooldown = 0;
            return;
        }
        Cooldown -= Time.deltaTime;
    }


    public void Use()
    {
        if (Cooldown > 0)
        {
            return;
        }
        SetCooldown();
        UseIntern();
    }

    protected virtual void SetCooldown()
    {
        Cooldown = BasicCooldown;
    }

    protected abstract void UseIntern();
}
