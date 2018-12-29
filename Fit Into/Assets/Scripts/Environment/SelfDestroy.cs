using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class SelfDestroy : MonoBehaviour
{
    public void Active(float time)
    {
        Destroy(this.gameObject, time);
    }
}
