using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientLogic : MonoBehaviour
{
    public static AmbientLogic current;
    [Header("Двигательный отсек")]
    [SerializeField] private AudioClip ChargingSound;
    [SerializeField] private AudioClip trustSound;
    [SerializeField] private AudioClip expSound;
    [SerializeField] private AudioSource[] AudioSourcePool = new AudioSource[10];

    [SerializeField] private AudioSource[] AmbientSounds;
    [SerializeField] private GameObject[] rocketFireEffects = new GameObject[3];
    [Space]
    [SerializeField] private AudioSource explosionAS;
    [SerializeField] private CameraMovement cameraMovement;
    [Space]
    [SerializeField] private AudioClip[] music = new AudioClip[4];
    [SerializeField] private AudioSource musicAS;
    private int currentMusicId;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        foreach (GameObject obj in rocketFireEffects)
        {
            obj.SetActive(false);
        }
        foreach (AudioSource ambientAS in AmbientSounds)
        {
            ambientAS.Play();
        }
        StartCoroutine(MakeLateBoom());
        GetComponent<FadeOut>().UnFadeScreen();

        currentMusicId = UnityEngine.Random.Range(0, music.Length);
    }

    private IEnumerator MakeLateBoom()
    {
        yield return new WaitForSeconds(1);
        CreateExplosion(1);
    }

    public IEnumerator ActivateTrust() // 1..3
    {
        AudioSource chargingAS = AudioSourcePool[0];
        if (chargingAS != null)
        {
            chargingAS.clip = ChargingSound;
            chargingAS.Play();

            yield return new WaitUntil(() => !chargingAS.isPlaying);
            chargingAS.clip = expSound;
            chargingAS.Play();
            foreach (var rcf in rocketFireEffects)
            {
                rcf.SetActive(true);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitUntil(() => !chargingAS.isPlaying);
            chargingAS.clip = trustSound;
            chargingAS.Play();
            chargingAS.loop = true;
        }
        else
            throw new InvalidOperationException("Need AudioSource!");
    }

    public void CreateExplosion(float power)
    {
        explosionAS.Play();
        StartCoroutine(cameraMovement.Shake(power, 2));
    }

    private AudioSource CheckForAudioSourceInPool()
    {
        foreach(AudioSource As in AudioSourcePool)
        {
            if (!As.isPlaying)
                return As;
        }
        return null;
    }

    private void Update()
    {
        DJChecker();
    }
    private void DJChecker()
    {
        if (!musicAS.isPlaying)
        {
            if (currentMusicId > music.Length)
                currentMusicId = 0;
            musicAS.clip = music[currentMusicId];
            musicAS.Play();
        }
    }
}
