using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SlowerPower : PowerBase
{
    [SerializeField]
    private float _slowerPerLevel;
    [SerializeField]
    private double _effectTimeSec;

    public void Update()
    {
        if (Input.GetKey(KeyCode.J))
        {
            base.Use();
        }
        base.UpdateFrame();
    }

    protected override void UseIntern()
    {
        MoveController moveController = GameObject.FindObjectOfType<MoveController>();
        moveController.AddNewModifier(new MoveModifierMultipler(DateTime.UtcNow.AddSeconds(_effectTimeSec), _slowerPerLevel * (base._level + 1)));
    }
}
