using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class NearViaTrigger : MonoBehaviour
{
    public static event Action<Dictionary<int, GameObject>> OnNearestEnemiesChanged;
    public readonly Dictionary<int, GameObject> _nearestEnemies = new Dictionary<int, GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.gameObject;
            var id = enemy.GetInstanceID();
            if (!_nearestEnemies.ContainsKey(id))
            {
                _nearestEnemies.Add(id, enemy);
                OnNearestEnemiesChanged?.Invoke(_nearestEnemies);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.gameObject;
            var id = enemy.GetInstanceID();
            if (!_nearestEnemies.ContainsKey(id))
            {
                _nearestEnemies.Add(id, enemy);
                OnNearestEnemiesChanged?.Invoke(_nearestEnemies);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.gameObject;
            var id = enemy.GetInstanceID();
            _nearestEnemies.Remove(id);
            OnNearestEnemiesChanged?.Invoke(_nearestEnemies);
        }
    }
}
