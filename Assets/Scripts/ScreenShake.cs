using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] private float duration = 0.4f;
    private bool start = false;
    public AnimationCurve curve;
    private TimeManager timemanager;

    void Start()
    {
        timemanager = timemanager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
    }

    void Update()
    {
        if (timemanager.TimeIsStopped && start == false)
        {
            StartCoroutine(Shaking());
            start = true;
        }

        if (!timemanager.TimeIsStopped && start == true)
        {
            start = false;
        }
    }

    IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            startPosition = transform.position;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }
    }
}