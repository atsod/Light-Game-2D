using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<PatternDownMovement>())
        {
            Destroy(other.gameObject);
        }
    }
}
