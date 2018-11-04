using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour
{
    public GameObject ToMenuButton;

    private CollisionController _collisionController;


    public void Update()
    {
        if (_collisionController == null)
        {
            _collisionController = GameObject.FindObjectOfType<CollisionController>();
        }
        ToMenuButton.SetActive(_collisionController.IsGameOver);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
