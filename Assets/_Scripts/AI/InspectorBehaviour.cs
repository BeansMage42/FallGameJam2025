using UnityEngine;
using UnityEngine.InputSystem.XR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
public class InspectorBehaviour : AIBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    private int _currentPathIndex;
    [SerializeField] private float _delayTime;

    [Header("FOV")]
    [Range(0, 360)]
    [SerializeField] private float angle;//what angle it can see within
    [SerializeField] private float viewDistance;//how far it can see
    [SerializeField] private LayerMask targetMask; //player layer
    [SerializeField] private LayerMask obstructMask; // layers it cant hear/see through
    [SerializeField] Interaction player;
    
    AudioSource audioSource;
    [SerializeField] private AudioClip [] voiceClip;
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(PatrolMotion());
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector3 enemyPosition = transform.position;
        Vector3 toPlayer = player.transform.position - enemyPosition;



        if (Vector3.Dot(toPlayer.normalized, transform.forward) > Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad))//checks if player is within the angle
        {



            if (!Physics.Raycast(transform.position, toPlayer.normalized, Vector3.Distance(transform.position, player.transform.position), obstructMask))//checks for walls
            {
                player.WatchStateChange(true);
               
            }
            else
            {
                player.WatchStateChange(false);
            }

        }
        else
        {
            player.WatchStateChange(false);
        }
    }

    private IEnumerator PatrolMotion()
    {
        TargetLocation(_waypoints[_currentPathIndex].position);

        while (true)
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                _currentPathIndex = ++_currentPathIndex % _waypoints.Length;//loops the array automatically
               
                TargetLocation(_waypoints[_currentPathIndex].position);

            }
            yield return new WaitForSeconds(_delayTime);
        }
    }
}
