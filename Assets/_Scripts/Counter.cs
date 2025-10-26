using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    private List<AIBehaviour> _theLine = new List<AIBehaviour>();
    [SerializeField] private float _lineSpacing;
    [SerializeField] private Transform[] availableSeats;
    List<Transform> filledSeats = new List<Transform>();
    List<Transform> emptySeats = new List<Transform>();

    private void Start()
    {
        foreach (Transform t in availableSeats)
        {
            emptySeats.Add(t);
        }
    }
    public void AddAIToLine(AIBehaviour newAI)
    {
        _theLine.Add(newAI);
    }

    public void RemoveCustomerFromLine(AIBehaviour newAI)
    {
        if (!_theLine.Contains(newAI)) return;
        int aiIndex = _theLine.IndexOf(newAI);
        Debug.Log(aiIndex);
        _theLine.Remove(newAI);
        for(int i = aiIndex; i < _theLine.Count; i++)
        {
            _theLine[i].TargetLocation(transform.position + (-Vector3.forward * _lineSpacing * (i+1)));
            _theLine[i].MoveUpInLine();
        }


    }

    public bool IsFirstCustomer(AIBehaviour newAI)
    {
        return _theLine.IndexOf(newAI) == 0;
    }

    public Vector3 LineEndPos()
    {
        return transform.position + (-Vector3.forward * _lineSpacing)*_theLine.Count;
    }

    public Transform GetEmptySeat()
    {
        Transform goToSeat = emptySeats[0];
        emptySeats.Remove(goToSeat);
        return goToSeat;

    }

    public void ReturnEmptySeat(Transform goToSeat)
    {
        emptySeats.Add(goToSeat);

    }

}
