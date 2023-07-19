using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    public AudioSource PlaySound;
    public AudioClip clip;

    public void OnTriggerEnter(Collider other)
    {

         PlaySound.Play();
        PlaySound.PlayOneShot(clip);

    }
}
