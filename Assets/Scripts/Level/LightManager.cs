using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    private static readonly int Light = Animator.StringToHash("Light");
    [SerializeField] private float bpm;
    [SerializeField] private float duration;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Intervals[] intervals;
    [SerializeField] private GameObject[] lights;
    [SerializeField] private Animator animator;

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
                    animator.SetTrigger(Light);
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

    public void SetBpm(float bpmNew)
    {
        this.bpm = bpmNew;
    }
    
    
    public void SetDuration(float durationNew)
    {
        this.duration = durationNew;
    }

    public void ActivateLight()
    {
        foreach (GameObject obj in lights)
        {
            obj.GetComponent<Light2D>().intensity = obj.CompareTag($"Stage") ? 1f : 5f;
        }
    }

    public void DeactivateLight()
    {
        foreach (GameObject obj in lights)
        {
            obj.GetComponent<Light2D>().intensity = 0;
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

