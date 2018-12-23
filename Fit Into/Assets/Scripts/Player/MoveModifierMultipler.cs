using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class MoveModifierMultipler : IMoveModifier
{
    public DateTime EndTime { get; private set; }
    private readonly float _multiple;

    public MoveModifierMultipler(DateTime finish, float multiple)
    {
        EndTime = finish;
        _multiple = multiple;
    }

    public float Modify(float baseSpeed)
    {
        return baseSpeed * _multiple;
    }
}
