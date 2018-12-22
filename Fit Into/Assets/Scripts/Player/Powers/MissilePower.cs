using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MissilePower : PowerBase
{
    public Missile MissilePrefab;
    public int Level;

    public void Awake()
    {
        if (MissilePrefab == null)
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
        for (int i = 0; i <= Level; ++i)
        {
            Rigidbody missile = GameObject.Instantiate(MissilePrefab).GetComponent<Rigidbody>();
            missile.transform.position = moveController.transform.position;
            missile.velocity = new Vector3(0, 0, 10);
            yield return new WaitForSeconds(.4f);
        }
    }
}
