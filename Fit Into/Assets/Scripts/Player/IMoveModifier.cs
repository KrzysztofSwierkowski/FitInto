using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IMoveModifier
{
    DateTime EndTime { get; }

    float Modify(float baseSpeed);
}
