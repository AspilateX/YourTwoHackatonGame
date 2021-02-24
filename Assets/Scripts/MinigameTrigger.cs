using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MinigameTrigger : MonoBehaviour
{
    [SerializeField] private IMinigame minigame;
    [SerializeField] private Sprite icon;
    [SerializeField] private GameObject pointerPrefab;
    private Arrow pointer;

    public bool isModuleFixed = false;

    private void Start()
    {
        pointer = Instantiate(pointerPrefab, GameObject.Find("UI Canvas").transform.Find("Pointers").transform.position, Quaternion.identity, GameObject.Find("UI Canvas").transform.Find("Pointers").transform).GetComponent<Arrow>();
        pointer.GeneratePointer(transform, icon);
    }
    private void OnEnable()
    {
        GameEvents.current.onPlayerHitInteractButton += Interact;
        GameEvents.current.onMiniGameEnded += AfterGameLogic;
    }
    private void OnDisable()
    {
        GameEvents.current.onPlayerHitInteractButton -= Interact;
        GameEvents.current.onMiniGameEnded -= AfterGameLogic;
    }
    private bool playerInTrigger = false;
    private bool waitForEndOfMinigame = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isModuleFixed)
        {
            playerInTrigger = true;
            GameEvents.current.RequestKeyHelper(KeyCode.E);
            pointer.ShowPointer(false);
        }
    }

    private void AfterGameLogic(float reward)
    {
        if (waitForEndOfMinigame)
        {
            if (reward > 0)
            {
                AfterWin();
                isModuleFixed = true;
                GameEvents.current.FixModule();
            }
            else
            {
                AfterLose();
            }
            pointer.ShowPointer(false);
        }
        waitForEndOfMinigame = false;
    }

    public abstract void AfterWin();
    protected abstract void AfterLose();

    private void Interact()
    {
        if (playerInTrigger && !isModuleFixed)
        {
            waitForEndOfMinigame = true;
            minigame.StartMiniGame();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && !isModuleFixed)
        {
            playerInTrigger = false;
            pointer.ShowPointer(true);
            GameEvents.current.RequestHideKeyHelper();
        }
    }
}
