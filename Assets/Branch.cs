using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    public static float bottomY = -20f;

    // Update is called once per frame
    void Update()
    {
        // If branch falls below the bottom, just destroy it
        if (transform.position.y < bottomY)
        {
            Destroy(this.gameObject);
        }
    }
}
