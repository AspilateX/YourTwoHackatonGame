using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera camera_;
    [SerializeField] private Transform target_;
    [Space]
    [Header("Настройки камеры")]
    [SerializeField] private float damping_;
    [SerializeField] private Vector3 offset_;
    [SerializeField] private float zoomDamping_;
    [SerializeField] private float zoomRange_;
    [SerializeField] private float forwardLookRange_;

    private float startZoom;
    private Vector3 targetVelocity;
    private Vector3 lastPosition;
    private bool isEntering = true;
    private float enteringZoom = 50;

    private void Start()
    {
        camera_.transparencySortMode = TransparencySortMode.CustomAxis;
        camera_.transparencySortAxis = Vector3.up;
        startZoom = camera_.orthographicSize;
        lastPosition = target_.position;

        StartCoroutine(Shake(0.15f, 10));
        camera_.orthographicSize = enteringZoom;
        //StartCoroutine(Entering(5));
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target_.position + offset_ + targetVelocity * forwardLookRange_;
        MoveTo(desiredPosition);

        targetVelocity = (target_.position - lastPosition) / Time.deltaTime;
        lastPosition = target_.position;

        float newZoom;

        newZoom = startZoom + targetVelocity.magnitude * zoomRange_;
        ChangeZoom(newZoom);
    }

    private void MoveTo(Vector3 targetPosition)
    {
        Vector3 smoothPos = Vector3.Lerp(camera_.transform.position, targetPosition, damping_ * Time.deltaTime);
        camera_.transform.position = smoothPos;
    }

    private void ChangeZoom(float targetZoom)
    {
        camera_.orthographicSize = Mathf.Lerp(camera_.orthographicSize, targetZoom, zoomDamping_ * Time.deltaTime);
    }

    public IEnumerator Shake(float power, float time)
    {
        Vector3 originalPos = transform.localPosition;
        float timeLeft = time;
        
        while (timeLeft >= Time.deltaTime)
        {
            float x = Random.Range(-1f, 1f) * power;
            float y = Random.Range(-1f, 1f) * power;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            timeLeft -= Time.deltaTime;
            power = Mathf.Lerp(power, 0, Time.deltaTime / timeLeft);
            yield return null;
        }
        transform.localPosition = originalPos;
    }

    //public IEnumerator Entering(float EnteringTime)
    //{
    //    float startZD = zoomDamping_;
    //    zoomDamping_ = 0.3f;
    //    camera_.orthographicSize = enteringZoom;
    //    yield return new WaitForSeconds(EnteringTime);
    //    zoomDamping_ = startZD;

    //}
}
