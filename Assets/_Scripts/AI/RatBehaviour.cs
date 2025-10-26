using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class RatBehaviour : AIBehaviour
{
    Coroutine waitRoutine;
    [SerializeField] float distance = 4f;
    [SerializeField] float waitTime = 1f;

    private void Start()
    {
        StartCoroutine(WanderToMotion());
    }
    private IEnumerator WanderToMotion()
    {

        _agent.isStopped = false;
        TargetLocation(CreatePosition());
         Debug.Log("start moving to destination");
        yield return new WaitUntil(() => _agent.remainingDistance <= _agent.stoppingDistance && !_agent.pathPending);
        _agent.isStopped = true;
        _agent.destination = transform.position;
         StartCoroutine(Wait());


        //State = defaultState;
    }



    private Vector3 CreatePosition()
    {

        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += transform.position;
        randomDirection.y = 0;
        NavMeshHit navHit;

        if (!NavMesh.SamplePosition(randomDirection, out navHit, distance, _agent.areaMask))
        {
            Debug.LogWarning("couldnt find position");
            
        }
        // print("current pos: " + transform.position + " SamplePos: " + navHit.position);



        return navHit.position;
    }
    private IEnumerator Wait()
    {

         print("waiting");
        yield return new WaitForSeconds(waitTime);
         print("wait finished");
        StartCoroutine(WanderToMotion());
    }
}
