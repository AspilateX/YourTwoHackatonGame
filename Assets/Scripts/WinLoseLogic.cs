using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(FadeOut))]
public class WinLoseLogic : MonoBehaviour
{
    [SerializeField] private string winSceneName;
    [SerializeField] private string loseSceneName;

    public static WinLoseLogic current;

    bool isWinLoseRequested = false;

    private void Awake()
    {
        current = this;
    }

    public void Win()
    {
        if (!isWinLoseRequested)
        {
            StartCoroutine(WinRoutine());
            isWinLoseRequested = true;
        }
    }
    public void Lose()
    {
        if (!isWinLoseRequested)
        {
            StartCoroutine(LoseRoutine());
            isWinLoseRequested = true;
        }
    }

    private IEnumerator WinRoutine()
    {
        GameData.lastGameHPAmount = HealthBar.current.currentHealth;
        GameData.lastGameMaxHP = 100;
        GameData.lastGameFixedMoulesCount = ModulesLogic.current.fixedModulesAmount;
        GameData.lastGameMaxModules = ModulesLogic.current.modulesAmount;

        GetComponent<FadeOut>().FadeScreen();
        yield return new WaitUntil(() => GetComponent<FadeOut>().isFadedOut);
        SceneManager.LoadScene(winSceneName, LoadSceneMode.Single);
    }
    private IEnumerator LoseRoutine()
    {
        GameData.lastGameHPAmount = HealthBar.current.currentHealth;
        GameData.lastGameMaxHP = 100;
        GameData.lastGameFixedMoulesCount = ModulesLogic.current.fixedModulesAmount;
        GameData.lastGameMaxModules = ModulesLogic.current.modulesAmount;

        GetComponent<FadeOut>().FadeScreen();
        yield return new WaitUntil(() => GetComponent<FadeOut>().isFadedOut);
        SceneManager.LoadScene(loseSceneName, LoadSceneMode.Single);
    }
}
