using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float bounce = 25f;
    [SerializeField] private float timeBodyBounce = 1f;
    [SerializeField] private bool isJumperUp = false;
    private bool timeFreeze = false;
    private Animator anim;
    private TimeManager timemanager;

    private void Start()
    {
        anim = GetComponent<Animator>();
        timemanager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool soundfx = false;
        if (!timeFreeze)
        {
            if (collision.gameObject.CompareTag("Player") && isJumperUp)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
                soundfx = true;
            }

            if (collision.gameObject.CompareTag("TimeBody") && isJumperUp)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * timeBodyBounce, ForceMode2D.Impulse);
                soundfx = true;
            }
            if (soundfx)
            {
                AudioManager.instance.Play("Jumper");
            }
        }
    }

    private void Update()
    {
        if (timemanager.TimeIsStopped)
        {
            timeFreeze = true;
            anim.speed = 0;
        }
        else
        {
            timeFreeze = false;
            anim.speed = 1;
        }
    }

}
