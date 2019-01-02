using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PowerUI : MonoBehaviour
{
    public void Update()
    {
        PowerBase power = GameEngine.Instance?.PowerController?.CurrentPower;
        if (power == null)
        {
            return;
        }
        Image image = GetComponentInChildren<Image>();
        image.sprite = power.Image;
        image.fillAmount = (power.BasicCooldown - power.Cooldown) / power.BasicCooldown; 

    }

    public void OnPower()
    {
        Debug.Log("Use power");
        GameEngine.Instance.PowerController.UsePower();
    }
    
}
