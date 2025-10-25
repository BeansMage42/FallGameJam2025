using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Timer : MonoBehaviour
{
    public float time;
    public bool isCanceled;

    private bool timerFinished;

    private void Update()
    {
        if (time > 0 && !isCanceled)
        {
            time -= Time.deltaTime;
        }
        else if (time <= 0 && !timerFinished)
        {
            print("Timer finished!");
            TimerFinished();
            timerFinished = true;
        }

    }
}