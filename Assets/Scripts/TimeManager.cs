using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimeManager : MonoBehaviour
{
    public bool TimeIsStopped;

    [SerializeField] private AudioSource freezeTimeSoundEffect;
    [SerializeField] private AudioSource unfreezeTimeSoundEffect;
    [SerializeField] private AudioSource tickingSoundEffect;

    public void ContinueTime()
    {
        TimeIsStopped = false;

        var objects = FindObjectsOfType<TimeBody>();  //Find Every object with the Timebody Component
        for (var i = 0; i < objects.Length; i++)
        {
            objects[i].GetComponent<TimeBody>().ContinueTime(); //continue time in each of them
        }

        unfreezeTimeSoundEffect.Play();
        tickingSoundEffect.Stop();
        if (freezeTimeSoundEffect.isPlaying)
        {
            freezeTimeSoundEffect.Stop();
        }

    }
    public void StopTime()
    {
        TimeIsStopped = true;
        freezeTimeSoundEffect.Play();
        tickingSoundEffect.Play();
        if (unfreezeTimeSoundEffect.isPlaying)
        {
            unfreezeTimeSoundEffect.Stop();
        }
    }
}
