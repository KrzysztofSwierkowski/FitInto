using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Missile : MonoBehaviour
{

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Wall>() != null)
        {
            GameObject.Destroy(other.gameObject);
            GameObject.Destroy(this.gameObject);
        }
    }
}
