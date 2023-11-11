using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObjects : MonoBehaviour
{
    public Transform grabDetect;
    public Transform boxHolder;
    public float maxRayDist = 1.5f; // Maximum ray distance
    private int layerIndex;

    private void Start()
    {
        layerIndex = LayerMask.NameToLayer("Object");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, maxRayDist, 1 << layerIndex);

        if (grabCheck.collider != null && grabCheck.collider.gameObject.layer == layerIndex)
        {
            float currentRayDist = Mathf.Clamp(Vector2.Distance(transform.position, grabCheck.point), 0, maxRayDist);

            if (Input.GetKeyDown(KeyCode.G))
            {
                Rigidbody2D rb2d = grabCheck.collider.gameObject.GetComponent<Rigidbody2D>();
                if (rb2d != null)
                {
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
                    grabCheck.collider.gameObject.transform.parent = null;
                    rb2d.isKinematic = false;
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