using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class GrabItem : MonoBehaviour
{
    [SerializeField] private float grabDistance;
    [SerializeField] private Transform grabPoint;
    [SerializeField] private float timeToNextGrab;
    private bool canGrab = true;
    private GameObject closestItem = null;
    public GameObject currentItem = null;

    private bool requestedHide = false;
    private void OnEnable()
    {
        GameEvents.current.onPlayerHitGrabButton += Grab;
    }
    private void OnDisable()
    {
        GameEvents.current.onPlayerHitGrabButton -= Grab;
    }

    private void Update()
    {
        CheckItemsInArea();
    }
    private void CheckItemsInArea()
    {
        if (currentItem == null && canGrab)
        {
            LayerMask mask = LayerMask.GetMask("Item");

            var itemInGrabArea = Physics2D.OverlapCircle(transform.position, grabDistance, mask);

            if (itemInGrabArea != null)
            {
                closestItem = itemInGrabArea.gameObject;
                GameEvents.current.RequestKeyHelper(KeyCode.G);
                requestedHide = false;
            }
            else
            {
                closestItem = null;
                if (!requestedHide)
                {
                    GameEvents.current.RequestHideKeyHelper();
                    requestedHide = true;
                }
            }
        }
    }

    private void Grab()
    {
        if (canGrab)
        {
            if (closestItem != null && currentItem == null)
            {
                currentItem = closestItem;
                currentItem.transform.position = grabPoint.transform.position;
                currentItem.transform.SetParent(grabPoint.transform);
                closestItem = null;
                GetComponent<PlayerMovement>().animator.SetBool("haveItem", true);
            }
            else if (currentItem != null)
            {
                currentItem.transform.SetParent(null);
                currentItem = null;
                GetComponent<PlayerMovement>().animator.SetBool("haveItem", false);
            }
            StartCoroutine(WaitGrabTimer());
            GameEvents.current.RequestHideKeyHelper();
            requestedHide = true;
        }
    }

    private IEnumerator WaitGrabTimer()
    {
        canGrab = false;
        yield return new WaitForSecondsRealtime(timeToNextGrab);
        canGrab = true;
    }
}
