using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightManager : MonoBehaviour
{
    [SerializeField] private float bpm;
    [SerializeField] private float duration;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Intervals[] intervals;
    [SerializeField] private GameObject[] lights;

    private void Update()
    {
        foreach (Intervals i in intervals)
        {
            float sampledTime = (audioSource.timeSamples / (audioSource.clip.frequency * i.GetIntervalLength(bpm)));
            if (i.CheckForNewInterval(sampledTime))
            {
                foreach (GameObject obj in lights)
                {
                    obj.SetActive(true);
                }

                StartCoroutine(DisableLights());
            }
        }
    }

    private IEnumerator DisableLights()
    {
        yield return new WaitForSeconds(duration);
        foreach (GameObject obj in lights)
        {
            obj.SetActive(false);
        }
    }
    
}

[System.Serializable]
public class Intervals
{
    [SerializeField] private float steps;

    private int _lastInterval;

    public float GetIntervalLength(float bpm)
    {
        return 60f / (bpm * steps);
    }

    public bool CheckForNewInterval(float interval)
    {
        if (Mathf.FloorToInt(interval) != _lastInterval)
        {
            _lastInterval = (int)interval;
            return true;
        }
        return false;
    }
}

