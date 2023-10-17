using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private int jewels = 0;
    public TextMeshProUGUI jewelsText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Jewel"))
        {
            Destroy(collision.gameObject);
            jewels++;
            jewelsText.text = "Jewels: " + jewels;
            AudioManager.instance.Play("Coin");
        }
    }
}
