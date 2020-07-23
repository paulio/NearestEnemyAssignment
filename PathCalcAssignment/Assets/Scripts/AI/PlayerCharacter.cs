using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]
    float _visionDistance = 12f;

    [SerializeField]
    int _maxEnimies = 100;

    Enemy _currentNearestEnemy = null;
    private bool _hasEnemies;
    Dictionary<int, GameObject> _enemies = null;
    private int _enemiesLayerMask;
    private int _currentNearestEnemyId;

    private Collider[] _hits;

    private void Start()
    {
        _enemiesLayerMask = LayerMask.GetMask("Enemies");
        _hits = new Collider[_maxEnimies];
    }

    private void FixedUpdate()
    {
        //if (_hasEnemies)
        {
            UpdateNearestPositionViaRayCast();
        }
    }

    private void UpdateNearestPositionViaRayCast()
    {
        if (Physics.OverlapSphereNonAlloc(transform.position, _visionDistance, _hits, _enemiesLayerMask) > 0)
        {
            var nearestEnemy = GetNearestEnemy(_hits);
            if (nearestEnemy != null)
            {
                var nearestEnemyId = nearestEnemy.GetInstanceID();
                if (nearestEnemyId != _currentNearestEnemyId)
                {
                    if (_currentNearestEnemyId != 0)
                    {
                        _currentNearestEnemy.IsNearest = false;
                    }

                    _currentNearestEnemy = nearestEnemy;
                    _currentNearestEnemyId = nearestEnemyId;
                    _currentNearestEnemy.IsNearest = true;
                }
            }
        }
    }

    private Enemy GetNearestEnemy(Collider[] hits)
    {
        var nearestIndex = -1;
        var nearestDistance = float.PositiveInfinity;
        var currentPosition = transform.position;
        for (int i = 0; i < hits.Length; i++)
        {
            var hit = hits[i];
            if (hit == null)
            {
                break;
            }

            var calcDistance = Vector3.Distance(hit.transform.position, currentPosition);
            if (calcDistance < nearestDistance)
            {
                nearestDistance = calcDistance;
                nearestIndex = i;
            }
        }

        if (nearestIndex > -1)
        {
            // print($"Nearest ememy was {hits[nearestIndex].name}");
            return hits[nearestIndex].gameObject.GetComponent<Enemy>();
        }
        else
        {
            return null;
        }
    }
}
