using System;
using UnityEngine;

public class DartSpawner : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Background"))
        {

            Debug.Log("Arrow hit the target!");


            GetComponent<Rigidbody>().isKinematic = true;

        }
    }
}

    