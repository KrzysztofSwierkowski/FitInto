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

    public void Update()
    {
        if(Input.GetKey(KeyCode.K))
        {
            base.Use();
        }
        base.UpdateFrame();
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
        Cooldown = TimeSpan.FromSeconds(_cooldownSeconds - _cooldownSeconds * _cooldownReducePerLevel * _level);
        Debug.Log(Cooldown);
    }
}
