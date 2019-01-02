using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MissilePower : PowerBase
{
    [SerializeField]
    private Missile _missilePrefab;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _cooldownReducePerLevel;

    public void Awake()
    {
        if (_missilePrefab == null)
        {
            Debug.LogError("MissilePrefab is not set");
        }
    }
    

    protected override void UseIntern()
    {
        MoveController moveController = GameObject.FindObjectOfType<MoveController>();
        Rigidbody missile = GameObject.Instantiate(_missilePrefab).GetComponent<Rigidbody>();
        missile.transform.position = moveController.transform.position;
        missile.velocity = new Vector3(0, 0, _speed);
    }

    protected override void SetCooldown()
    {
        Cooldown = BasicCooldown - BasicCooldown * _cooldownReducePerLevel * Level;
        Debug.Log(Cooldown);
    }
}
