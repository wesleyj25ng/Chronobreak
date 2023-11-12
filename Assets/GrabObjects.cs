using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObjects : MonoBehaviour
{
    public Transform grabDetect;
    public Transform boxHolder;
    public float maxRayDist = 1.5f; // Maximum ray distance
    private int layerIndex;
    private Collider2D playerCollider; // Reference to the player's collider
    private TimeManager timemanager;

    private void Start()
    {
        layerIndex = LayerMask.NameToLayer("Object");
        playerCollider = GetComponent<Collider2D>();
        timemanager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale.x, maxRayDist, 1 << layerIndex);

        if (grabCheck.collider != null && grabCheck.collider.gameObject.layer == layerIndex)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                Rigidbody2D rb2d = grabCheck.collider.gameObject.GetComponent<Rigidbody2D>();
                if (rb2d != null)
                {
                    // Disable collision between the player and the grabbed object
                    Physics2D.IgnoreCollision(playerCollider, grabCheck.collider, true);

                    grabCheck.collider.gameObject.transform.parent = boxHolder;
                    rb2d.velocity = Vector2.zero; // Stop any existing movement
                    rb2d.isKinematic = true;
                }
                else
                {
                    Debug.LogWarning("Rigidbody2D component not found on the grabbed object.");
                }
            }
            else if (Input.GetKeyUp(KeyCode.G))
            {
                Rigidbody2D rb2d = grabCheck.collider.gameObject.GetComponent<Rigidbody2D>();
                if (rb2d != null)
                {
                    // Re-enable collision between the player and the grabbed object
                    Physics2D.IgnoreCollision(playerCollider, grabCheck.collider, false);

                    grabCheck.collider.gameObject.transform.parent = null;
                    rb2d.isKinematic = false;

                    if (timemanager.TimeIsStopped)
                    {
                        rb2d.velocity = Vector2.zero; //makes the rigidbody stop moving
                        rb2d.isKinematic = true; //not affected by forces
                    }
                } 
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(grabDetect.position, grabDetect.position + Vector3.right * transform.localScale.x * maxRayDist);
    }
}
