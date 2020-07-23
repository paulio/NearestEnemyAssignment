using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    Enemy _currentNearestEnemy = null;
    private bool _hasEnemies;
    Dictionary<int, GameObject> _enemies = null;

    private void Awake()
    {
        NearViaTrigger.OnNearestEnemiesChanged += this.NearViaTrigger_OnNearestEnemiesChanged;
    }


    private void Update()
    {
        if (_hasEnemies)
        {
            UpdateNearestPosition();
        }
    }

    private GameObject GetNearestEnemy(IEnumerable<GameObject> enemies)
    {
        GameObject nearestEnemy = null;
        var nearestDistance = float.PositiveInfinity;
        var currentPosition = transform.position;
        foreach(var enemy in enemies)
        {
            var hit = enemy;
            if (hit == null)
            {
                break;
            }

            var calcDistance = Vector3.Distance(hit.transform.position, currentPosition);
            if (calcDistance < nearestDistance)
            {
                nearestDistance = calcDistance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    private void NearViaTrigger_OnNearestEnemiesChanged(Dictionary<int, GameObject> enemies)
    {
        _hasEnemies = true;
        _enemies = enemies;
        UpdateNearestPosition();
    }

    private void UpdateNearestPosition()
    {
        var nearestEnemy = GetNearestEnemy(_enemies.Values);
        //var currentPosition = transform.position;
        //var nearestEnemy = _enemies.Values.OrderBy(e => Vector3.Distance(e.transform.position, currentPosition)).FirstOrDefault();
        if (nearestEnemy != null)
        {
            if (_currentNearestEnemy != null)
            {
                this._currentNearestEnemy.IsNearest = false;
            }

            _currentNearestEnemy = nearestEnemy.GetComponent<Enemy>();
            _currentNearestEnemy.IsNearest = true;
        }
    }
}
