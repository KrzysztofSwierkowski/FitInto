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
    private float _sleepTime;

    [SerializeField]
    private float _speed;

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
    }

    protected override void UseIntern()
    {
        StartCoroutine(Fire());
        
    }

    private IEnumerator Fire()
    {
        MoveController moveController = GameObject.FindObjectOfType<MoveController>();
        for (int i = 0; i <= _level; ++i)
        {
            Rigidbody missile = GameObject.Instantiate(_missilePrefab).GetComponent<Rigidbody>();
            missile.transform.position = moveController.transform.position;
            missile.velocity = new Vector3(0, 0, _speed);
            yield return new WaitForSeconds(_sleepTime);
        }
    }
}
