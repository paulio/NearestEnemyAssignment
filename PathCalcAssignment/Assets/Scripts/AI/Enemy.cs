using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Enemy : MonoBehaviour
{
    private Material _material;
    private Color _startColor;
    private bool _isNearest;

    public bool IsNearest 
    { 
        get
        {
            return _isNearest;
        }

        set
        {
            if (value)
            {
                _material.color = Color.green;
            }
            else
            {
                _material.color = _startColor;
            }

            _isNearest = value;
        }
    }

    public Vector3 EnemyPosition => transform.position;

    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        _startColor = _material.color;
    }
}
