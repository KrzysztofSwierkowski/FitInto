using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Shape[] AcceptableShapes;

    [SerializeField]
    GameObject _OnDestroyObject;

    public bool AcceptShape(Shape shape)
    {
        return AcceptableShapes.Contains(shape);
    }

    private void OnDestroy()
    {
        if (_OnDestroyObject != null)
        {
            GameObject go = GameObject.Instantiate(_OnDestroyObject);
            var destroy = go.AddComponent<SelfDestroy>();
            destroy.Active(2f);
            go.transform.position = this.transform.position;
        }
    }
}
