using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Arrow : MonoBehaviour
{
    public Transform target;
    [SerializeField] private RectTransform pointerRectTransform;
    [SerializeField] private Camera cameraTransform;
    [SerializeField] private float borderSize;

    [SerializeField] private Image pointerImage;
    [SerializeField] private Image circleImage;
    [SerializeField] private Image dotImage;

    [SerializeField] private Image Icon;

    private RectTransform canvasRect;
    private bool isShowing = false;

    private void Start()
    {
        cameraTransform = Camera.main;
        canvasRect = transform.parent.GetComponentInParent<RectTransform>();
    }

    private void LateUpdate()
    {
        if (isShowing)
        {
            Vector3 toPos = target.position;
            Vector3 fromPos = cameraTransform.transform.position;
            toPos.z = cameraTransform.transform.position.z;
            Vector3 dir = (toPos - fromPos).normalized;

            pointerRectTransform.right = dir;

            Vector3 targetScreenPos = cameraTransform.WorldToScreenPoint(target.position);
            bool isOffScreen = targetScreenPos.x <= borderSize || targetScreenPos.x >= Screen.width - borderSize || targetScreenPos.y <= borderSize || targetScreenPos.y >= Screen.height - borderSize;

            if (isOffScreen)
            {
                Vector3 cappedTargetScreenPos = targetScreenPos;

                if (cappedTargetScreenPos.x <= borderSize) cappedTargetScreenPos.x = borderSize;
                if (cappedTargetScreenPos.x >= Screen.width - borderSize) cappedTargetScreenPos.x = Screen.width - borderSize;
                if (cappedTargetScreenPos.y <= borderSize) cappedTargetScreenPos.y = borderSize;
                if (cappedTargetScreenPos.y >= Screen.height - borderSize) cappedTargetScreenPos.y = Screen.height - borderSize;


                pointerRectTransform.anchoredPosition = cappedTargetScreenPos - new Vector3(Screen.width / 2, Screen.height / 2, 0);
                //pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);

                pointerImage.gameObject.SetActive(true);
                circleImage.gameObject.SetActive(true);
            }
            else
            {
                pointerRectTransform.anchoredPosition = targetScreenPos - new Vector3(Screen.width / 2, Screen.height / 2, 0);
                //pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
                
                pointerImage.gameObject.SetActive(false);
                circleImage.gameObject.SetActive(false);
            }
            Icon.transform.rotation = Quaternion.identity;
            pointerRectTransform.rotation = Quaternion.Euler(0, pointerRectTransform.eulerAngles.y, pointerRectTransform.eulerAngles.z);
        }
    }

    public void GeneratePointer(Transform trg, Sprite icon)
    {
        target = trg;
        Icon.gameObject.SetActive(true);
        Icon.sprite = icon;
        ShowPointer(true);
    }
    public void ShowPointer(bool show)
    {
        if (show)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
            isShowing = true;
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
            isShowing = false;
        }
    }
}
