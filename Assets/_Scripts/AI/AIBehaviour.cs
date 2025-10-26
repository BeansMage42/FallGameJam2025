using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class AIBehaviour : MonoBehaviour
{
    protected NavMeshAgent _agent;

    public virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void TargetLocation(Vector3 location)
    {
        _agent.SetDestination(location);
    }

    public virtual void MoveUpInLine()
    {

    }
}
