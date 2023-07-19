using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHandler : MonoBehaviour
{

        private void OnTriggerEnter(Collider other)
        {
      
                if (other.gameObject.CompareTag("Rotater") )
                {

                    Debug.Log("Arrow hit the target!");


                    GetComponent<Rigidbody>().isKinematic = true;

                    StartCoroutine(DestroyDartObject());

                }

           
            }
                private IEnumerator DestroyDartObject()
            {
                yield return new WaitForSeconds(100f); 
                Destroy(gameObject);
            }

 
}
