using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotation : MonoBehaviour
{
    public GameObject dartPrefab;
    public Transform throwPoint;
    public GameObject target;
    float throwForce = 600f;
    float moveSpeed = 50f;
    int dartCount = 0;
    GameObject[] spawnedDarts;

    private Vector2 swipeStartPosition;
   private bool isStopped = false;
    private Vector2 swipeEndPosition;
    float minSwipeMagnitude = 100f; 
    float maxSwipeMagnitude = 1500f;

    //public AudioSource dartHitSound;

    //  public AudioSource audioPlayer;
    //public AudioClip clip;

    void Start()
    {
     
        spawnedDarts = new GameObject[3];
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                swipeStartPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                swipeEndPosition = touch.position;
                Vector2 swipeDirection = swipeEndPosition - swipeStartPosition;
                float swipeMagnitude = swipeDirection.magnitude;
                if (swipeMagnitude >= minSwipeMagnitude && swipeMagnitude <= maxSwipeMagnitude)
                {

                    Ray ray = Camera.main.ScreenPointToRay(swipeEndPosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        //Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
                    }

                    Vector3 moveDirection = ray.origin - throwPoint.position;

                    if (dartCount == 3)
                    {
                        DestroyDarts();
                        dartCount = 0;
                    }

                    GameObject spawnedObject = Instantiate(dartPrefab, ray.origin, Quaternion.identity);
                    spawnedDarts[dartCount] = spawnedObject;

                    Rigidbody dartRigidbody = spawnedObject.GetComponent<Rigidbody>();
                    dartRigidbody.AddForce(moveDirection * throwForce, ForceMode.Impulse);

                    Debug.DrawRay(ray.origin, ray.direction * 500f, Color.red, 1f);

                    dartCount++;
                }


            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rotater"))
        {
           //audioPlayer.Play();
            //audioPlayer.PlayOneShot(clip);
            Debug.Log("Arrow hit the target!");
         


            GetComponent<Rigidbody>().isKinematic = true;
            //PlayDartHitSound();
            StartCoroutine(DestroyDartObject());
        }
    }
    //private void PlayDartHitSound()
    //{
    //    if (dartHitSound != null)
    //    {
    //        dartHitSound.Play();
    //    }
    //}

    private IEnumerator DestroyDartObject()
    {
        yield return new WaitForSeconds(100f);
        Destroy(gameObject);

        if (dartCount == 3)
        {
            SpawnNewObject();
        }
    }

    private void DestroyDarts()
    {
        for (int i = 0; i < 3; i++)
        {
            if (spawnedDarts[i] != null)
            {
                Destroy(spawnedDarts[i]);
            }
        }
    }

    private void SpawnNewObject()
    {
        GameObject spawnedObject = Instantiate(dartPrefab, throwPoint.position, Quaternion.identity);
        spawnedDarts[0] = spawnedObject;

        Rigidbody dartRigidbody = spawnedObject.GetComponent<Rigidbody>();
        dartRigidbody.AddForce(Vector3.forward * throwForce, ForceMode.Impulse);
    }
}

