using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtingusher : MonoBehaviour, IItem
{
    [SerializeField] private float maxDistance;
    [SerializeField] private float extingushingFireTime = 2f;
    [SerializeField] private AudioSource FEAS;
    [SerializeField] private ParticleSystem particle;
    [Space]
    [SerializeField] private GameObject pointerPrefab;
    private GameObject currentFire = null;
    private PlayerMovement playerInfo;
    private Arrow pointer;
    private bool requestedHide = false;
    private bool isGrabbedByPlayer = false;
    private void Start()
    {
        pointer = Instantiate(pointerPrefab, GameObject.Find("UI Canvas").transform.Find("Pointers").transform.position, Quaternion.identity, GameObject.Find("UI Canvas").transform.Find("Pointers").transform).GetComponent<Arrow>();
        pointer.GeneratePointer(transform.Find("dotPoint").transform, GetComponent<SpriteRenderer>().sprite);
        playerInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        LayerMask mask = LayerMask.GetMask("Fire");
        var fireInArea = Physics2D.OverlapCircle(transform.position, maxDistance, mask);

        if (fireInArea != null && playerInfo.currentItem != null)
        {
            GameEvents.current.RequestKeyHelper(KeyCode.E);
            requestedHide = false;
        }
        else
        {
            if (!requestedHide)
            {
                GameEvents.current.RequestHideKeyHelper();
                requestedHide = true;
            }
        }
    }
    public void OnUse()
    {
        if (currentFire == null)
        {
            LayerMask mask = LayerMask.GetMask("Fire");
            var fireInArea = Physics2D.OverlapCircle(transform.position, maxDistance, mask);

            if (fireInArea != null)
            {
                currentFire = fireInArea.gameObject;
                StartCoroutine(ExtinguishingFireRoutine());
            }
        }
    }

    private IEnumerator ExtinguishingFireRoutine()
    {
        float timeLeft = extingushingFireTime;
        Color currentColor = currentFire.GetComponent<Renderer>().material.GetColor("FireColor");
        Color endColor = currentColor / Mathf.Pow(2, 6);
        Material currentMaterial = currentFire.GetComponent<Renderer>().material;

        FEAS.Play();
        particle.Play();
        while (timeLeft > Time.deltaTime) 
        {
            currentColor = Color.Lerp(currentColor, endColor, Time.deltaTime / timeLeft);
            currentMaterial.SetColor("FireColor", currentColor);
            timeLeft -= Time.deltaTime;
            yield return null;
        }
        FEAS.Stop();
        Destroy(currentFire);
        currentFire = null;

    }
}
