using UnityEngine;
using System.Collections;

public class AnimationStop : MonoBehaviour
{
	private Animator anim;
	private TimeManager timemanager;

    private void Start()
    {
        anim = GetComponent<Animator>();
        timemanager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
    }

    void Update()
	{
        if (timemanager.TimeIsStopped)
        {
            anim.speed = 0;
        }
        else
        {
            anim.speed = 1;
        }

    }
}

