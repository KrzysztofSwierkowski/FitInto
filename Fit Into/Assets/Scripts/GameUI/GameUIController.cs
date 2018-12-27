using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public GameObject GameOverMenu;
    public GameObject HUB;

    private CollisionController _collisionController;


    public void Update()
    {
        if (_collisionController == null)
        {
            _collisionController = GameObject.FindObjectOfType<CollisionController>();
        }
        GameOverMenu.SetActive(_collisionController.IsGameOver);
        HUB.SetActive(false == _collisionController.IsGameOver);
    }
}
