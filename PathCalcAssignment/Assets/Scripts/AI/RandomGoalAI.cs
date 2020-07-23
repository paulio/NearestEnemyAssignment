using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RandomGoalAI : MonoBehaviour
{
    [SerializeField]
    float _boardRadius = 6f;

    private NavMeshAgent _navMeshAgent;

    private readonly WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(NextGoal());
    }

    private IEnumerator NextGoal()
    {
        Vector3 nextGoal = default;
        while (nextGoal == default)
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * _boardRadius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, _boardRadius, NavMesh.AllAreas))
            {
                nextGoal = hit.position;
            }

            yield return _waitForEndOfFrame;
        }

        _navMeshAgent.stoppingDistance = UnityEngine.Random.Range(0.1f, 2f);
        _navMeshAgent.SetDestination(nextGoal);
    }

   
}
