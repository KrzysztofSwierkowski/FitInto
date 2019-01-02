using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PowerDefinition
{
    public PowerType Type;
    public GameObject Prefab;
}

public class PowerController : MonoBehaviour
{
    [SerializeField]
    private List<PowerDefinition> _definitions;

    [SerializeField]
    private GameObject _powerSlot;

    [SerializeField]

    public PowerBase CurrentPower { get; private set; }

    private void Start()
    {
        ResetStatus();
    }

    public void UsePower()
    {
        CurrentPower.Use();
    }

    public void ResetStatus()
    {
        GameSettings settings = GameEngine.Instance.GameSettingsManager.Load();
        foreach (Transform child in _powerSlot.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        GameObject newPower = GameObject.Instantiate(_definitions.First(x => x.Type == settings.Power.Type).Prefab, _powerSlot.transform);
        CurrentPower = newPower.GetComponent<PowerBase>();
        CurrentPower.Level = settings.Power.Level;
    }
}
