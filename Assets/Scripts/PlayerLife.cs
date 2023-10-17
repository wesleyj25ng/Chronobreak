using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{

    private Animator anim;
    private Rigidbody2D rb;
    private TimeManager timemanager;
    private BackgroundMusic bgm;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        timemanager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
        bgm = FindObjectOfType<BackgroundMusic>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            AudioManager.instance.Play("Saw");
            Die();
        }

        if (collision.gameObject.CompareTag("Out of Bounds"))
        {
            AudioManager.instance.Play("Out of Bounds");
            Die();
        }
    }



    public void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (timemanager.TimeIsStopped)
        {
            bgm.ContinueMusic(); // Unpause the background music if time is stopped
        }
    }
}
