using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public GameObject ToShow;
    public bool IsGameOver;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Wall>() != null)
        {
            IsGameOver = true;
            ToShow.SetActive(true);
        }
    }

}
