using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] footstepsClips;




    public void PlayerFootStep()
    {
        audioSource.clip = footstepsClips[Random.Range(0, footstepsClips.Length)];
        audioSource.pitch = (Random.Range(0.95f, 1.05f));
        audioSource.Play();
    }







}
