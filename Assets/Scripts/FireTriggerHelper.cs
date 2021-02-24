using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTriggerHelper : MonoBehaviour
{
    private UIHelp helper;

    private void Start()
    {
        helper = GameObject.Find("UI Canvas").GetComponent<UIHelp>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && transform.childCount > 0)
        {
            if (collision.GetComponent<PlayerMovement>().currentItem == null)
                helper.ShowHelpMassage("Для тушения огня необходим огнетушитель!", 1);
            else
                helper.ShowHelpMassage("Нажмите \"E\" чтобы потушить огонь", 1);
        }
    }
}
