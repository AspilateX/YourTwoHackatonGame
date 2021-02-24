using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract class IMinigame : MonoBehaviour
{
    public GameObject Player;
    [SerializeField] private GameObject game;
    public bool isActivated = false;
    public string HelpMassage;

    public void StartMiniGame()
    {
        if (!Player.GetComponent<PlayerMovement>().isInMinigameNow && !isActivated)
        {
            isActivated = true;
            Player.GetComponent<PlayerMovement>().LockMovement(true);
            game.SetActive(true);
            UIHelp.current.RequestBigHelp(HelpMassage);
            Realization();
            Player.GetComponent<PlayerMovement>().isInMinigameNow = true;
        }
    }
    protected abstract void Realization();
    protected void EndMiniGame(float reward)
    {
        isActivated = false;
        Player.GetComponent<PlayerMovement>().LockMovement(false);
        game.SetActive(false);
        RewardPlayer(reward);
        Player.GetComponent<PlayerMovement>().isInMinigameNow = false;
    }
    private void RewardPlayer(float reward)
    {
        GameEvents.current.EndMiniGame(reward);
    }

}
